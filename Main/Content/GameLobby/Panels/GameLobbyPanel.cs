using Main.Content.Lobby;
using Main.Utils.Events;
using Main.Utils.Graphic;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.GameLobby.Panels
{
    public abstract class GameLobbyPanel :
        IDrawable,
        IEventHandler<MouseEvent>,
        IEventHandler<GameLobbyResultChanged>
    {
        public GameLobbyView View { get; init; }

        public readonly IGameLobbyContent _gameContent;
        public static readonly float Gap = 8f;

        public GameLobbyPanel(IGameLobbyContent gameContent) =>
            (_gameContent) = (gameContent);

        public abstract void Draw(RenderTarget drawer);
        public abstract void Handle(MouseEvent e);
        public abstract void Handle(GameLobbyResultChanged e);
    }
}
