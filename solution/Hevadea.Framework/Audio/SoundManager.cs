using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Hevadea.Framework.Audio
{
    public class SoundManager
    {
        private float _transition = 1f;

        public List<SoundEffectInstance> SoundEffectInstances { get; set; } = new List<SoundEffectInstance>();

        public Song PlayingSong { get; set; }
        public Song NextSong { get; set; }

        public void Play(SoundEffectPool soundEffectPool, bool RandomPitch = false)
        {
            Play(soundEffectPool.PickRandom(), RandomPitch);
        }

        public void Play(SoundEffect soundEffect, bool RandomPitch = false)
        {
            var instance = soundEffect.CreateInstance();
            if (RandomPitch) instance.Pitch = Rise.Rnd.NextFloatRange(0.5f) - 0.25f;
            instance.Volume = Rise.Config.MasterVolume * Rise.Config.EffectVolume;
            SoundEffectInstances.Add(instance);
            instance.Play();
        }

        public void Play(Song song)
        {
            if (PlayingSong != song)
            {
                if (PlayingSong == null)
                {
                    PlayingSong = song;
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(PlayingSong);
                }
                else
                {
                    NextSong = song;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (NextSong != null) _transition = Mathf.Clamp01(_transition - gameTime.GetDeltaTime());

            if (_transition == 0f)
            {
                _transition = 1f;
                PlayingSong = NextSong;
                NextSong = null;
                MediaPlayer.Play(PlayingSong);
            }

            MediaPlayer.Volume = Rise.Config.MasterVolume * Rise.Config.MusicVolume * _transition;

            var finishInstance = new List<SoundEffectInstance>();

            foreach (var instance in SoundEffectInstances)
            {
                if (instance.State != SoundState.Playing) finishInstance.Add(instance);

                instance.Volume = Rise.Config.MasterVolume * Rise.Config.EffectVolume;
            }

            finishInstance.ForEach(i => SoundEffectInstances.Remove(i));
        }
    }
}