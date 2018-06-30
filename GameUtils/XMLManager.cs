using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameProject.GameUtils
{
    public class XMLManager
    {
        // Load Object
        public static T Load<T>(string path)
        {
            T instance;

            FileStream stream = File.OpenRead(path);
            XmlSerializer xmls = new XmlSerializer(typeof(T));

            instance = (T)xmls.Deserialize(stream);

            stream.Close();
            return instance;
        }

        // Save Object
        public static void Save(object o, string path)
        {
            FileStream stream = File.Create(path);
            XmlSerializer xmls = new XmlSerializer(o.GetType());

            xmls.Serialize(stream, o);

            stream.Close();
        }
    }
}
