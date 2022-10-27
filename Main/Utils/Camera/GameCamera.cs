using Main.Utils.Events;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Camera
{
    internal class GameCamera
        : IEventHandler<MouseEvent>
    {
        public GameCameraMoveDirection MoveDirection { get; private set; }
        public static readonly float MoveSpeed = 8.0f;
        public static readonly float ViewBandwith = 64.0f;

        public float MoveX { get; set; } = 0.0f;
        public float MoveY { get; set; } = 0.0f;

        private float workspaceWidth;
        private float workspaceHeight;

        public GameCamera(float width, float height) => (workspaceWidth, workspaceHeight) = (width, height);

        public void Move(Vector2f vector)
        {
            this.MoveX += vector.X;
            this.MoveY += vector.Y;
        }

        public void Handle(MouseEvent e)
        {
            bool moveTop =
                   0.0f <= e.Y
                && e.Y < 0.0f + ViewBandwith;

            bool moveBottom = true
                && workspaceHeight - ViewBandwith <= e.Y
                && e.Y < workspaceHeight;

            bool moveLeft = true
                && 0.0f <= e.X
                && e.X < 0.0f+ ViewBandwith;

            bool moveRight = true
                && workspaceWidth - ViewBandwith <= e.X
                && e.X < workspaceWidth;

            if (!moveTop && !moveBottom && !moveLeft && !moveRight)
            {
                this.MoveDirection = GameCameraMoveDirection.NoMove;
                return;
            }

            if (moveTop && moveLeft) this.MoveDirection = GameCameraMoveDirection.TopLeft;
            else if (moveTop && moveRight) this.MoveDirection = GameCameraMoveDirection.TopRight;
            else if (moveBottom && moveLeft) this.MoveDirection = GameCameraMoveDirection.BottomLeft;
            else if (moveBottom && moveRight) this.MoveDirection = GameCameraMoveDirection.BottomRight;
            else if (moveLeft) this.MoveDirection = GameCameraMoveDirection.Left;
            else if (moveRight) this.MoveDirection = GameCameraMoveDirection.Right;
            else if (moveTop) this.MoveDirection = GameCameraMoveDirection.Top;
            else if (moveBottom) this.MoveDirection = GameCameraMoveDirection.Bottom;
        }
    }
}
