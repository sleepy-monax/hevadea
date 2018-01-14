using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Rise.Utils.FiniteStateMachine
{
    public struct FsmSnapshot<T>
    {
        internal T CurrentState { get; private set; }
        internal float StateAge { get; private set; }

        internal FsmSnapshot(float age, T currentState)
        {
            CurrentState = currentState;
            StateAge = age;
        }
    }
}
