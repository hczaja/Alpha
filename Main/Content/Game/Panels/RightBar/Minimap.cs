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
        private readonly RectangleShape _mapBackground;

        private readonly RectangleShape _background;

        private readonly IGameContent _gameContent;

        public static readonly float Margin = RightBarPanel.Size.Y * 0.05f;
        public static readonly Vector2f BackgroundSize = new Vector2f(RightBarPanel.Size.X, RightBarPanel.Size.X);

        public Minimap(IGameContent gameContent)
        {
            this._gameContent = gameContent;

            this._background = new RectangleShape(BackgroundSize);
            this._background.Position = RightBarPanel.Position + new Vector2f(0.0f, Options.BackgroundSize.Y);
            this._background.FillColor = Color.Black;
            this._background.OutlineThickness = 1f;
            this._background.OutlineColor = Color.Red;

            var map = this._gameContent.GetMapInfo();
            (int w, int h) sizes = Grid.GetGridDimensions(map.GridSize);
            this._gridPreview = new RectangleShape[sizes.w, sizes.h];

            float minimapSize = RightBarPanel.Size.X - Margin;

            this._mapBackground = new RectangleShape(new Vector2f(minimapSize, minimapSize));
            this._mapBackground.Position = this._background.Position + new Vector2f(Margin / 2f, Margin / 2f) ;
            this._mapBackground.FillColor = Color.Black;

            Vector2f cellSize = new Vector2f(minimapSize / sizes.w, minimapSize / sizes.h);
            
            for (int i = 0; i < sizes.w; i++)
            {
                for (int j = 0; j < sizes.h; j++)
                {
                    this._gridPreview[i, j] = new RectangleShape(cellSize);
                    this._gridPreview[i, j].Position = new Vector2f(
                        this._mapBackground.Position.X + i * cellSize.X,
                        this._mapBackground.Position.Y + j * cellSize.Y);

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
            drawer.Draw(this._mapBackground);

            foreach (var cellPreview in this._gridPreview)
            {
                drawer.Draw(cellPreview);
            }
        }
    }
}
