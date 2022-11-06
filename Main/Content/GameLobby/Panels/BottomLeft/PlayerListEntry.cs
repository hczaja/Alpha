using Main.Content.Common.MapManager;
using Main.Content.Game;
using Main.Content.Game.Factions;
using Main.Utils;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.GameLobby.Panels.BottomLeft
{
    public class PlayersListEntry : IDrawable, IEventHandler<MouseEvent>
    {
        public static readonly Vector2f Size = new Vector2f(BottomLeftPanel.Size.X, 0.125f * BottomLeftPanel.Size.Y);
        public RectangleShape Shape { get; private init; }

        private static readonly uint _textSize = 24;

        private static readonly Vector2f IndexCellSize = new Vector2f(0.10f * BottomLeftPanel.Size.X, PlayersListEntry.Size.Y);
        private Text _indexCellText;
        private RectangleShape _indexCell;

        private static readonly Vector2f NameCellSize = new Vector2f(0.25f * BottomLeftPanel.Size.X, PlayersListEntry.Size.Y);
        private Text _nameCellText;
        private RectangleShape _nameCell;

        private static readonly Vector2f FactionCellSize = new Vector2f(0.25f * BottomLeftPanel.Size.X, PlayersListEntry.Size.Y);
        private Text _factionCellText;
        private RectangleShape _factionCell;

        public PlayerInfo PlayerInfo { get; private init; }
        private bool _selected;

        public PlayersListEntry(int positionOnList, PlayerInfo playerInfo)
        {
            this.PlayerInfo = playerInfo;

            this.Shape = new RectangleShape(Size);
            this.Shape.Position = BottomLeftPanel.Position + new Vector2f(0f, positionOnList * Size.Y);

            this.Shape.FillColor = Color.Transparent;
            this.Shape.OutlineThickness = 1f;
            this.Shape.OutlineColor = Color.Red;

            this.InitializeCellShape(ref this._indexCell!, PlayersListEntry.IndexCellSize, this.Shape.Position);
            this.InitializeCellShape(ref this._nameCell!, PlayersListEntry.NameCellSize, this._indexCell.Position + new Vector2f(this._indexCell.Size.X, 0.0f));
            this.InitializeCellShape(ref this._factionCell!, PlayersListEntry.FactionCellSize, this._nameCell.Position + new Vector2f(this._nameCell.Size.X, 0.0f));

            var factionColor = playerInfo.Faction == "Faction"
                ? Color.White
                : Faction.FactionToColor(Enum.Parse<FactionType>(playerInfo.Faction));

            this.InitializeCellText(ref this._indexCellText!, this.Shape.Position, playerInfo.Index, Color.White);
            this.InitializeCellText(ref this._nameCellText!, this._indexCell.Position + new Vector2f(this._indexCell.Size.X, 0.0f), playerInfo.Name, Color.White);
            this.InitializeCellText(ref this._factionCellText!, this._nameCell.Position + new Vector2f(this._nameCell.Size.X, 0.0f), playerInfo.Faction, factionColor);
        }

        private void InitializeCellText(ref Text cellText, Vector2f position, string content, Color color)
        {
            cellText = new Text(content, GameSettings.Font, _textSize);
            cellText.Position = position;
            cellText.FillColor = color;
        }

        private void InitializeCellShape(ref RectangleShape cell, Vector2f size, Vector2f position)
        {
            cell = new RectangleShape(size);
            cell.Position = position;
            cell.FillColor = Color.Transparent;
            cell.OutlineThickness = 1f;
            cell.OutlineColor = Color.Red;
        }

        public void Draw(RenderTarget drawer)
        {
            this.Shape.FillColor = this._selected
                ? new Color(66, 61, 60)
                : Color.Black;

            this._nameCellText.DisplayedString = this._selected
                ? "Player"
                : "<Empty>";

            drawer.Draw(this.Shape);

            drawer.Draw(this._indexCell);
            drawer.Draw(this._indexCellText);
            drawer.Draw(this._nameCell);
            drawer.Draw(this._nameCellText);
            drawer.Draw(this._factionCell);
            drawer.Draw(this._factionCellText);
        }

        public void Handle(MouseEvent e)
        {

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
