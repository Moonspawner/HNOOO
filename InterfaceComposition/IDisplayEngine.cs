using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceComposition.Interface
{
	public interface IDisplayEngine
    {
        ///<returns>whether the keypress has been handled</returns>
        bool Keypress(ConsoleKeyInfo key);

        void Navigate(string address);

	    ConsoleDrawRegion Region { get; set; }
	}
}
