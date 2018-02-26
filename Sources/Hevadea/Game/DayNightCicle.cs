using System.Threading;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;

namespace Hevadea.Game
{
    public class DayNightCicle
    {
        public double Time { get; set; }
        public double DayTime { get; set; } = 60;
        public double NightTime { get; set; } = 30;
        public double Transition { get; set; } = 3;
        public Color DayLight = Color.White;
        public Color NightColor = Color.Blue * 0.1f;
        

        public double TimeOfTheDay => (Time % (DayTime + NightTime));
        public int DayCount => (int) (Time / (DayTime + NightTime));

        private EasingManager _anim;
        
        public void UpdateTime(double elapsedTime)
        {
            Time += elapsedTime;
        }

        public Color GetAmbiantLight()
        {
            if (IsDay())
            {
                return DayLight;
            }
            else
            {
                return NightColor;
            }
        }

        public bool IsNight()
        {
            return TimeOfTheDay > DayTime;
        }

        public bool IsDay()
        {
            return TimeOfTheDay < DayTime;
        }
    }
}