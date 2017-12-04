using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WorldOfImagination.Entities;

namespace WorldOfImagination.GameComponent.Scene
{
    public class GameScene : Scene
    {

        public List<Entity> Entities;
        
        public GameScene(WorldOfImaginationGame game) : base(game)
        {
            Entities = new List<Entity>();
        }

        public override void Load()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var e in Entities)
            {
                foreach (var f in Entities)
                {
                    if (e != f && e.ToRectangleF().Colide(f.ToRectangleF()))
                    {
                        e.OnColide(f);
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            
        }

        public override void Unload()
        {
            
        }
    }
}