using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        internal static LoadingScreen WelcomeScreen = new LoadingScreen();
        internal static HNOOOBrowser.HNOOOBrowser _browser;

        static void Main(string[] args)
        {
            new Task(WelcomeScreen.DisplayWelcomeAnimation).Start();

            _browser = new HNOOOBrowser.HNOOOBrowser();
            var initTask = new Task(_browser.InitializeBrowser);

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