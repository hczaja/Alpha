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
        public static readonly float MoveSpeed = 2.0f;
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
            //if (e.Type != MouseEventType.Move)
            //{
            //    this.MoveDirection = GameCameraMoveDirection.NoMove;
            //    return;
            //}

            bool moveTop =
                   0.0f + this.MoveY <= e.Y
                && e.Y < 0.0f + this.MoveY + ViewBandwith;

            bool moveBottom = true
                && workspaceHeight + this.MoveY - ViewBandwith <= e.Y
                && e.Y < workspaceHeight + this.MoveY;

            bool moveLeft = true
                && 0.0f + this.MoveX <= e.X
                && e.X < 0.0f + this.MoveX + ViewBandwith;

            bool moveRight = true
                && workspaceWidth + this.MoveX - ViewBandwith <= e.X
                && e.X < workspaceWidth + this.MoveX;

            if (!moveTop && !moveBottom && !moveLeft && !moveRight)
            {
                this.MoveDirection = GameCameraMoveDirection.NoMove;
                return;
            }

            if (moveTop && moveLeft) this.MoveDirection = GameCameraMoveDirection.TopLeft;
            if (moveTop && moveRight) this.MoveDirection = GameCameraMoveDirection.TopRight;
            if (moveBottom && moveLeft) this.MoveDirection = GameCameraMoveDirection.BottomLeft;
            if (moveBottom && moveRight) this.MoveDirection = GameCameraMoveDirection.BottomRight;
            if (moveLeft) this.MoveDirection = GameCameraMoveDirection.Left;
            if (moveRight) this.MoveDirection = GameCameraMoveDirection.Right;
            if (moveTop) this.MoveDirection = GameCameraMoveDirection.Top;
            if (moveBottom) this.MoveDirection = GameCameraMoveDirection.Bottom;
        }
    }
}
