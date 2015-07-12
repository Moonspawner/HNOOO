using System;
using System.Threading;
using System.Threading.Tasks;
using IDisplayEngine;

namespace InterfaceComposition.Interface {
    public class InterfaceCompositor {
        //http://stackoverflow.com/a/1522972
        public static readonly object ConsoleWriterLock = new object();

        private readonly IDisplayEngine.IDisplayEngine _engine;

        public GUIFooter Footer { get; set; }

        public InterfaceCompositor(IDisplayEngine.IDisplayEngine engine) {
            Footer = new GUIFooter();
            new Task(Footer.Update).Start();

            _engine = engine;


            UpdateRegion(); //because the program may run before the first UpdateRegion() occured and that will cause bugs
            new Task(() => {
                         while(true) {
                             UpdateRegion();
                             Thread.Sleep(200);
                         }
                     }).Start();

            new Task(() => _engine.Navigate("home")).Start(); //new Task since IDisplayEngine.Navigate() could be blocking, thus hindering keypresses from being handled

            ConsoleKeyInfo key;
            while((key = Console.ReadKey(true)) != null) {
                if(!(Footer.Keypress(key) || _engine.Keypress(key))) { Footer.Keypress(key, true); }
            }
        }

        private void UpdateRegion() {
            _engine.Region = new ConsoleDrawRegion(0, 0, Console.WindowWidth, Console.WindowHeight - GUIFooter.Height);
        }
    }
}
