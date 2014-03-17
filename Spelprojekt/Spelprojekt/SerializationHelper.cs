using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace Spelprojekt
{
    public class SerializationHelper
    {
        private static string _saveGameFileFullPath;
        private static string _saveGameFileFullPath2;

        static SerializationHelper()
        {
            string runningDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //_saveGameFileFullPath2 = Path.Combine(runningDirectoryPath, "savedata.xml");
        }


        public static T Load<T>(string fileName)
        {
            string runningDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            _saveGameFileFullPath2 = Path.Combine(runningDirectoryPath, fileName + ".xml");

            try 
            {
                if (!File.Exists(_saveGameFileFullPath2))
                {
                    return default(T);
                }
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                Console.WriteLine("Map loaded.");

                using (FileStream stream = File.Open(_saveGameFileFullPath2, FileMode.Open,FileAccess.Read))
                {
                    return (T)serializer.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
             throw new Exception("Error loading the type '" + typeof(T) +"' from the file '" + _saveGameFileFullPath + "'. The error is: " + e.ToString(), e);
            }
        }

        public static void Save(object saveData, string fileName)
        {
            string runningDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            _saveGameFileFullPath = Path.Combine(runningDirectoryPath, fileName + ".xml");

            try
            {
                XmlSerializer serializer = new XmlSerializer(saveData.GetType());

                using (FileStream stream = File.Open(_saveGameFileFullPath, FileMode.Create, FileAccess.Write)) 
                {
                    serializer.Serialize(stream, saveData);

                    Console.WriteLine("Map saved.");
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error saving and object of the type '" + saveData.GetType() + "' to the file '" + _saveGameFileFullPath + "'. The error is: " + e.ToString(), e);
            }
        
        }




    }
}
