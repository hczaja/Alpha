using Main.Content.Common.MapManager;
using Main.Content.Game;
using Main.Content.Game.Terrains;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.GameLobby.Panels.TopLeft
{
    public class MapPreview : IDrawable
    {
        private readonly RectangleShape[,] _gridPreview;

        public MapPreview(MapInfo map)
        {
            (int w, int h) sizes = Grid.GetGridDimensions(map.GridSize);
            this._gridPreview = new RectangleShape[sizes.w, sizes.h];

            Vector2f cellSize = new Vector2f(TopLeftPanel.Size.X / sizes.w, TopLeftPanel.Size.Y / sizes.h);

            for (int i = 0; i < sizes.w; i++)
            {
                for (int j = 0; j < sizes.w; j++)
                {
                    this._gridPreview[i, j] = new RectangleShape(cellSize);
                    this._gridPreview[i, j].Position = new Vector2f(
                        TopLeftPanel.Position.X + i * cellSize.X,
                        TopLeftPanel.Position.Y + j * cellSize.Y);

                    var mapField = map.MapData.Fields.FirstOrDefault(f => f.Column == i && f.Row == j);
                    if (mapField is not null) this._gridPreview[i, j].FillColor = Terrain.TerrainToColor[mapField.TerrainType];
                }
            }
        }

        public void Draw(RenderTarget drawer)
        {
            foreach (var cellPreview in this._gridPreview)
            {
                drawer.Draw(cellPreview);
            }
        }
    }
}
