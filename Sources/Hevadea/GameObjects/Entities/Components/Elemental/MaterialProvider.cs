using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components.Elemental
{
    public enum Materials
    {
        Wood, Metal, Rock, Organic, Explosive, None
    }

    public enum Elements
    {
        Water, Fire, Ice, Electro, Air, None
    }

    public enum ElementalStatus
    {
        Burned, Frosted, Shocked, None 
    }
    
    public class MaterialProvider : IEntityComponentDrawable, IEntityComponentUpdatable, IEntityComponentSaveLoad
    {
        public Materials Material { get; }
        public ElementalStatus Status { get; private set; }
           
        public MaterialProvider(Materials material)
        {
            Material = material;
            
        }

        public void Interact(Elements element, float strenght)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void OnGameSave(EntityStorage store)
        {

        }

        public void OnGameLoad(EntityStorage store)
        {

        }
    }
}