using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Maker.Rise.Components
{
    public class AudioManager : GameComponent
    {

        private List<SoundEffectInstance> PlayingSoundEffects;
        public float MasterVolume = 0.75f;
        public float EffectVolume = 0.5f;


        public AudioManager(RiseGame game) : base(game)
        {
            PlayingSoundEffects = new List<SoundEffectInstance>();            
        }

        public override void Initialize()
        {

        }

        public override void Draw(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            for (int index = 0; index < PlayingSoundEffects.Count; index++)
            {
                var i = PlayingSoundEffects[index];
                i.Volume = MasterVolume * EffectVolume;

                if (i.State == SoundState.Stopped)
                {
                    PlayingSoundEffects.Remove(i);
                    i.Dispose();
                    index--;
                }
            }
        }

        public void PlaySoundEffect(SoundEffect soundEffect)
        {
            var instance = soundEffect.CreateInstance();
            PlayingSoundEffects.Add(instance);
            instance.Volume = MasterVolume * EffectVolume;
            instance.Play();
        }

        public void StopAll()
        {
            foreach (var i in PlayingSoundEffects)
            {
                i.Stop();
            }
        }
    }
}
