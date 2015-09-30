namespace ConsoleUI
{
    public class HNOOOBrowser
    {
        internal static InterfaceCompositor Compositor;

        public static void InitializeBrowser()
        {
            Compositor = new InterfaceCompositor(new HMDisplayEngine());
        }
    }
}