using Main.Content.Common.MapManager;
using Main.Utils;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.GameLobby.Panels.TopRight
{
    public class MapListEntry : IDrawable
    {
        public static readonly Vector2f Size = new Vector2f(TopRightPanel.Size.X, 0.125f * TopRightPanel.Size.Y);
        public RectangleShape Shape { get; private init; }

        private static readonly uint _textSize = 24;

        private static readonly Vector2f IndexCellSize = new Vector2f(0.10f * TopRightPanel.Size.X, MapListEntry.Size.Y);
        private Text _indexCellText;
        private RectangleShape _indexCell;

        private static readonly Vector2f NameCellSize = new Vector2f(0.35f * TopRightPanel.Size.X, MapListEntry.Size.Y);
        private Text _nameCellText;
        private RectangleShape _nameCell;

        private static readonly Vector2f SizeCellSize = new Vector2f(0.10f * TopRightPanel.Size.X, MapListEntry.Size.Y);
        private Text _sizeCellText;
        private RectangleShape _sizeCell;

        private static readonly Vector2f PlayerCellSize = new Vector2f(0.10f * TopRightPanel.Size.X, MapListEntry.Size.Y);
        private Text _playersCellText;
        private RectangleShape _playersCell;

        public MapInfo MapInfo { get; private init; }
        private bool _selected;

        public MapListEntry(int positionOnList, MapInfo mapInfo)
        {
            this.MapInfo = mapInfo;

            this.Shape = new RectangleShape(Size);
            this.Shape.Position = TopRightPanel.Position + new Vector2f(0f, positionOnList * Size.Y);

            this.Shape.FillColor = Color.Black;
            this.Shape.OutlineThickness = 2f;
            this.Shape.OutlineColor = Color.Red;

            this.InitializeCellShape(ref this._indexCell!, MapListEntry.IndexCellSize, this.Shape.Position);
            this.InitializeCellShape(ref this._nameCell!, MapListEntry.NameCellSize, this._indexCell.Position + new Vector2f(this._indexCell.Size.X, 0.0f));
            this.InitializeCellShape(ref this._sizeCell!, MapListEntry.SizeCellSize, this._nameCell.Position + new Vector2f(this._nameCell.Size.X, 0.0f));
            this.InitializeCellShape(ref this._playersCell!, MapListEntry.PlayerCellSize, this._sizeCell.Position + new Vector2f(this._sizeCell.Size.X, 0.0f));

            this.InitializeCellText(ref this._indexCellText!, this.Shape.Position, positionOnList.ToString());
            this.InitializeCellText(ref this._nameCellText!, this._indexCell.Position + new Vector2f(this._indexCell.Size.X, 0.0f), mapInfo.Name!);
            this.InitializeCellText(ref this._sizeCellText!, this._nameCell.Position + new Vector2f(this._nameCell.Size.X, 0.0f), mapInfo.Size!);
            this.InitializeCellText(ref this._playersCellText!, this._sizeCell.Position + new Vector2f(this._sizeCell.Size.X, 0.0f), mapInfo.Players.ToString());
        }

        private void InitializeCellShape(ref RectangleShape cell, Vector2f size, Vector2f position)
        {
            cell = new RectangleShape(size);
            cell.Position = position;
            cell.FillColor = Color.Transparent;
            cell.OutlineThickness = 1f;
            cell.OutlineColor = Color.Red;
        }

        private void InitializeCellText(ref Text cellText, Vector2f position, string content)
        {
            cellText = new Text(content, GameSettings.Font, _textSize);
            cellText.Position = position;
            cellText.FillColor = Color.White;
        }

        public void Draw(RenderTarget drawer)
        {
            this.Shape.FillColor = this._selected
                ? new Color(66, 61, 60)
                : Color.Black;

            drawer.Draw(this.Shape);

            drawer.Draw(this._indexCell);
            drawer.Draw(this._indexCellText);
            drawer.Draw(this._nameCell);
            drawer.Draw(this._nameCellText);
            drawer.Draw(this._sizeCell);
            drawer.Draw(this._sizeCellText);
            drawer.Draw(this._playersCell);
            drawer.Draw(this._playersCellText);
        }

        public void Select()
        {
            this._selected = true;
        }

        public void Unselect()
        {
            this._selected = false;
        }
    }
}
