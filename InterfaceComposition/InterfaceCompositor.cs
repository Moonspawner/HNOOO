using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterfaceComposition.Interface
{
    public class InterfaceCompositor
	{
        //http://stackoverflow.com/a/1522972
        public static readonly object ConsoleWriterLock = new object();

        private readonly IDisplayEngine _engine;

	    public GUIFooter Footer { get; set; }

	    public InterfaceCompositor(IDisplayEngine engine)
	    {
	        Footer = new GUIFooter();

	        var updateFooter = new Task(() =>
                                        {
                                            while (true) {
                                                Footer.Update();
                                                Thread.Sleep(1000);
                                            }
                                        });
	        updateFooter.Start();

		    _engine = engine;

            var updateEngineRegion = new Task(() =>
                                            {
                                                while (true) {
                                                    UpdateRegion();
                                                    Thread.Sleep(1000);
                                                }
                                            });
            UpdateRegion(); //because the program may run before the first UpdateRegion() occured and that will cause bugs
            updateEngineRegion.Start();

            _engine.Navigate("home");

	        ConsoleKeyInfo key;
	        while ((key = Console.ReadKey(true)) != null) {
	            if(!(Footer.Keypress(key) || _engine.Keypress(key))) {
	                //Keypress couldn't be handled :c
	            }
	        }
		}

	    private void UpdateRegion()
	    {
	        _engine.Region = new ConsoleDrawRegion(0, 0, Console.WindowWidth, Console.WindowHeight - GUIFooter.Height);
	    }
	}
}
