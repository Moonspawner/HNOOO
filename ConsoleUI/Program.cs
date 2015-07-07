using InterfaceComposition.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HNOOOMarkupEngine.DisplayEngine;
using Snake;

namespace ConsoleUI
{
    class Program
    {
        internal static InterfaceCompositor Compositor;
        internal static LoadingScreen WelcomeScreen = new LoadingScreen();

        static void Main(string[] args)
        {
            WelcomeScreen.DisplayWelcomeAnimation();

            var initTask = new Task(InitializeBrowser);

            initTask.Start();
            initTask.Wait();

            ExpressCompleteness();
        }

        private static void ExpressCompleteness() //I might have to change that name since it's silly
        {
            Console.WriteLine("\n\nHNOOOBrowser terminated...");
            Console.ReadKey(true);
        }

        internal static void InitializeBrowser()
        {
            Compositor = new InterfaceCompositor(new SnakeDisplayEngine());
        }
    }
}