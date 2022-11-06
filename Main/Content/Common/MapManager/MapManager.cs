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
            var result = new MapRegistry();
            var serializer = new XmlSerializer(typeof(MapRegistry));

            var directory = new DirectoryInfo("Content/Common/MapManager/Maps");
            foreach (var file in directory.EnumerateFiles())
            {
                var content = File.ReadAllText($"Content/Common/MapManager/Maps/{file.Name}");
                
                using (var reader = new StringReader(content))
                {
                    try
                    {
                        var registry = serializer.Deserialize(reader) as MapRegistry;
                        result.Maps.AddRange(registry.Maps);
                    }
                    catch (Exception)
                    { }
                }
            }

            return result;
        }
    }
}
