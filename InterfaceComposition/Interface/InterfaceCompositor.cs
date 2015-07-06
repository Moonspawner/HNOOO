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
	                                        while (true)
	                                        {
	                                            Footer.Update();
	                                            Thread.Sleep(200);
	                                        }
	                                    });
	        updateFooter.Start();

		    _engine = engine;
            UpdateRegion();
            _engine.Navigate("home");
		}

	    private void UpdateRegion()
	    {
	        _engine.Region = new ConsoleDrawRegion(0, 0, Console.WindowWidth, Console.WindowHeight - GUIFooter.Height);
	    }
	}
}
