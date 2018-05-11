using Hevadea.Storage;

namespace Hevadea.GameObjects.Entities.Components
{
    public interface IEntityComponentSaveLoad
    {
        void OnGameSave(EntityStorage store);
        void OnGameLoad(EntityStorage store);
    }
}