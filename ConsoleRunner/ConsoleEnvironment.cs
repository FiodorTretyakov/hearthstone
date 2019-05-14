using System;
using Game;

namespace ConsoleRunner
{
    public class ConsoleEnvironment : IEnvironment
    {
        public void Finish() => Environment.Exit(0);
    }
}