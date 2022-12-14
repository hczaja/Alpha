using Main.Content.Common;
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

namespace Main.Content.GameLobby.Notifications
{
    public class ProblemWithStartGameNotification : Notification
    {
        private static readonly Vector2f _size = new Vector2f(0.5f * GameSettings.WindowWidth, 0.2f * GameSettings.WindowHeight);
        private static readonly Vector2f _position = new Vector2f(GameSettings.WindowWidth / 2f - 0.5f * _size.X, GameSettings.WindowHeight / 2f - 0.5f * _size.Y);

        private readonly Text _content;
        private readonly ProblemWithStartGameNotificationOKButton _button;

        private readonly INotificationService _notificationService;

        public ProblemWithStartGameNotification(INotificationService service, string content) : base(
            new RectangleShape(_size),
            new Text($"Problem!", GameSettings.Font, _titleFontSize),
            drawBackground: false)
        {
            this._notificationService = service;

            this._background.Position = _position;
            this._background.FillColor = Color.Black;
            this._background.OutlineThickness = 1.0f;
            this._background.OutlineColor = Color.White;

            this._title.Position = _position + new Vector2f(_fontSpacing, _fontSpacing);
            this._title.FillColor = Color.White;
            this._title.Style = Text.Styles.Bold;

            this._content = new Text($"{content}", GameSettings.Font, _contentFontSize);
            this._content.Position = this._title.Position + new Vector2f(0.0f, _titleFontSize + _fontSpacing);
            this._content.FillColor = Color.White;
            this._content.Style = Text.Styles.Bold;

            this._button = new ProblemWithStartGameNotificationOKButton();
        }

        public override void Draw(RenderTarget drawer)
        {
            base.Draw(drawer);
            drawer.Draw(this._content);
            this._button.Draw(drawer);
        }

        public override void Handle(MouseEvent e)
        {
            if (e.Type == MouseEventType.ButtonPressed
                && e.Button == Mouse.Button.Left)
            {
                if (MouseEvent.IsMouseEventRaisedIn(this._button.Rectangle.GetGlobalBounds(), e))
                {
                    this._notificationService.DequeueNotification(-1);
                }
            }
        }

        private sealed class ProblemWithStartGameNotificationOKButton : TextButton
        {
            private static Vector2f _size = ProblemWithStartGameNotification._size - new Vector2f(0f, 0.15f * GameSettings.WindowHeight);
            private static Vector2f _position = ProblemWithStartGameNotification._position + new Vector2f(0.0f, 0.15f * GameSettings.WindowHeight);

            public ProblemWithStartGameNotificationOKButton()
                : base("OK", _size, _position, "OK")
                { }
        }
    }
}
