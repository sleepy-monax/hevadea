using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Hevadea.Framework.Audio
{
    public class SoundEffectPool
    {
        public List<SoundEffect> Sounds { get; set; } = new List<SoundEffect>();

        public SoundEffect PickRandom()
        {
            return Rise.Rnd.Pick(Sounds);
        }
    }
}