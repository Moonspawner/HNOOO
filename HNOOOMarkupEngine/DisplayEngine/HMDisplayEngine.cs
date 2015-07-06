using InterfaceComposition.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using HNOOOMarkupEngine.Parser;

namespace HNOOOMarkupEngine.DisplayEngine
{
    // ReSharper disable once InconsistentNaming
	public class HMDisplayEngine : IDisplayEngine
    {
        private string _address;

		public void Keypress(ConsoleKeyInfo key)
		{
			throw new NotImplementedException();
		}

		public void Navigate(string address)
		{
            _address = address;

		    var x = 0;
		    var y = 0;
		    var directionx = 1;
		    var directiony = -1;

		    while (true)
		    {

                if (x + directionx >= Region.X + Region.Width) { directionx = -Math.Abs(directionx); }
                if (x + directionx <= Region.X) { directionx = Math.Abs(directionx); }
                if (y + directiony >= Region.Y + Region.Height) { directiony = -Math.Abs(directiony); }
                if (y + directiony <= Region.Y) { directiony = Math.Abs(directiony); }

                x += directionx;
		        y += directiony;

		        lock (InterfaceCompositor.ConsoleWriterLock)
		        {
                    Console.SetCursorPosition(x, y);

                    Console.ForegroundColor = (ConsoleColor)((int)(Console.ForegroundColor + 1) & 0xF);
                    Console.Write((int)Console.ForegroundColor > 0x7 ? '*' : ' ');
                }

		        Thread.Sleep(150);
		    }
		}

	    public ConsoleDrawRegion Region { get; set; }
    }
}
