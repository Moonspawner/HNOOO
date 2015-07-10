using HNOOOMarkupEngine.DisplayEngine;
using InterfaceComposition.Interface;

namespace HNOOOBrowser
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