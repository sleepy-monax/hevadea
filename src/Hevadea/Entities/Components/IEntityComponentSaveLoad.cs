using Hevadea.Storage;

namespace Hevadea.Entities.Components
{
    public interface IEntityComponentSaveLoad
    {
        void OnGameSave(EntityStorage store);

        void OnGameLoad(EntityStorage store);
    }
}