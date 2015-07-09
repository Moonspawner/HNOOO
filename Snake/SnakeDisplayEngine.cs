using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceComposition.Interface;

namespace Snake
{
    public class SnakeDisplayEngine : IDisplayEngine
    {
        private int _consoleLeft = 0;
        private int _consoleTop = 0;

        public bool Keypress(ConsoleKeyInfo key)
        {
            //a simple text editor
            if (key.Key != ConsoleKey.Backspace) {
                foreach (var @char in new[] {key.KeyChar, '_'}) {
                    lock (InterfaceCompositor.ConsoleWriterLock) {
                        Console.SetCursorPosition(_consoleLeft, _consoleTop);
                        Console.Write(@char);
                    }
                    if (++_consoleLeft >= Region.Right) {
                        _consoleTop++;
                        _consoleLeft = Region.X + 1;
                        if (_consoleTop >= Region.Bottom) {
                            Console.MoveBufferArea(Region.X, Region.Y + 1, Region.Width, Region.Height - 1, Region.X, Region.Y);
                            _consoleTop--;
                        }
                    }
                }
                _consoleLeft--;
            } else { //we typed a backspace
                _consoleLeft--;
                if (_consoleLeft < Region.X) { 
                    _consoleLeft = Region.Right - _consoleLeft - 3;
                    _consoleTop--;
                }
                if (_consoleTop < Region.Y) {
                    _consoleLeft = Region.X;
                    _consoleTop = Region.Y;
                }
                lock (InterfaceCompositor.ConsoleWriterLock) {
                    Console.SetCursorPosition(_consoleLeft, _consoleTop);
                    Console.Write('_');
                }
            }
            return true;
        }

        public void Navigate(string address)
        {
            
        }

        public ConsoleDrawRegion Region { get; set; }
    }
}
