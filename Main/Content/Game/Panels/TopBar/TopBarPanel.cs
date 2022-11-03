using Main.Content.Game.Panels.TopBar.Resources;
using Main.Content.Game.Turns;
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

namespace Main.Content.Game.Panels
{
    internal class TopBarPanel : GamePanel
    {
        public static readonly Vector2f Position = new Vector2f(0f, 0f);
        public static readonly Vector2f Size = new Vector2f(GameSettings.WindowWidth, 0.05f * GameSettings.WindowHeight);

        private readonly RectangleShape _panelShape;

        private GoldInfo GoldInfo;
        private static readonly Vector2f _goldInfoPosition = new Vector2f(0f, 0f);

        private readonly TexturedButton _nextTurnButton;
        private Text TurnInfoDay;
        private Text TurnInfoPlayer;

        private readonly float _wordSpacing = 16f;

        public TopBarPanel(IGameContent gameContent, ITurnManager turnManager) : base(gameContent, turnManager)
        {
            var rectangle = new FloatRect(Position, Size);
            this.View = new RightBarView(rectangle);

            this._panelShape = new RectangleShape(Size);
            this._panelShape.Position = Position;
            this._panelShape.FillColor = Color.Black;
            this._panelShape.OutlineColor = Color.Red;
            this._panelShape.OutlineThickness = 1.0f;

            var currentPlayer = _turnManager.GetCurrentPlayer();
            var income = currentPlayer.CalculateIncome();
            this.GoldInfo = new GoldInfo(currentPlayer.Supplies.Gold, income.Gold, _goldInfoPosition);

            const uint fontSize = 24;
            this._nextTurnButton = new NextTurnButton(this._panelShape);

            this.TurnInfoDay = new Text($"Day {ITurnManager.turnCounter}.", GameSettings.Font, fontSize);
            this.TurnInfoDay.Position = this.GetTurnInfoDayPosition();
            this.TurnInfoDay.FillColor = Color.White;
            this.TurnInfoDay.Style = Text.Styles.Bold;

            this.TurnInfoPlayer = new Text($"{currentPlayer.Faction.Type}.", GameSettings.Font, fontSize);
            this.TurnInfoPlayer.Position = this.GetTurnInfoFactionPosition();
            this.TurnInfoPlayer.FillColor = currentPlayer.Faction.GetFactionColor();
            this.TurnInfoPlayer.Style = Text.Styles.Bold;
        }

        public override void Draw(RenderTarget drawer)
        {
            drawer.SetView(this.View);
            drawer.Draw(this._panelShape);

            this.GoldInfo.Draw(drawer);
            drawer.Draw(this.TurnInfoDay);
            drawer.Draw(this.TurnInfoPlayer);
            this._nextTurnButton.Draw(drawer);
        }

        public override void Handle(MouseEvent e) 
        {
            if (e.Type == MouseEventType.ButtonPressed
                    && e.Button == Mouse.Button.Left)
            {
                if (MouseEvent.IsMouseEventRaisedIn(this._nextTurnButton.Rectangle.GetGlobalBounds(), e)) 
                {
                    this._gameContent.ProcessNextTurn();
                }
            }
        }

        public override void Handle(KeyboardEvent e) { }

        public override void Handle(NewTurnEvent e) 
        {
            var playerInfo = e.PlayerInfo;
            var income = playerInfo.CalculateIncome();

            this.GoldInfo = new GoldInfo(playerInfo.Supplies.Gold, income.Gold, _goldInfoPosition);

            this.TurnInfoDay.DisplayedString = $"Day {ITurnManager.turnCounter}.";
            this.TurnInfoDay.Position = this.GetTurnInfoDayPosition();

            this.TurnInfoPlayer.DisplayedString = $"{playerInfo.Faction.Type}.";
            this.TurnInfoPlayer.Position = this.GetTurnInfoFactionPosition();
            this.TurnInfoPlayer.FillColor = playerInfo.Faction.GetFactionColor();
        }

        private Vector2f GetTurnInfoDayPosition() => this._nextTurnButton.Rectangle.Position - new Vector2f(this.TurnInfoDay.GetLocalBounds().Width + _wordSpacing, 0f);
        private Vector2f GetTurnInfoFactionPosition() => this.TurnInfoDay.Position - new Vector2f(this.TurnInfoPlayer.GetLocalBounds().Width + _wordSpacing, 0f);

        public override void Update() { }
    }
}
