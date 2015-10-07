using Bismuth.Framework.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plan2015.Score.ScoreBoard.Settings
{
    public class Configuration
    {
        public PlayerSettings[] Players { get; set; }
        public VideoSettings Video { get; set; }
        public AudioSettings Audio { get; set; }
        public NetworkSettings Network { get; set; }

        public static Configuration Load()
        {
            XmlSerializer<Configuration> serializer = new XmlSerializer<Configuration>();
            return serializer.Load(FileHelper.CleanPath("Content\\Configuration.xml"));
        }

        public static void Save(Configuration configuration)
        {
            XmlSerializer<Configuration> serializer = new XmlSerializer<Configuration>();
            serializer.Save(FileHelper.CleanPath("Content\\Configuration.xml"), configuration);
        }

        public static void Save()
        {
            XmlSerializer<Configuration> serializer = new XmlSerializer<Configuration>();
            serializer.Save(@"C:\Test\Configuration.xml", CreateDefault());
        }

        public static Configuration CreateDefault()
        {
            return new Configuration
            {
                Players = new[]
                {
                    new PlayerSettings{Name = "Ug"},
                    new PlayerSettings{Name = "Bug"},
                    new PlayerSettings{Name = "Krog"},
                    new PlayerSettings{Name = "Rogh"}
                },
                Video = new VideoSettings
                {
                    IsFullScreen = true,
                    AutoDetectResolution = true,
                    Width = 1280,
                    Height = 720
                },
                Audio = new AudioSettings
                {
                    IsMusicEnabled = true
                },
                Network = new NetworkSettings
                {
                    ServerName = "MeServer",
                    IP = "0.0.0.0",
                    Port = 55555
                }
            };
        }
    }

    public class VideoSettings
    {
        public bool IsFullScreen { get; set; }
        public bool AutoDetectResolution { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class AudioSettings
    {
        public bool IsMusicEnabled { get; set; }
    }

    public class NetworkSettings
    {
        public string Url { get; set; }
        public string ServerName { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
    }

    public class PlayerSettings
    {
        public string Name { get; set; }
    }
}
