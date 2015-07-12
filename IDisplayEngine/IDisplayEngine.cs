using System;

namespace IDisplayEngine
{
	public interface IDisplayEngine
    {
        ///<returns>whether the keypress has been handled</returns>
        bool Keypress(ConsoleKeyInfo key);

        void Navigate(string address);

	    ConsoleDrawRegion Region { get; set; }
	}
}
