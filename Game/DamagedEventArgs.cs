using System;

namespace Game
{
    public class DamagedEventArgs :  EventArgs
    {
        public int Value {get; private set;}
        public DamagedEventArgs(int value) => Value = value;
    }
}