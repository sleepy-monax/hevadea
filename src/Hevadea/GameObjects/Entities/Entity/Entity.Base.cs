using Hevadea.Framework.Graphic.Particles;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities.Blueprints;
using Hevadea.GameObjects.Entities.Graphic;
using Hevadea.Storage;
using Hevadea.Utils;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.GameObjects.Entities
{
    public partial class Entity
    {
        public int Ueid { get; set; } = -1;
        public float X { get; private set; }
        public float Y { get; private set; }
        public Direction Facing { get; set; } = Direction.South;
        public bool Removed { get; set; } = true;
        public EntityBlueprint Blueprint { get; set; }
        public Renderer Renderer { get; set; } = new NullRenderer();
        public ParticleSystem ParticleSystem { get; } = new ParticleSystem();

        public Level Level { get; set; }
        public World World { get; private set; }
        public Game Game { get; private set; }

        public int SortingOffset { get; set; } = 0;

        public void SetPosition(float x, float y)
        {
            var oldPos = GetTilePosition();
            Chunk oldChunk = Level.GetChunkAt(oldPos);

            X = x; Y = y;

            var newPos = GetTilePosition();
            Chunk newChunk = Level.GetChunkAt(newPos);

            // Remove the entity from his previous position.
            oldChunk.Entities.Remove(this);
            oldChunk.EntitiesOnTiles[oldPos.X % Chunk.CHUNK_SIZE, oldPos.Y % Chunk.CHUNK_SIZE].Remove(this);

            // Add the entity to his new position.
            newChunk.Entities.Add(this);
            newChunk.EntitiesOnTiles[newPos.X % Chunk.CHUNK_SIZE, newPos.Y % Chunk.CHUNK_SIZE].Add(this);
        }

        public string GetIdentifier()
        {
            return $"{Blueprint?.Name ?? "none"}:{Ueid:x}";
        }

        public bool IsMemberOf(BlueprintGroupe<EntityBlueprint> groupe)
        {
            return Blueprint != null && groupe.Members.Contains(Blueprint);
        }

        private static Dictionary<Direction, Anchor> DirectionToAnchore = new Dictionary<Direction, Anchor>()
        {
            { Direction.North, Anchor.Bottom },
            { Direction.South, Anchor.Top    },
            { Direction.West,  Anchor.Right  },
            { Direction.East,  Anchor.Left   },
        };

        public Rectangle GetFacingArea(int size)
        {
            return new Rectangle(Position.ToPoint() - new Rectangle(new Point(0), new Point(size)).GetAnchorPoint(DirectionToAnchore[Facing]), new Point(size));
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
                return facingEntities[0];
            }

            return null;
        }

        public virtual void OnSave(EntityStorage store)
        {
        }

        public virtual void OnLoad(EntityStorage store)
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