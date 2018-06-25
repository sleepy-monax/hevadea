using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.Audio
{
    public class SoundEffectPool
    {
        public List<SoundEffect> Sounds { get; set; } = new List<SoundEffect>();

        public SoundEffect PickRandom()
        {
            return Rise.Rnd.NextValue(Sounds);
        }
    }
}
