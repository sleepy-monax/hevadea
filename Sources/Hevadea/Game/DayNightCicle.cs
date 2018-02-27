using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Game
{
    public class DayStage
    {
        public string Name;
        public double Duration;
        public Color LightColor;

        public DayStage(string name, double duration, Color lightColor)
        {
            Name = name;
            Duration = duration;
            LightColor = lightColor;
        }
    }

    public class DayNightCicle
    {

        private List<DayStage> _stages;
        private DayStage _previousStage;
        private DayStage _currentStage;

        public double Time { get; set; }
        public double CicleDuration { get; }
        public double Transition { get; set; } = 5;
        public double TimeOfTheDay => (Time % CicleDuration);
        public int DayCount => (int) (Time / CicleDuration);


        public DayNightCicle(params DayStage[] dayStages)
        {
            Time = 0;
            _stages = dayStages.ToList();
            CicleDuration = _stages.Select(x=>x.Duration).Aggregate((a, b) => a + b);
        }

        public DayStage GetDayStage(double time)
        {
            time = time % CicleDuration;
            double counter = 0;

            foreach (var s in _stages)
            {
                counter += s.Duration;
                if (time <= counter)
                {
                    return s;
                }
            }

            return _stages.Last();
        }

        public DayStage GetCurrentStage()
        {
            return _currentStage;
        }

        public double GetTimeOfTheCurrentStage()
        {
            double counter = 0f;

            foreach (var s in _stages)
            {
                counter += s.Duration;
                if (TimeOfTheDay <= counter)
                {
                    return -(counter - TimeOfTheDay) + s.Duration;
                }
            }

            return 0;
        }

        public void UpdateTime(double elapsedTime)
        {
            Time += elapsedTime;

            var stage = GetDayStage(Time);

            if (_currentStage != stage)
            {
                _previousStage = _currentStage;
                _currentStage = stage;
            }
        }

        public Color GetAmbiantLight()
        {
            if (_previousStage == null) return _currentStage.LightColor;

            return Color.Lerp(_previousStage.LightColor, _currentStage.LightColor, Mathf.Clamp01((float)(GetTimeOfTheCurrentStage() * (1/Transition))));
        }
    }
}