namespace IDisplayEngine
{
    public struct ConsoleDrawRegionHorizontal
    {
        public int X { get; set; }
        public int Width{ get; set; }

        public ConsoleDrawRegionHorizontal(int x, int width) 
        {
            X = x;
            Width = width;
        }
    }
}