using Hevadea.Storage;

namespace Hevadea.Entities
{
    public interface IEntityComponentSaveLoad
    {
        void OnGameSave(EntityStorage store);
        void OnGameLoad(EntityStorage store);
    }
}