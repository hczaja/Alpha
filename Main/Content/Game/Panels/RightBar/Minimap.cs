using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels.RightBar
{
    public class Minimap : IDrawable, IEventHandler<UpdateMinimapEvent>
    {
        private readonly RectangleShape[,] _gridPreview;
        private readonly RectangleShape _background;

        private readonly IGameContent _gameContent;

        public Minimap(IGameContent gameContent)
        {
            this._gameContent = gameContent;

            var map = this._gameContent.GetMapInfo();
            (int w, int h) sizes = Grid.GetGridDimensions(map.GridSize);
            this._gridPreview = new RectangleShape[sizes.w, sizes.h];

            const float gap = 2f;
            Vector2f cellSize = new Vector2f((RightBarPanel.Size.X - 2 * gap) / sizes.w, (RightBarPanel.Size.X - 2 * gap) / sizes.h);

            this._background = new RectangleShape(new Vector2f(sizes.w * cellSize.X, sizes.h * cellSize.Y));
            this._background.Position = new Vector2f(gap, gap) + RightBarPanel.Position;
            this._background.OutlineThickness = 2f;
            this._background.OutlineColor = Color.Red;

            for (int i = 0; i < sizes.w; i++)
            {
                for (int j = 0; j < sizes.h; j++)
                {
                    this._gridPreview[i, j] = new RectangleShape(cellSize);
                    this._gridPreview[i, j].Position = new Vector2f(
                        gap + RightBarPanel.Position.X + i * cellSize.X,
                        gap + RightBarPanel.Position.Y + j * cellSize.Y);

                    this._gridPreview[i, j].FillColor = FogOfWar.GetFogColor();
                }
            }
        }

        public void Handle(UpdateMinimapEvent e)
        {
            Dictionary<(int i, int j), Color> mapByColors = e.Map.GetMapColorLayer();
            foreach (var cell in mapByColors)
            {
                this._gridPreview[cell.Key.i, cell.Key.j].FillColor = cell.Value;
            }
        }

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this._background);

            foreach (var cellPreview in this._gridPreview)
            {
                drawer.Draw(cellPreview);
            }
        }
    }
}
