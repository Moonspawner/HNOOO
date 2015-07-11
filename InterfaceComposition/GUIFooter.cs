using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

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

	    public void Update() {
	        //needed because I want to draw the footer in one thing for performance reasons (writing to the console is slow, so is setting the cursor position)
	        var footerText = new Dictionary<UIState, string> {{UIState.Normal, "CTRL + G for goto"}, {UIState.ExpectInput, "URL: " + (_urlInput == "" ? "_" : _urlInput)}};

	        lock(InterfaceCompositor.ConsoleWriterLock) {
	            Console.SetCursorPosition(Region.X, Region.Y);
	            SetConsoleColor();

	            try {
	                for(Console.CursorTop = Region.Y; Console.CursorTop + 1 < Region.Height + Region.Y; Console.CursorTop++) {
	                    for(Console.CursorLeft = Region.X; Console.CursorLeft + 1 < Region.Width + Region.X;) {
	                        Console.Write(((Console.CursorTop  - Region.Y)      == 1 &&
                                          ((Console.CursorLeft - Region.X) - 1) >= 0 &&
                                          ((Console.CursorLeft - Region.X) - 1) <= (footerText[state].Length - 1))
                                            ? footerText[state][(Console.CursorLeft - Region.X) - 1] : ' ');
	                    }
	                }
	            }
	            finally { RevertConsoleColor(); }
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
	        if (key.Key == ConsoleKey.G && key.Modifiers == ConsoleModifiers.Control) {
	            lock (InterfaceCompositor.ConsoleWriterLock) {
	                Console.SetCursorPosition(Region.X + 1, Region.Y + 1);
	                state = state == UIState.Normal ? UIState.ExpectInput : UIState.Normal; //switch between UIState.Normal and UIState.ExpectInput
	            }
                _urlInput = "";
                new Task(Update).Start();
                return true;
	        } else if (state == UIState.ExpectInput) {
	            if (key.Key == ConsoleKey.Enter) {
                    return true;
	            }
	            _urlInput += key.KeyChar;
	            new Task(Update).Start();
                return true;
	        }
            return false;
	    }
    }
}
