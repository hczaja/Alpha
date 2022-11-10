using Main.Content.Common.MapManager;
using Main.Content.Game.GameObjects.Buildings;
using Main.Content.Game.Terrains;
using Main.Content.Game.Turns;
using Main.Utils.Camera;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game
{
    public enum GridSize
    {
        Small, Medium, Large
    }

    internal class Grid : IDrawable, IEventHandler<MouseEvent>, IEventHandler<NewTurnEvent>
    {
        public readonly int _width;
        public readonly int _height;
        private readonly GameCamera _gameCamera;
        private readonly ITurnManager _turnManager;

        private Cell[,] _cells { get; init; }
        private Cell? _currentCell = null;

        public static (int, int) GetGridDimensions(GridSize size) => size switch
            {
                GridSize.Small => (16, 16),
                GridSize.Medium => (24, 24),
                GridSize.Large => (32, 32)
            };

        public Grid(Map map, GameCamera camera, ITurnManager turnManager)
        {
            this._gameCamera = camera;
            this._turnManager = turnManager;

            (this._width, this._height) = GetGridDimensions(map.GridSize);

            this._cells = new Cell[_width, _height];

            this.InitializeCells(map);
            this.InitializeSurroundings();
            this.InitializeStartingPositions(map);
        }

        public void Draw(RenderTarget drawer)
        {
            foreach (var cell in this._cells)
            {
                if (cell == this._currentCell)
                {
                    continue;
                }

                cell.Draw(drawer);
            }

            this._currentCell?.Draw(drawer);
        }

        public void Handle(NewTurnEvent e)
        {
            foreach (var cell in this._cells)
            {
                cell.Handle(e);
            }

            this._currentCell?.Unselect();
            this._currentCell = null;
        }

        public void Handle(MouseEvent e)
        {
            if (e.Type == MouseEventType.ButtonPressed && e.Button == Mouse.Button.Left)
            {
                var currentPlayerId = this._turnManager.GetCurrentPlayer().ID;

                float x = e.X + this._gameCamera.MoveX[currentPlayerId];
                float y = e.Y + this._gameCamera.MoveY[currentPlayerId];

                int i = (int)(x / Cell._CellSizeX);
                int j = (int)(y / Cell._CellSizeY);

                if (x < 0 || y < 0)
                {
                    this._currentCell?.Unselect();
                    this._currentCell = null;
                    return;
                }

                try
                {
                    var cell = this._cells[i, j];

                    this._currentCell?.Unselect();

                    this._currentCell = cell;
                    this._currentCell.Select();
                }
                catch (IndexOutOfRangeException)
                {
                    this._currentCell?.Unselect();
                    this._currentCell = null;
                }
            }
        }

        private void InitializeSurroundings()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    (int _i, int _j) bottomRight = (i + 1, j + 1);
                    if (bottomRight._i < _width && bottomRight._j < _height) this._cells[i, j].Surrounding.Add(Direction.BottomRight, this._cells[bottomRight._i, bottomRight._j]);
                    else this._cells[i, j].Surrounding.Add(Direction.BottomRight, null);

                    (int _i, int _j) bottom = (i, j + 1);
                    if (bottom._j < _height) this._cells[i, j].Surrounding.Add(Direction.Bottom, this._cells[bottom._i, bottom._j]);
                    else this._cells[i, j].Surrounding.Add(Direction.Bottom, null);

                    (int _i, int _j) bottomLeft = (i - 1, j + 1);
                    if (bottomLeft._i >= 0 && bottomLeft._j < _height) this._cells[i, j].Surrounding.Add(Direction.BottomLeft, this._cells[bottomLeft._i, bottomLeft._j]);
                    else this._cells[i, j].Surrounding.Add(Direction.BottomLeft, null);

                    (int _i, int _j) left = (i - 1, j);
                    if (left._i >= 0) this._cells[i, j].Surrounding.Add(Direction.Left, this._cells[left._i, left._j]);
                    else this._cells[i, j].Surrounding.Add(Direction.Left, null);

                    (int _i, int _j) topleft = (i - 1, j - 1);
                    if (topleft._i >= 0 && topleft._j >= 0) this._cells[i, j].Surrounding.Add(Direction.TopLeft, this._cells[topleft._i, topleft._j]);
                    else this._cells[i, j].Surrounding.Add(Direction.TopLeft, null);

                    (int _i, int _j) top = (i, j - 1);
                    if (top._j >= 0) this._cells[i, j].Surrounding.Add(Direction.Top, this._cells[top._i, top._j]);
                    else this._cells[i, j].Surrounding.Add(Direction.Top, null);

                    (int _i, int _j) topRight = (i + 1, j - 1);
                    if (topRight._i < _width && topRight._j >= 0) this._cells[i, j].Surrounding.Add(Direction.TopRight, this._cells[topRight._i, topRight._j]);
                    else this._cells[i, j].Surrounding.Add(Direction.TopRight, null);

                    (int _i, int _j) right = (i + 1, j);
                    if (right._i < _width) this._cells[i, j].Surrounding.Add(Direction.Right, this._cells[right._i, right._j]);
                    else this._cells[i, j].Surrounding.Add(Direction.Right, null);
                }
            }
        }

        private void InitializeCells(Map map)
        {
            var currentPlayer = this._turnManager.GetCurrentPlayer();
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    // temporary solution
                    var field = map.MapData.Fields.FirstOrDefault(f => f.Column == i && f.Row == j);
                    this._cells[i, j] = new Cell(i, j, currentPlayer, new Terrain(field.TerrainType));
                }
            }
        }

        private void InitializeStartingPositions(Map map)
        {
            var players = this._turnManager.GetAllPlayers();
            foreach (var player in players)
            {
                int playerID = player.ID;
                var startingField = map.MapData.Fields.FirstOrDefault(f => f.StartingPointFor == playerID.ToString());

                var playerStartingCell = this._cells[startingField.Column, startingField.Row];
                playerStartingCell.DiscoverFor(playerID);
                foreach (var surroundingCell in playerStartingCell.Surrounding.Where(c => c.Value is not null))
                {
                    surroundingCell.Value.DiscoverFor(playerID);
                }

                playerStartingCell.AddBuilding(new Castle(playerStartingCell.Rectangle.Position));
            }
        }
    }
}
