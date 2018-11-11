using Hevadea.Entities.Blueprints;
using Hevadea.Entities.Components;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic.Particles;
using Hevadea.Framework.Utils;
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
        public int Ueid { get; set; } = -1;
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; set; } = 0f;

        public Vector2 Position2D { get => new Vector2(X, Y); set => SetPosition(value.X, value.Y); }
        public Vector3 Position3D { get => new Vector3(X, Y, Z); set => SetPosition(value.X, value.Y, value.Z); }


        public bool Initialized { get; private set; } = false;
        public bool Removed { get; set; } = true;
        public EntityBlueprint Blueprint { get; set; }

        public Coordinates Coordinates => new Coordinates((int)(X / Game.Unit), (int)(Y / Game.Unit));
        public Coordinates FacingCoordinates => new Coordinates(FacingVector.X + Coordinates.X, FacingVector.Y + Coordinates.Y);
        public Direction Facing { get; set; } = Direction.South;
        public int SortingOffset { get; set; } = 0;
        public List<EntityComponent> Componenents { get; set; } = new List<EntityComponent>();
        public ParticleSystem ParticleSystem { get; } = new ParticleSystem();
        public Point FacingVector => Facing.ToPoint();
        public string Identifier => $"{Blueprint?.Name ?? "none"}:{Ueid:x}";
        public Tile TileOver { get => Level.GetTile(Coordinates); set => Level.SetTile(Coordinates, value); }

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
            if (Componenents.Any(e => e == component)) throw new Exception($"Duplicated component ({typeof(T).Name}) !");

            Componenents.Add(component);
            component.Owner = this;

            return component;
        }

        public T GetComponent<T>() where T : EntityComponent
        {
            var result = Componenents.OfType<T>();
            return result.Count() > 0 ? result.First() : null;
        }

        public EntityComponent GetEntityComponent(Type type)
        {
            var result = Componenents.Where(c => c.GetType() == type);
            return result.Count() > 0 ? result.First() : null;
        }

        public bool HasComponent<T>(out T component) where T : EntityComponent
        {
            component = GetComponent<T>();
            return component != null;
        }

        public bool HasComponent(Type type)
            => Componenents.Any(c => c.GetType() == type);

        public bool HasComponent<T>() where T : EntityComponent
            => Componenents.Any(c => c is T);

        public bool HasAnyComponent(params Type[] types)
        {
            for (int i = 0; i < types.Length; i++)
                foreach (var c in Componenents)
                    if (c.GetType().Equals(types[i]))
                        return true;

            return types.Length == 0;
        }

        public bool HasNoneComponent(params Type[] types)
        {
            foreach (var type in types)
            {
                foreach (var c in Componenents)
                {
                    if (c.GetType() == type)
                    {
                        return false;
                    };
                }
            }

            return true;
        }

        public bool HasAllComponent(params Type[] types)
        {
            int matchedComponents = 0;

            for (int i = 0; i < types.Length; i++)
                foreach (var c in Componenents)
                    if (c.GetType().Equals(types[i]))
                    {
                        matchedComponents++;
                        break;
                    }

            return types.Length == 0 || (matchedComponents == types.Length && matchedComponents != 0);
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

            if (Ueid == -1 && world != null)
            {
                Ueid = world.GetUeid();
            }

            Initialized = true;
        }

        public Entity Load(EntityStorage store)
        {
            Ueid = store.Ueid;
            X = store.ValueOf("X", X);
            Y = store.ValueOf("Y", Y);

            Facing = (Direction)store.ValueOf("Facing", (int)Facing);

            Componenents
                .OfType<IEntityComponentSaveLoad>()
                .ForEarch(x => x.OnGameLoad(store));

            OnLoad(store);

            return this;
        }

        public EntityStorage Save()
        {
            var store = new EntityStorage(Blueprint.Name, Ueid);

            store.Value("X", X);
            store.Value("Y", Y);
            store.Value("Facing", (int)Facing);

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

        public void SetPosition(float x, float y) => SetPosition(x, y, Z);
        public void SetPosition(float x, float y, float z)
        {
            var oldPos = Coordinates;
            Chunk oldChunk = Level.GetChunkAt(oldPos);

            X = x;
            Y = y;
            Z = z;
            var newPos = Coordinates;
            Chunk newChunk = Level.GetChunkAt(newPos);

            // Remove the entity from his previous position.
            oldChunk.Entities.Remove(this);
            oldChunk.EntitiesOnTiles[oldPos.X % Chunk.CHUNK_SIZE, oldPos.Y % Chunk.CHUNK_SIZE].Remove(this);

            // Add the entity to his new position.
            if (newChunk != null)
            {
                newChunk.Entities.Add(this);
                newChunk.EntitiesOnTiles[newPos.X % Chunk.CHUNK_SIZE, newPos.Y % Chunk.CHUNK_SIZE].Add(this);
            }
        }

        public bool MemberOf(BlueprintGroupe<EntityBlueprint> groupe)
            => Blueprint != null && groupe.Members.Contains(Blueprint);

        private static Dictionary<Direction, Anchor> DirectionToAnchore = new Dictionary<Direction, Anchor>()
        {
            { Direction.North, Anchor.Bottom },
            { Direction.South, Anchor.Top    },
            { Direction.West,  Anchor.Right  },
            { Direction.East,  Anchor.Left   },
        };

        public Rectangle GetFacingArea(int size)
        {
            return new Rectangle(Position2D.ToPoint() - new Rectangle(new Point(0), new Point(size)).GetAnchorPoint(DirectionToAnchore[Facing]), new Point(size));
        }

        public List<Entity> GetFacingEntities(int area)
        {
            var facingEntities = Level.GetEntitiesOnArea(GetFacingArea(area));
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

            if (facingEntities.Count > 0)
            {
                return facingEntities.First();
            }

            return null;
        }

        public List<Entity> GetEntitiesInRadius(float radius)
        {
            return Level?.GetEntitiesOnArea(Position2D, radius);
        }

        /* --- Game loop --------------------------------------------------- */

        public void Update(GameTime gameTime)
        {
            Componenents
                .OfType<IEntityComponentUpdatable>()
                .ForEarch(x => x.Update(gameTime));

            OnUpdate(gameTime);
            ParticleSystem.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Componenents
                .OfType<IEntityComponentDrawable>()
                .ForEarch(x => x.Draw(spriteBatch, gameTime));

            OnDraw(spriteBatch, gameTime);
            ParticleSystem.Draw(spriteBatch, gameTime);
        }

        public void Overlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Componenents
                .OfType<IEntityComponentOverlay>()
                .ForEarch(x => x.Overlay(spriteBatch, gameTime));
        }

        /* --- Virtual functions ------------------------------------------- */

        public virtual void OnLoad(EntityStorage store)
        {
        }

        public virtual void OnSave(EntityStorage store)
        {
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
        }

        public virtual void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }
    }
}