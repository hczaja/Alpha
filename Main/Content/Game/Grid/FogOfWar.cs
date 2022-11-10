using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game
{
    public class FogOfWar
    {
        private Dictionary<int, bool> Visibility { get; init; }

        public FogOfWar()
        {
            this.Visibility = new Dictionary<int, bool>();
            for (int id = 0; id < Player.GetMaxID(); id++)
            {
                this.Visibility[id] = false;
            }
        }

        public void DiscoverFor(int playerId) => this.Visibility[playerId] = true;
        public void HideFor(int playerId) => this.Visibility[playerId] = false;
        public bool IsVisibleFor(int playerId) => this.Visibility[playerId];

        public Color GetFogColor() => new Color(0, 51, 51);
    }
}
