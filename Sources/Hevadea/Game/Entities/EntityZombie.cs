using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Ai;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Entities.Components.Render;
using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Game.Entities
{
    public class EntityZombie : Entity
    {
        public EntityZombie()
        {
            Width = 8;
            Height = 8;

            Origin = new Point(4, 4);

            Add(new Move());
            Add(new Health(10));
            Add(new Attack());
            Add(new Swim());
            Add(new Energy());
            Add(new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))));            
            Add(new Agent());
        }

        TilePosition _tagetPosition = null;
        List<PathFinder.Node> _path = null;
        int _pathIndex = 0;

        public override void OnUpdate(GameTime gameTime)
        {
            var playerPosition = Game.Player.GetTilePosition();
            _path = new PathFinder(Level).PathFinding(GetTilePosition(), playerPosition);
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (_path != null)
            {
                PathFinder.DrawPath(spriteBatch, _path, Color.Red);
            }
        }

        public override bool IsBlocking(Entity entity)
        {
            return true;
        }
    }
}