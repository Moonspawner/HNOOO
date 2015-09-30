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

        int _x = 0;
        int _y = 0;
        int _directionx = 1;
        int _directiony = -1;


        public bool Keypress(ConsoleKeyInfo key)
		{
		    switch (key.Key)
		    {
		        case ConsoleKey.LeftArrow:
                    _directionx = -Math.Abs(_directionx);
                    break;
		        case ConsoleKey.UpArrow:
                    _directiony = -Math.Abs(_directiony);
                    break;
		        case ConsoleKey.RightArrow:
                    _directionx = Math.Abs(_directionx);
                    break;
		        case ConsoleKey.DownArrow:
                    _directiony = Math.Abs(_directiony);
                    break;
		        default:
                    return false;
		    }
            return true;
		}

		public void Navigate(string address)
		{
            _address = address;

		    while (true)
		    {
		        if(_x + _directionx >= Region.X + Region.Width) { _directionx = -Math.Abs(_directionx); }
		        if(_x + _directionx <= Region.X) { _directionx = Math.Abs(_directionx); }
		        if(_y + _directiony >= Region.Y + Region.Height) { _directiony = -Math.Abs(_directiony); }
		        if(_y + _directiony <= Region.Y) { _directiony = Math.Abs(_directiony); }

                _x += _directionx;
		        _y += _directiony;

		        lock (InterfaceCompositor.ConsoleWriterLock)
		        {
                    Console.SetCursorPosition(_x, _y);

                    Console.ForegroundColor = (ConsoleColor)((int)(Console.ForegroundColor + 1) & 0xF);
                    Console.Write('*');
                }

		        Thread.Sleep(150);
		    }
		}

	    public ConsoleDrawRegion Region { get; set; }
    }
}
