using Hevadea.Storage;

namespace Hevadea.GameObjects.Entities
{
    public interface IEntityComponentSaveLoad
    {
        void OnGameSave(EntityStorage store);
        void OnGameLoad(EntityStorage store);
    }
}