using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KeyValue3Updater
{
    internal class Config
    {
        public bool Enable_InitFloat_4_RadToDeg { get; set; } = false;
        
        public static Config Load()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "config.json");
            if(File.Exists(path))
            {
                string fileText = File.ReadAllText(path);
                try
                {
                    Config loadedConfig = JsonSerializer.Deserialize<Config>(fileText);
                    return loadedConfig;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to parse config, is it valid JSON? Error: {e.Message}");
                }
            }

            Config config = new Config();
            Console.WriteLine("Made config");
            File.WriteAllText(path, JsonSerializer.Serialize<Config>(config, new JsonSerializerOptions() { WriteIndented = true }));
            return config;
        }
    }
}
