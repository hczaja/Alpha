using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game
{
    internal abstract class GameObject
    {
        private static ulong GameObjectID = 100_000;
        public ulong ID { get; init; }

        public GameObject() => (ID) = (GameObjectID++);

        public static readonly Dictionary<ulong, GameObject> _registry
            = new Dictionary<ulong, GameObject>();
    }
}
