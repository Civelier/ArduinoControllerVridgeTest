using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ControllerInterface.DataTypes
{
    [Serializable]
    public class PropertiesData : ISerializable
    {
        private static Version _configFileVersion = new Version(1, 1, 0, 0);
        private static DirectoryInfo _appDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ArduinoControllers");
        private static FileInfo _file = new FileInfo(_appDirectory.FullName + "\\Properties.cfg");
        private static PropertiesData _instance;
        public static PropertiesData Instance
        {
            get
            {
                if (_instance == null)
                {
                    if (TryLoad(out PropertiesData instance))
                        _instance = instance;
                    else
                    {
                        _instance = new PropertiesData();
                        Save();
                    }
                }
                return _instance;
            }
        }

        public float Height
        {
            get;
            set;
        }

        public int KinectAngleOffset
        {
            get; 
            set;
        }

        public int KinectElevationAngle
        {
            get;
            set;
        }

        public PropertiesData()
        {
            Height = 1.6f;
            KinectAngleOffset = 180;
            KinectElevationAngle = -27;
        }

        public PropertiesData(SerializationInfo info, StreamingContext context)
        {
            var ver = (Version)info.GetValue("ConfigFileVersion", typeof(Version));
            if (_configFileVersion.Major > ver.Major)
                Upgrade(ver, _configFileVersion, info, context);
            Height = info.GetSingle("Height");
            KinectAngleOffset = info.GetInt32("KinectAngleOffset");
            KinectElevationAngle = info.GetInt32("KinectElevationAngle");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ConfigFileVersion", _configFileVersion);
            info.AddValue("Height", Height);
            info.AddValue("KinectAngleOffset", KinectAngleOffset);
            info.AddValue("KinectElevationAngle", KinectElevationAngle);
        }

        private static bool TryLoad(out PropertiesData instance)
        {
            try
            {
                if (_file.Exists)
                {
                    using (var fileStream = _file.OpenText())
                    {
                        using (var json = new JsonTextReader(fileStream))
                        {
                            var serializer = new JsonSerializer();
                            instance = serializer.Deserialize<PropertiesData>(json);
                        }
                    }
                    return instance != null;
                }
            }
            catch (UnauthorizedAccessException) 
            { }
            catch (SerializationException) 
            { }

            instance = null;
            return false;
        }

        static void CreateDir()
        {
            if (!_appDirectory.Exists)
            {
                _appDirectory.Create();
            }
        }

        public static void Save()
        {
            try
            {
                if (!_file.Exists)
                {
                    CreateDir();
                    _file.Create();
                }

                using (var fileStream = _file.CreateText())
                {
                    using (var json = new JsonTextWriter(fileStream))
                    {
                        var serializer = new JsonSerializer();
                        serializer.Serialize(json, Instance);
                    }
                }
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (SerializationException)
            {
            }
        }

        public static void ResetDefaults()
        {
            _instance = new PropertiesData();
            Save();
        }

        static void Upgrade(Version from, Version to, SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}
