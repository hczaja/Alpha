using Main.Content.Common;
using Main.Content.Common.MapManager;
using Main.Content.Game;
using Main.Content.Game.Factions;
using Main.Content.Game.Terrains;
using Main.Utils.Events;
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
    public class MapPreview : IDrawable, IEventHandler<GameLobbyResultPlayersChanged>
    {
        private readonly RectangleShape[,] _gridPreview;
        private readonly Dictionary<string, (int, int)> _startingPoints;

        public MapPreview(Map map, IPlayerManager playerManager)
        {
            this._startingPoints = new Dictionary<string, (int, int)>();

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
                    if (mapField is not null)
                    {
                        var color = Terrain.TerrainToColor[mapField.TerrainType];
                        if (!string.IsNullOrEmpty(mapField.StartingPointFor))
                        {
                            var player = playerManager.Players
                                .FirstOrDefault(p => p.Index == mapField.StartingPointFor);

                            if (player is not null)
                            {
                                color = this.GetMapPreviewPlayerColor(player);
                                this._startingPoints[player.Index] = (i, j);

                                this._gridPreview[i, j].OutlineColor = Color.Red;
                                this._gridPreview[i, j].OutlineThickness = 2.0f;
                            }
                        }

                        this._gridPreview[i, j].FillColor = color;
                    }
                }
            }
        }

        public void Handle(GameLobbyResultPlayersChanged e)
        {
            var players = e.PlayerManager.Players;
            
            foreach (var player in players)
            {
                var color = this.GetMapPreviewPlayerColor(player);

                (int column, int row) position = this._startingPoints[player.Index];
                this._gridPreview[position.column, position.row].FillColor = color;
            }
        }

        private Color GetMapPreviewPlayerColor(PlayerInfo player)
        {
            return player.Type == PlayerType.Empty.ToString()
                ? Color.Black
                : FactionTypeExtensions.FactionToColor(Enum.Parse<FactionType>(player.Faction));
        }

        public void Draw(RenderTarget drawer)
        {
            foreach (var cellPreview in this._gridPreview)
            {
                drawer.Draw(cellPreview);
            }

            foreach ((int i, int j) startingPoints in this._startingPoints.Values)
            {
                drawer.Draw(this._gridPreview[startingPoints.i, startingPoints.j]);
            }
        }
    }
}
