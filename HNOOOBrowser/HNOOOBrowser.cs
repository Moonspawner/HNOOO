using System;
using System.IO;
using System.Linq;
using System.Reflection;
using InterfaceComposition.Interface;

namespace HNOOOBrowser
{
    public class HNOOOBrowser
    {
        internal InterfaceCompositor Compositor;
        private IDisplayEngine.IDisplayEngine _displayEngine;

        public void InitializeBrowser()
        {
            //loads the first IDisplayEngine it can find
            var displayengine = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll").Select(Assembly.LoadFile).SelectMany(
                currentAssembly => (currentAssembly.GetTypes().Where(
                    type => type.GetInterfaces().Contains(typeof(IDisplayEngine.IDisplayEngine))))).First();
            _displayEngine = (IDisplayEngine.IDisplayEngine)Activator.CreateInstance(displayengine);
            Compositor = new InterfaceCompositor(_displayEngine);
        }
    }
}