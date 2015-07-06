namespace InterfaceComposition.Interface
{
    public struct ConsoleDrawRegion
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width{ get; set; }
        public int Height { get; set; }

        public ConsoleDrawRegion(int x, int y, int width, int height) 
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}