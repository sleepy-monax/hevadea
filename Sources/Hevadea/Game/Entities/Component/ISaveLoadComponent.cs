using Hevadea.Game.Storage;

namespace Hevadea.Game.Entities.Component
{
    public interface ISaveLoadComponent
    {
        void OnGameSave(EntityStorage store);
        void OnGameLoad(EntityStorage store);
    }
}