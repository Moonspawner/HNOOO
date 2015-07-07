﻿using System;
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

        public void Keypress(ConsoleKeyInfo key)
        {
            //a simple text editor
            if (key.Key != ConsoleKey.Backspace) {
                foreach (var @char in new[] {key.KeyChar, '_'}) {
                    lock (InterfaceCompositor.ConsoleWriterLock) {
                        Console.SetCursorPosition(_consoleLeft, _consoleTop);
                        Console.Write(@char);
                    }
                    if (++_consoleLeft >= Region.X + Region.Width) {
                        _consoleTop++;
                        _consoleLeft = Region.X + 1;
                    }
                }

                _consoleLeft--;
            } else {
                _consoleLeft--;
                if (_consoleLeft < Region.X) {
                    _consoleLeft = Region.X + Region.Width - _consoleLeft - 3;
                    _consoleTop--;
                }
                if (_consoleTop < Region.Y) {
                    _consoleTop = Region.X;
                }

                lock (InterfaceCompositor.ConsoleWriterLock) {
                    Console.SetCursorPosition(_consoleLeft, _consoleTop);
                    Console.Write('_');
                }
            }
        }

        public void Navigate(string address)
        {
            
        }

        public ConsoleDrawRegion Region { get; set; }
    }
}
