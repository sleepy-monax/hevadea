using Hevadea.GameObjects.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects
{
    public class Component
    {
        public Entity Entity => (Entity)GameObject;
        public GameObject Obj => GameObject;
        public GameObject GameObject { get; set; }

        public virtual void Update(GameTime gt)
        {}
        
        public virtual void Draw(SpriteBatch sb, GameTime gt)
        {}
    }
}