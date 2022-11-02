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

namespace Main.Content.Game.Notifications
{
    public class NewTurnNotification : Notification
    {
        private static readonly Vector2f _size = new Vector2f(0.2f * GameSettings.WindowWidth, 0.2f * GameSettings.WindowHeight);
        private static readonly Vector2f _position = new Vector2f(GameSettings.WindowWidth / 2f - 0.5f * _size.X, GameSettings.WindowHeight / 2f - 0.5f * _size.Y);

        private readonly Text _content;
        private readonly Text _playerDescription;
        private readonly NewTurnNotificationOKButton _button;

        private readonly INotificationService _notificationService;
        private readonly Player _playerInfo;

        public NewTurnNotification(INotificationService service, Player info) : base(
            new RectangleShape(_size),
            new Text($"New Turn!", GameSettings.Font, _titleFontSize),
            drawBackground: true)
        {
            this._notificationService = service;
            this._playerInfo = info;

            this._background.Position = _position;
            this._background.FillColor = Color.Black;
            this._background.OutlineThickness = 1.0f;
            this._background.OutlineColor = Color.White;

            this._title.Position = _position + new Vector2f(_fontSpacing, _fontSpacing);
            this._title.FillColor = Color.White;
            this._title.Style = Text.Styles.Bold;

            this._content = new Text($"Day {ITurnManager.turnCounter}.", GameSettings.Font, _contentFontSize);
            this._content.Position = this._title.Position + new Vector2f(0.0f, _titleFontSize + _fontSpacing);
            this._content.FillColor = Color.White;
            this._content.Style = Text.Styles.Bold;

            this._playerDescription = new Text($"{this._playerInfo.Faction.Type}", GameSettings.Font, _contentFontSize);
            this._playerDescription.Position = this._title.Position + new Vector2f(this._content.GetLocalBounds().Width + _fontSpacing, _titleFontSize + _fontSpacing);
            this._playerDescription.FillColor = this._playerInfo.Faction.GetFactionColor();
            this._playerDescription.Style = Text.Styles.Bold;

            this._button = new NewTurnNotificationOKButton();
        }

        public override void Draw(RenderTarget drawer)
        {
            base.Draw(drawer);
            drawer.Draw(this._content);
            drawer.Draw(this._playerDescription);
            this._button.Draw(drawer);
        }

        public override void Handle(MouseEvent e)
        {
            if (e.Type == MouseEventType.ButtonPressed
                && e.Button == Mouse.Button.Left)
            {
                if (MouseEvent.IsMouseEventRaisedIn(this._button.Rectangle.GetGlobalBounds(), e))
                {
                    this._notificationService.DequeueNotification(this._playerInfo.ID);
                }
            }
        }

        private sealed class NewTurnNotificationOKButton : TextButton
        {
            private static Vector2f _size = NewTurnNotification._size - new Vector2f(0f, 0.15f * GameSettings.WindowHeight);
            private static Vector2f _position = NewTurnNotification._position + new Vector2f(0.0f, 0.15f * GameSettings.WindowHeight);

            public NewTurnNotificationOKButton()
                : base("OK", _size, _position, "OK")
                { }
        }
    }
}
