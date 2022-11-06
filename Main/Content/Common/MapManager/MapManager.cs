using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Main.Content.Common.MapManager
{
    public class MapManager
    {
        private static MapRegistry _registryInstance;
        public static MapRegistry RegistryInstance => _registryInstance ?? (_registryInstance = Deserialize());

        private static MapRegistry Deserialize()
        {
            const string path = "Content/Common/MapManager/MapRegistry.xml";

            var content = File.ReadAllText(path);

            var result = new MapRegistry();
            var serializer = new XmlSerializer(typeof(MapRegistry));
                
            using (var reader = new StringReader(content))
            {
                try
                {
                    result = serializer.Deserialize(reader) as MapRegistry;
                }
                catch (Exception)
                { }
            }

            return result;
        }
    }
}
