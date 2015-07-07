using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceComposition.Interface
{
	public interface IDisplayEngine
	{
		void Keypress(ConsoleKeyInfo key);

        void Navigate(string address);

	    ConsoleDrawRegion Region { get; set; }
	}
}
