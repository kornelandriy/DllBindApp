using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TestDllBindApp.Interfaces;

namespace TestDllBindApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var allAssemblies = new List<Assembly>();
            var cameras = new List<ICamera>();
            var scales = new List<IScale>();
            var trafficLights = new List<ITraficLight>();
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (var dll in Directory.GetFiles(path, "lib/*.dll"))
            {
                allAssemblies.Add(Assembly.LoadFile(dll));
            }
            
            foreach(var type in allAssemblies)
            {
                cameras.AddRange(type.GetExportedTypes()
                    .Where(x => typeof(ICamera)
                        .IsAssignableFrom(x))
                    .Select(x => (ICamera) Activator.CreateInstance(x))
                    .ToArray());
                scales.AddRange(type.GetExportedTypes()
                    .Where(x => typeof(IScale)
                        .IsAssignableFrom(x))
                    .Select(x => (IScale) Activator.CreateInstance(x))
                    .ToArray());
                trafficLights.AddRange(type.GetExportedTypes()
                    .Where(x => typeof(ITraficLight)
                        .IsAssignableFrom(x))
                    .Select(x => (ITraficLight) Activator.CreateInstance(x))
                    .ToArray());
            }

            foreach (var camera in cameras)
            {
                camera.GetPicture();
            }
            
            foreach (var scale in scales)
            {
                scale.GetData();
            }
            
            foreach (var tl in trafficLights)
            {
                tl.GetDIState();
            }
        }
    }
}