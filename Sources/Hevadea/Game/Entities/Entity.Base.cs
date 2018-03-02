using Hevadea.Framework.Graphic.Particles;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Hevadea.Game.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public partial class Entity
    {
        #region Properties
        
        public float X { get; private set; }
        public float Y { get; private set; }
        public Point Origin { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Direction Facing { get; set; }
        public bool Removed { get; set; } = true;
        public EntityBlueprint Blueprint { get; set; } = null;
        public ParticleSystem ParticleSystem { get; } = new ParticleSystem();
        
        public Level Level { get; private set; }
        public World World { get; private set; }
        public GameManager Game { get; private set; }    
        #endregion

        #region Properties Getters and Setters

        public void SetPosition(float x, float y)
        {
            var oldPosition = GetTilePosition();

            X = x; Y = y;

            var pos = GetTilePosition();

            Level?.RemoveEntityFromTile(oldPosition, this);
            Level?.AddEntityToTile(pos, this);
        }

        #endregion
        
        #region Constructor
        
        public Entity()
        {
            Facing = Direction.Down;
            Origin = new Point(0, 0);
            Width = 32;
            Height = 32;
        }
        
        #endregion

        #region Virtual methodes

        public virtual void OnSave(EntityStorage store){}
        public virtual void OnLoad(EntityStorage store){}
        public virtual bool IsBlocking(Entity entity) => false;
        public virtual void OnUpdate(GameTime gameTime) {}
        public virtual void OnDraw(SpriteBatch spriteBatch, GameTime gameTime){}
        
        #endregion
    }
}