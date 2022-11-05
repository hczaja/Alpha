using Main.Content.Game;
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
        public static readonly float MoveSpeed = 4.0f;
        public static readonly float ViewBandwith = 48.0f;

        public float MoveX { get; private set; }
        public float MoveY { get; private set; }
        
        public bool CanMove { get; private set; }

        private readonly Vector2f _workspacePosition;
        private readonly Vector2f _workspaceSize;

        public GameCamera(Vector2f position, Vector2f size) => (_workspacePosition, _workspaceSize) = (position, size);

        public void Move(Vector2f vector)
        {
            this.MoveX += vector.X;
            this.MoveY += vector.Y;
        }

        public void Handle(MouseEvent e)
        {
            if (e.Type == MouseEventType.ButtonPressed && e.Button == Mouse.Button.Middle) this.CanMove = true;
            else if (e.Type == MouseEventType.ButtonReleased && e.Button == Mouse.Button.Middle) this.CanMove = false;
            else if (e.Type == MouseEventType.MouseMoved)
            {
                if (!this.CanMove)
                {
                    this.MoveDirection = Direction.Unknown;
                    return;
                }

                float topEdge = _workspacePosition.Y;
                float bottomEdge = _workspacePosition.Y + _workspaceSize.Y;
                float leftEdge = _workspacePosition.X;
                float rightEdge = _workspacePosition.X + _workspaceSize.X;

                bool moveTop = topEdge <= e.Y && e.Y < topEdge + ViewBandwith;
                bool moveBottom = bottomEdge - ViewBandwith < e.Y && e.Y <= bottomEdge;
                bool moveLeft = leftEdge <= e.X && e.X < leftEdge + ViewBandwith;
                bool moveRight = rightEdge - ViewBandwith < e.X && e.X < rightEdge;

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
}
