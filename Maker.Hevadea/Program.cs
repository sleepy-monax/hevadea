using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Scenes;
using Maker.Rise;
using Maker.Rise.Utils.FiniteStateMachine;
using System;

namespace Maker.Hevadea
{
    public static class Program
    {
        enum test { state1, state2, state3 }

        [STAThread]
        static void Main()
        {
            var f = new FSM<test>("fsm-test");

            Engine.Initialize();
            Engine.Start(new SplashScene());
            Environment.Exit(0);
        }
    }
}