using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using TestDllBindApp.Interfaces;
namespace DllLibraries
{
    public class Camera : ICamera
    {
        public string GetPicture()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path += @"\config\cameras.json";
            if (File.Exists(path))
            {
                var jsonStr = File.ReadAllText(path);

                try
                {
                    var cameraConfig = JsonConvert.DeserializeObject<CameraConfig>(jsonStr);
                    foreach (var device in cameraConfig.Devices)
                    {
                        Console.WriteLine($"camera ip= {device.Ip}");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Camera config json is invalid");
                }
                
            } 
            return "Picture sent";
        }
    }
    
    public class CameraConfig
    {
        public List<Device> Devices { get; set; }
    }
        
    public class Device
    {
        public string Ip { get; set; }
        public string Port { get; set; }
    }
}