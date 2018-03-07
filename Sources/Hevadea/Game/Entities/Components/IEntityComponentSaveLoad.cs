using Hevadea.Game.Storage;

namespace Hevadea.Game.Entities.Components
{
    public interface IEntityComponentSaveLoad
    {
        void OnGameSave(EntityStorage store);
        void OnGameLoad(EntityStorage store);
    }
}