using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Ai;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Entities.Components.Render;
using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Hevadea.Game.Entities.Components.Ai.Actions;

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
            Add(new Pushable());
        }


        public override void OnUpdate(GameTime gameTime)
        {
            var agent = Get<Agent>();

            if (!agent.IsBusy())
            {
                var playerPosition = Game.Player.GetTilePosition();
                List<PathFinder.Node> _path = null;
                _path = new PathFinder(Level, this).PathFinding(GetTilePosition(), playerPosition);

                if (_path != null)
                foreach (var n in _path)
                {
                    agent.ActionQueue.Enqueue(new ActionMoveToLocation(new TilePosition(n.X, n.Y)));
                }
            }
            
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public override bool IsBlocking(Entity entity)
        {
            return true;
        }
    }
}