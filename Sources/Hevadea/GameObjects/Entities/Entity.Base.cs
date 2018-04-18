using Hevadea.Framework.Graphic.Particles;
using Hevadea.GameObjects.Entities.Renderers;
using Hevadea.Registry;
using Hevadea.Storage;
using Hevadea.Utils;
using Hevadea.Worlds;
using Hevadea.Worlds.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        public GameManager.GameManager Game { get; private set; }

        public int SortingOffset { get; set; } = 0;

        public void SetPosition(float x, float y)
        {
            var oldPosition = GetTilePosition();

            X = x; Y = y;

            var pos = GetTilePosition();

            Level?.RemoveEntityFromTile(oldPosition, this);
            Level?.AddEntityToTile(pos, this);
        }

        public string GetIdentifier()
        {
            return $"{Blueprint?.Name ?? "none"}:{Ueid:x}";
        }

        public bool IsMemberOf(BlueprintGroupe<EntityBlueprint> groupe)
        {
            return Blueprint != null && groupe.Members.Contains(Blueprint);
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