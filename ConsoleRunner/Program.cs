using System;
using Game;
using TextUi;

namespace ConsoleRunner
{
    class Program
    {
        static void Main()
        {
            _ = new Battle(new TextInterface(), new ConsoleEnvironment());
        }
    }
}
