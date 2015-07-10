using System;
using System.IO;
using System.Threading;

namespace ConsoleUI
{
	public class LoadingScreen
	{
		public void DisplayWelcomeAnimation()
		{
		    try
		    {
		        var lines = File.ReadAllText("Welcome.txt").Split('\n');
		        var arguments = lines[0].Split(' ');
		        var heightInLines = int.Parse(arguments[0]);
                var durationInMs = int.Parse(arguments[1]);

		        for(var index = 1; index < lines.Length;) {
		            Console.Clear();
		            do { Console.WriteLine(lines[index]); }
		            while((index++ - 1)%heightInLines != heightInLines - 1);

		            Thread.Sleep(durationInMs);
		        }

                Console.Clear();
            }
		    catch (Exception e)
		    {
		        Console.WriteLine("Following error occured while trying to display the welcome message:\n{0}", e);
		    }
		}
	}
}
