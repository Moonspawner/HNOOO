using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace InterfaceComposition.Interface
{
    // ReSharper disable once InconsistentNaming
	public class GUIFooter
	{
	    public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.DarkBlue;

	    public ConsoleColor TextColor { get; set; } = ConsoleColor.White;

        public string Text { get; set; }

	    public bool Scrolling { get; set; }

	    public static int Height { get; private set; } = 4;

	    private ConsoleDrawRegion Region => new ConsoleDrawRegion(0, Console.WindowHeight - Height, Console.WindowWidth, Height);

	    public void Update()
	    {
	        lock (InterfaceCompositor.ConsoleWriterLock)
	        {
	            Console.SetCursorPosition(Region.X, Region.Y);

	            var previousBackgroundColor = Console.BackgroundColor;
	            Console.BackgroundColor = BackgroundColor;

                var previousForegroundColor = Console.ForegroundColor;
                Console.ForegroundColor = TextColor;

	            for (Console.CursorLeft = Region.X; Console.CursorLeft + 1 < Region.Width + Region.X; Console.CursorLeft++)
	            {
	                for (Console.CursorTop = Region.Y; Console.CursorTop + 1 < Region.Height + Region.Y; Console.CursorTop++)
	                {
	                    var oldX = Console.CursorLeft;
	                    var oldY = Console.CursorTop;

                        Console.Write('_');
	                    Console.SetCursorPosition(oldX, oldY);
	                }
                }

	            Console.BackgroundColor = previousBackgroundColor;
                Console.ForegroundColor = previousForegroundColor;
            }
	    }
    }
}
