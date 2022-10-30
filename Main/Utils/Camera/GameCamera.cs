using Main.Utils.Events;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Camera
{
    internal class GameCamera : IEventHandler<MouseEvent>
    {
        public Direction MoveDirection { get; private set; }
        public static readonly float MoveSpeed = 2.0f;
        public static readonly float ViewBandwith = 64.0f;

        public float MoveX { get; private set; }
        public float MoveY { get; private set; }
        
        public bool CanMove { get; private set; }

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
            if (e.Type == MouseEventType.ButtonPressed && e.Button == Mouse.Button.Middle) this.CanMove = true;
            else if (e.Type == MouseEventType.ButtonReleased && e.Button == Mouse.Button.Middle) this.CanMove = false;

            if (!this.CanMove)
            {
                this.MoveDirection = Direction.Unknown;
                return;
            }

            bool moveTop = 0.0f <= e.Y && e.Y < 0.0f + ViewBandwith;
            bool moveBottom = workspaceHeight - ViewBandwith <= e.Y && e.Y < workspaceHeight;
            bool moveLeft = 0.0f <= e.X && e.X < 0.0f + ViewBandwith;
            bool moveRight = workspaceWidth - ViewBandwith <= e.X && e.X < workspaceWidth;

            if (!moveTop && !moveBottom && !moveLeft && !moveRight)
            {
                this.MoveDirection = Direction.Unknown;
                return;
            }

            if (moveTop && moveLeft) this.MoveDirection = Direction.TopLeft;
            else if (moveTop && moveRight) this.MoveDirection = Direction.TopRight;
            else if (moveBottom && moveLeft) this.MoveDirection = Direction.BottomLeft;
            else if (moveBottom && moveRight) this.MoveDirection = Direction.BottomRight;
            else if (moveLeft) this.MoveDirection = Direction.Left;
            else if (moveRight) this.MoveDirection = Direction.Right;
            else if (moveTop) this.MoveDirection = Direction.Top;
            else if (moveBottom) this.MoveDirection = Direction.Bottom;
        }
    }
}
