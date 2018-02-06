using Maker.Hevadea.Game.Storage;

namespace Maker.Hevadea.Game.Entities.Component
{
    public interface ISaveLoadComponent
     {
        void OnGameSave(EntityStorage store);
        void OnGameLoad(EntityStorage store);
    }
 }