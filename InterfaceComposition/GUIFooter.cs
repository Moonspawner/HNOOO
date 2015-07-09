using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace InterfaceComposition.Interface
{
    // ReSharper disable once InconsistentNaming
	public class GUIFooter
	{
	    private enum UIState
	    {
	        Normal,
            ExpectInput
	    }

        #region vars
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.DarkBlue;

	    public ConsoleColor TextColor { get; set; } = ConsoleColor.White;

        public string Text { get; set; }

	    public bool Scrolling { get; set; }

	    public static int Height { get; private set; } = 4;

	    private ConsoleDrawRegion Region => new ConsoleDrawRegion(0, Console.WindowHeight - Height, Console.WindowWidth, Height);

        ConsoleColor _previousBackgroundColor;
        ConsoleColor _previousForegroundColor;

	    private UIState state = UIState.Normal;

	    private string _urlInput = "";
        #endregion

        public void Update()
	    {
	        lock (InterfaceCompositor.ConsoleWriterLock)
	        {
	            Console.SetCursorPosition(Region.X, Region.Y);

	            SetConsoleColor();

	            for (Console.CursorLeft = Region.X; Console.CursorLeft + 1 < Region.Width + Region.X; Console.CursorLeft++)
	            {
	                for (Console.CursorTop = Region.Y; Console.CursorTop + 1 < Region.Height + Region.Y; Console.CursorTop++)
	                {
	                    var oldX = Console.CursorLeft;
	                    var oldY = Console.CursorTop;

                        Console.Write(' ');
	                    Console.SetCursorPosition(oldX, oldY);
	                }
                }

	            switch (state)
	            {
	                case UIState.Normal:
                        Console.SetCursorPosition(Region.X + 1, Region.Y + 1);
                        Console.Write("STRG + G for goto");
                        break;
                    case UIState.ExpectInput:
                        Console.SetCursorPosition(Region.X + 1, Region.Y + 1);
                        Console.Write("URL: " + (_urlInput == "" ? "_" : _urlInput));
                        break;
	            }


                RevertConsoleColor();
	        }
	    }

	    private void RevertConsoleColor()
	    {
	        Console.BackgroundColor = _previousBackgroundColor;
	        Console.ForegroundColor = _previousForegroundColor;
	    }

	    private void SetConsoleColor()
	    {
            _previousBackgroundColor = Console.BackgroundColor;
	        Console.BackgroundColor = BackgroundColor;

            _previousForegroundColor = Console.ForegroundColor;
	        Console.ForegroundColor = TextColor;
	    }

	    public bool Keypress(ConsoleKeyInfo key)
	    {
	        if (key.Key == ConsoleKey.G && key.Modifiers == ConsoleModifiers.Control)
	        {
	            lock (InterfaceCompositor.ConsoleWriterLock) {
	                Console.SetCursorPosition(Region.X + 1, Region.Y + 1);
	                state = state == UIState.Normal ? UIState.ExpectInput : UIState.Normal; //switch between UIState.Normal and UIState.ExpectInput
	            }
                _urlInput = "";
                return true;
	        } else if (state == UIState.ExpectInput) {
	            _urlInput += key.KeyChar;
                return true;
	        }
            return false;
	    }
    }
}
