using Hevadea.Framework.Graphic.Particles;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Hevadea.Game.Worlds;
using Hevadea.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public partial class Entity
    {
        #region Properties
        public int Ueid = -1;
        public float X { get; private set; }
        public float Y { get; private set; }
        public Direction Facing { get; set; } = Direction.South;
        public bool Removed { get; set; } = true;
        public EntityBlueprint Blueprint { get; set; } = null;
        public ParticleSystem ParticleSystem { get; } = new ParticleSystem();
        
        public Level Level { get; set; }
        public World World { get; private set; }
        public GameManager Game { get; private set; }   
        
        public int SortingOffset { get; set; } = 0;
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

        public string GetIdentifier()
        {
            return $"<{Blueprint?.Name ?? "none"}>{Ueid}[{(int)X}, {(int)Y}]";
        }
        #endregion

        #region Virtual methodes

        public virtual void OnSave(EntityStorage store){}
        public virtual void OnLoad(EntityStorage store){}
        public virtual void OnUpdate(GameTime gameTime) {}
        public virtual void OnDraw(SpriteBatch spriteBatch, GameTime gameTime){}
        
        #endregion
    }
}