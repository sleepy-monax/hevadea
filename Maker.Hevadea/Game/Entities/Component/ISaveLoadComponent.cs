using Maker.Hevadea.Game.Storage;

namespace Maker.Hevadea.Game.Entities.Component
{
    public interface ISaveLoadComponent
     {
        void OnSave(EntityStorage store);
        void OnLoad(EntityStorage store);
    }
 }