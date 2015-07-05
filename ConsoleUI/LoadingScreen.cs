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
		        var heightInLines = int.Parse(lines[0].Split(' ')[0]);
                var durationInMs = int.Parse(lines[0].Split(' ')[1]); //vioalting DRY here ;__;

		        for (var index = 1; index < lines.Length; index += heightInLines) {
		            Console.Clear();
		            for (var subline = 0; subline < heightInLines; subline++) { //I feel like working with index here to save a variable but unfortunately I don't think that's possible
		                Console.WriteLine(lines[index + subline]);
		            }

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
