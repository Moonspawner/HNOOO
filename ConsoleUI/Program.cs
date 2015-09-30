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
        internal static LoadingScreen WelcomeScreen = new LoadingScreen();

        static void Main(string[] args)
        {
            WelcomeScreen.DisplayWelcomeAnimation();

            var initTask = new Task(HNOOOBrowser.HNOOOBrowser.InitializeBrowser);

            initTask.Start();
            initTask.Wait();

            ExpressCompleteness();
        }

        private static void ExpressCompleteness() //I might have to change that name since it's silly
        {
            Console.WriteLine("\n\nHNOOOBrowser terminated...");
            Console.ReadKey(true);
        }
    }
}