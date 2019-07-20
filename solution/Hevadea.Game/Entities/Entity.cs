using Hevadea.Entities.Blueprints;
using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic.Particles;
using Hevadea.Registry;
using Hevadea.Storage;
using Hevadea.Tiles;
using Hevadea.Utils;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Entities
{
    public class Entity
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public Vector2 Position
        {
            get => new Vector2(X, Y);
            set => SetPosition(value.X, value.Y);
        }

        public bool Initialized { get; private set; } = false;
        public bool Removed { get; set; } = true;
        public EntityBlueprint Blueprint { get; set; }

        public Coordinates Coordinates => new Coordinates((int) (X / Game.Unit), (int) (Y / Game.Unit));

        public Coordinates FacingCoordinates =>
            new Coordinates(FacingVector.X + Coordinates.X, FacingVector.Y + Coordinates.Y);

        public Direction Facing { get; set; } = Direction.South;
        public int SortingOffset { get; set; } = 0;
        public List<EntityComponent> Componenents { get; set; } = new List<EntityComponent>();
        public ParticleSystem ParticleSystem { get; } = new ParticleSystem();
        public Point FacingVector => Facing.ToPoint();
        public string Identifier => $"{Blueprint?.Name ?? "none"}";

        public Tile TileOver
        {
            get => Level.GetTile(Coordinates);
            set => Level.SetTile(Coordinates, value);
        }

        public GameState GameState { get; private set; }
        public World World { get; private set; }
        public Level Level { get; set; }

        public Entity()
        {
        }

        /* --- Components -------------------------------------------------- */

        public T AddComponent<T>(T component) where T : EntityComponent
        {
            if (Initialized) throw new Exception($"Trying to add component ({typeof(T).Name}) at runtime !");
            if (Componenents.Any(e => e == component))
                throw new Exception($"Duplicated component ({typeof(T).Name}) !");

            Componenents.Add(component);
            component.Owner = this;

            return component;
        }

        public T GetComponent<T>() where T : EntityComponent
        {
            var result = Componenents.OfType<T>();
            return result.Any() ? result.First() : null;
        }

        public EntityComponent GetEntityComponent(Type type)
        {
            var result = Componenents.Where(c => c.GetType() == type);
            return result.Any() ? result.First() : null;
        }

        public bool HasComponent<T>(out T component) where T : EntityComponent
        {
            component = GetComponent<T>();
            return component != null;
        }

        public bool HasComponent(Type type)
        {
            return Componenents.Any(c => c.GetType() == type);
        }

        public bool HasComponent<T>() where T : EntityComponent
        {
            return Componenents.Any(c => c is T);
        }

        public bool HasAnyComponent(params Type[] types)
        {
            for (var i = 0; i < types.Length; i++)
                foreach (var c in Componenents)
                    if (c.GetType().Equals(types[i]))
                        return true;

            return types.Length == 0;
        }

        public bool HasNoneComponent(params Type[] types)
        {
            foreach (var type in types)
            foreach (var c in Componenents)
            {
                if (c.GetType() == type) return false;
                ;
            }

            return true;
        }

        public bool HasAllComponent(params Type[] types)
        {
            var matchedComponents = 0;

            for (var i = 0; i < types.Length; i++)
                foreach (var c in Componenents)
                    if (c.GetType().Equals(types[i]))
                    {
                        matchedComponents++;
                        break;
                    }

            return types.Length == 0 || matchedComponents == types.Length && matchedComponents != 0;
        }

        public bool Match(Filter filter)
        {
            return HasAllComponent(filter.AllType.ToArray()) &&
                   HasAnyComponent(filter.AnyType.ToArray()) &&
                   HasNoneComponent(filter.NoneType.ToArray());
        }

        public static Entity operator +(Entity left, EntityComponent right)
        {
            left.AddComponent(right);
            return left;
        }

        /* --- Operations -------------------------------------------------- */

        internal void Initialize(Level level, World world, GameState gameState)
        {
            Level = level;
            World = world;
            GameState = gameState;

            Componenents.Sort((a, b) => (0xff - a.Priority).CompareTo(0xff - b.Priority));

            Initialized = true;
        }

        public Entity Load(EntityStorage store)
        {
            X = store.ValueOf("X", X);
            Y = store.ValueOf("Y", Y);

            Facing = (Direction) store.ValueOf("Facing", (int) Facing);

            Componenents
                .OfType<IEntityComponentSaveLoad>()
                .ForEarch(x => x.OnGameLoad(store));

            OnLoad(store);

            return this;
        }

        public EntityStorage Save()
        {
            var store = new EntityStorage(Blueprint.Name);

            store.Value("X", X);
            store.Value("Y", Y);
            store.Value("Facing", (int) Facing);

            Componenents
                .OfType<IEntityComponentSaveLoad>()
                .ForEarch(x => x.OnGameSave(store));

            OnSave(store);

            return store;
        }

        public void Remove()
        {
            Level.RemoveEntity(this);
        }

        public void SetPosition(float x, float y)
        {
            if (!Removed)
            {
                var oldPos = Coordinates;
                var oldChunk = Level.GetChunkAt(oldPos);

                X = x;
                Y = y;
                var newPos = Coordinates;
                var newChunk = Level.GetChunkAt(newPos);

                // Remove the entity from his previous position.
                oldChunk.Entities.Remove(this);
                oldChunk.EntitiesOnTiles[oldPos.X % Chunk.SIZE, oldPos.Y % Chunk.SIZE].Remove(this);

                // Add the entity to his new position.
                if (newChunk != null)
                {
                    newChunk.Entities.Add(this);
                    newChunk.EntitiesOnTiles[newPos.X % Chunk.SIZE, newPos.Y % Chunk.SIZE].Add(this);
                }
            }
            else
            {
                X = x;
                Y = y;
            }
        }

        public bool MemberOf(Groupe<EntityBlueprint> groupe)
        {
            return Blueprint != null && groupe.Members.Contains(Blueprint);
        }

        private static readonly Dictionary<Direction, Anchor> DirectionToAnchore = new Dictionary<Direction, Anchor>()
        {
            {Direction.North, Anchor.Bottom},
            {Direction.South, Anchor.Top},
            {Direction.West, Anchor.Right},
            {Direction.East, Anchor.Left},
        };

        public Rectangle GetFacingArea(int size)
        {
            return new Rectangle(
                Position.ToPoint() -
                new Rectangle(new Point(0), new Point(size)).GetAnchorPoint(DirectionToAnchore[Facing]),
                new Point(size));
        }

        public List<Entity> GetFacingEntities(int areaSize)
        {
            var facingEntities = Level.QueryEntity(GetFacingArea(areaSize)).ToList();
            facingEntities.Sort((a, b) =>
            {
                return Mathf.Distance(a.X, a.Y, X, Y)
                    .CompareTo(Mathf.Distance(b.X, b.Y, X, Y));
            });

            facingEntities.Remove(this);

            return facingEntities;
        }

        public Entity GetFacingEntity(int area)
        {
            var facingEntities = GetFacingEntities(area);

            if (facingEntities.Count > 0) return facingEntities.First();

            return null;
        }

        public IEnumerable<Entity> GetEntitiesInRadius(float radius)
        {
            return Level?.QueryEntity(Position, radius);
        }

        /* --- Game loop --------------------------------------------------- */

        private int _lastUpdateTick = -1;

        public void Update(GameTime gameTime)
        {
            if (_lastUpdateTick != Rise.MonoGame.Ticks)
            {
                _lastUpdateTick = Rise.MonoGame.Ticks;

                foreach (var sys in SYSTEMS.UpdateSystems)
                    if (sys.Enable && Match(sys.Filter))
                        sys.Update(this, gameTime);


                Componenents
                    .OfType<IEntityComponentUpdatable>()
                    .ForEarch(x => x.Update(gameTime));

                OnUpdate(gameTime);
                ParticleSystem.Update(gameTime);
            }
        }

        private int _lastDrawTick = -1;

        public void Draw(LevelSpriteBatchPool spriteBatchPool, GameTime gameTime)
        {
            if (_lastDrawTick != Rise.MonoGame.Ticks)
            {
                _lastDrawTick = Rise.MonoGame.Ticks;

                foreach (var sys in SYSTEMS.DrawSystems)
                    if (sys.Enable && Match(sys.Filter))
                        sys.Draw(this, spriteBatchPool, gameTime);


                Componenents
                    .OfType<IEntityComponentDrawable>()
                    .ForEarch(x => x.Draw(spriteBatchPool.Entities, gameTime));

                OnDraw(spriteBatchPool.Entities, gameTime);
                ParticleSystem.Draw(spriteBatchPool.Entities, gameTime);
            }
        }

        public void Overlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Componenents
                .OfType<IEntityComponentOverlay>()
                .ForEarch(x => x.Overlay(spriteBatch, gameTime));
        }

        /* --- Virtual functions ------------------------------------------- */

        public virtual void OnLoad(EntityStorage store)
        {}

        public virtual void OnSave(EntityStorage store)
        {}

        public virtual void OnUpdate(GameTime gameTime)
        {}

        public virtual void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {}
    }
}