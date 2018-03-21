using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Ai;
using Hevadea.Game.Entities.Components.Ai.Actions;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Entities.Components.Render;
using Hevadea.Game.Registry;
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
            Attach(new Move());
            Attach(new Health(10));
            Attach(new Attack());
            Attach(new Swim());
            Attach(new Energy());
            Attach(new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))));            
            Attach(new Agent());
            Attach(new Pushable() { CanBePushBy = { ENTITIES.PLAYER } });
            Attach(new Colider(new Rectangle(-2, -2, 4, 4)));
            Attach(new Burnable(1f));
        }

        public override void OnUpdate(GameTime gameTime)
        {
            var agent = Get<Agent>();

            if (!agent.IsBusy())
            {
                var playerPosition = Game.MainPlayer.GetTilePosition();
                List<PathFinder.Node> _path = null;
                _path = new PathFinder(Level, this).PathFinding(GetTilePosition(), playerPosition);

                if (_path != null)
                foreach (var n in _path)
                {
                    agent.ActionQueue.Enqueue(new ActionMoveToLocation(new TilePosition(n.X, n.Y), 0.5f));
                }
            }
            
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }
    }
}