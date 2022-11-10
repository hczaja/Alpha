using Main.Content.Common;
using Main.Content.Game.Factions;
using Main.Content.Lobby;
using Main.Utils;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
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

        private static readonly Vector2f TypeCellSize = new Vector2f(0.10f * BottomLeftPanel.Size.X, PlayersListEntry.Size.Y);
        private Text _typeCellText;
        private RectangleShape _typeCell;

        private static readonly Vector2f FactionCellSize = new Vector2f(0.25f * BottomLeftPanel.Size.X, PlayersListEntry.Size.Y);
        private Text _factionCellText;
        private RectangleShape _factionCell;

        private static readonly Vector2f TeamCellSize = new Vector2f(0.10f * BottomLeftPanel.Size.X, PlayersListEntry.Size.Y);
        private Text _teamCellText;
        private RectangleShape _teamCell;

        public PlayerInfo PlayerInfo { get; private init; }
        private readonly IPlayerManager _playerManager;
        private readonly IGameLobbyContent _gameLobbyContent;

        public PlayersListEntry(int positionOnList, PlayerInfo playerInfo, IPlayerManager manager, IGameLobbyContent gameLobbyContent)
        {
            this.PlayerInfo = playerInfo;
            this._playerManager = manager;
            this._gameLobbyContent = gameLobbyContent;

            this.Shape = new RectangleShape(Size);
            this.Shape.Position = BottomLeftPanel.Position + new Vector2f(0f, positionOnList * Size.Y);

            this.Shape.FillColor = Color.Transparent;
            this.Shape.OutlineThickness = 1f;
            this.Shape.OutlineColor = Color.Red;

            this.InitializeCellShape(ref this._indexCell!, PlayersListEntry.IndexCellSize, this.Shape.Position);
            this.InitializeCellShape(ref this._nameCell!, PlayersListEntry.NameCellSize, this._indexCell.Position + new Vector2f(this._indexCell.Size.X, 0.0f));
            this.InitializeCellShape(ref this._typeCell!, PlayersListEntry.TypeCellSize, this._nameCell.Position + new Vector2f(this._nameCell.Size.X, 0.0f));
            this.InitializeCellShape(ref this._factionCell!, PlayersListEntry.FactionCellSize, this._typeCell.Position + new Vector2f(this._typeCell.Size.X, 0.0f));
            this.InitializeCellShape(ref this._teamCell!, PlayersListEntry.TeamCellSize, this._factionCell.Position + new Vector2f(this._factionCell.Size.X, 0.0f));

            var factionColor = playerInfo.Faction == "Faction"
                ? Color.White
                : FactionTypeExtensions.FactionToColor(Enum.Parse<FactionType>(playerInfo.Faction));

            this.InitializeCellText(ref this._indexCellText!, this.Shape.Position, playerInfo.Index, Color.White);
            this.InitializeCellText(ref this._nameCellText!, this._indexCell.Position + new Vector2f(this._indexCell.Size.X, 0.0f), playerInfo.Name, Color.White);
            this.InitializeCellText(ref this._typeCellText!, this._nameCell.Position + new Vector2f(this._nameCell.Size.X, 0.0f), playerInfo.Type, Color.White);
            this.InitializeCellText(ref this._factionCellText!, this._typeCellText.Position + new Vector2f(this._typeCell.Size.X, 0.0f), playerInfo.Faction, factionColor);
            this.InitializeCellText(ref this._teamCellText!, this._factionCellText.Position + new Vector2f(this._factionCell.Size.X, 0.0f), playerInfo.Team, Color.White);
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

        private bool IsEmpty() => this._typeCellText.DisplayedString == PlayerType.Empty.ToString();

        public void Draw(RenderTarget drawer)
        {
            drawer.Draw(this.Shape);

            drawer.Draw(this._indexCell);
            if (!this.IsEmpty()) drawer.Draw(this._indexCellText);
            drawer.Draw(this._nameCell);
            if (!this.IsEmpty()) drawer.Draw(this._nameCellText);
            drawer.Draw(this._typeCell);
            drawer.Draw(this._typeCellText);
            drawer.Draw(this._factionCell);
            if (!this.IsEmpty()) drawer.Draw(this._factionCellText);
            drawer.Draw(this._teamCell);
            if (!this.IsEmpty()) drawer.Draw(this._teamCellText);
        }

        public void Handle(MouseEvent e)
        {
            if (e.Type == MouseEventType.ButtonPressed && e.Button == Mouse.Button.Right)
            {
                if (MouseEvent.IsMouseEventRaisedIn(this._typeCell.GetGlobalBounds(), e)) 
                {
                    string type_asString = this._typeCellText.DisplayedString;
                    var type = Enum.Parse<PlayerType>(type_asString);

                    var nextType = PlayerTypeExtensions.GetNextPlayerType(type);

                    this.PlayerInfo.Type = nextType.ToString();
                    this._typeCellText.DisplayedString = this.PlayerInfo.Type;

                    this._gameLobbyContent.Handle(new GameLobbyResultPlayersChanged(this._playerManager));
                }
                else if (!this.IsEmpty() && MouseEvent.IsMouseEventRaisedIn(this._factionCell.GetGlobalBounds(), e))
                {
                    string type_asString = this._factionCellText.DisplayedString;
                    var type = Enum.Parse<FactionType>(type_asString);

                    var nextType = FactionTypeExtensions.GetNextFactionType(type);

                    this.PlayerInfo.Faction = nextType.ToString();
                    this._factionCellText.DisplayedString = this.PlayerInfo.Faction;
                    this._factionCellText.FillColor = FactionTypeExtensions.FactionToColor(Enum.Parse<FactionType>(this.PlayerInfo.Faction));

                    this._gameLobbyContent.Handle(new GameLobbyResultPlayersChanged(this._playerManager));
                }
                else if (!this.IsEmpty() && MouseEvent.IsMouseEventRaisedIn(this._teamCell.GetGlobalBounds(), e))
                {
                    int amountOfPlayers = this._playerManager.Players.Length;

                    int teamNumber = int.Parse(this.PlayerInfo.Team);
                    int nextTeamNumber = teamNumber + 1;

                    if (nextTeamNumber > amountOfPlayers) nextTeamNumber = 1;

                    this.PlayerInfo.Team = nextTeamNumber.ToString();
                    this._teamCellText.DisplayedString = nextTeamNumber.ToString();
                }
            }
        }
    }
}
