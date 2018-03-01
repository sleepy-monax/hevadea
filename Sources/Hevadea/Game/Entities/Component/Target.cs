using Hevadea.Framework.Graphic;
using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Component
{
    public class Target : EntityComponent, IDrawableOverlayComponent
    {
        private Entity _targetEntity = null;
        private TilePosition _targetTile = null;
        
        public void SetTarget(Entity e)
        {
            _targetEntity = e;
            _targetTile = null;
        }

        public void SetTarget(int tx, int ty)
        {
            _targetEntity = null;
            _targetTile = new TilePosition(tx, ty);
        }

        public Entity GetTargetEntity()
        {
            return _targetEntity;
        }
        
        public TilePosition GetTargetLocation()
        {
            if (!HasTarget()) return null;
            return _targetEntity != null ? _targetEntity.GetTilePosition() : _targetTile;
        }
        
        public bool HasTarget()
        {
            return _targetEntity != null && _targetTile != null;
        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!HasTarget()) return;
            
            var target = GetTargetLocation();
            spriteBatch.DrawLine(Owner.X, Owner.Y, target.WorldX, target.WorldY, Color.Blue);
            spriteBatch.PutPixel(new Vector2(target.WorldX, target.WorldY), Color.Blue, 4f);
        }
    }
}