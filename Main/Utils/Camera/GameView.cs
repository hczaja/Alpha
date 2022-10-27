using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Camera
{
    internal class GameView : View
    {
        private readonly GameCamera _camera;

        public static readonly float GameViewBorderTolerance = 32.0f;

        public GameView(GameCamera camera) : base(
            new FloatRect(
                new Vector2f(0.0f, 0.0f),
                new Vector2f(GameSettings.WindowWidth, GameSettings.WindowHeight)))
            => (_camera) = (camera);

        private bool CanMoveLeft() => this._camera.MoveX + GameViewBorderTolerance - GameCamera.MoveSpeed >= 0.0f;
        private bool CanMoveTop() => this._camera.MoveY + GameViewBorderTolerance - GameCamera.MoveSpeed >= 0.0f;
        private bool CanMoveRight() => this._camera.MoveX - GameViewBorderTolerance + GameCamera.MoveSpeed <= 6000.0f; 
        private bool CanMoveBottom() => this._camera.MoveY - GameViewBorderTolerance + GameCamera.MoveSpeed <= 6000.0f;
    
        public void Update()
        {
            switch (this._camera.MoveDirection)
            {
                case GameCameraMoveDirection.TopLeft:
                    this.UpdateView(this.CanMoveLeft(), new Vector2f(-GameCamera.MoveSpeed, 0.0f));
                    this.UpdateView(this.CanMoveTop(), new Vector2f(0.0f, -GameCamera.MoveSpeed));
                    break;
                case GameCameraMoveDirection.TopRight:
                    this.UpdateView(this.CanMoveRight(), new Vector2f(+GameCamera.MoveSpeed, 0.0f));
                    this.UpdateView(this.CanMoveTop(), new Vector2f(0.0f, -GameCamera.MoveSpeed));
                    break;
                case GameCameraMoveDirection.BottomRight:
                    this.UpdateView(this.CanMoveRight(), new Vector2f(+GameCamera.MoveSpeed, 0.0f));
                    this.UpdateView(this.CanMoveBottom(), new Vector2f(0.0f, +GameCamera.MoveSpeed));
                    break;
                case GameCameraMoveDirection.BottomLeft:
                    this.UpdateView(this.CanMoveLeft(), new Vector2f(-GameCamera.MoveSpeed, 0.0f));
                    this.UpdateView(this.CanMoveBottom(), new Vector2f(0.0f, +GameCamera.MoveSpeed));
                    break;
                case GameCameraMoveDirection.Left:
                    this.UpdateView(this.CanMoveLeft(), new Vector2f(-GameCamera.MoveSpeed, 0.0f));
                    break;
                case GameCameraMoveDirection.Top:
                    this.UpdateView(this.CanMoveTop(), new Vector2f(0.0f, -GameCamera.MoveSpeed));
                    break;
                case GameCameraMoveDirection.Right:
                    this.UpdateView(this.CanMoveRight(), new Vector2f(+GameCamera.MoveSpeed, 0.0f));
                    break;
                case GameCameraMoveDirection.Bottom:
                    this.UpdateView(this.CanMoveBottom(), new Vector2f(0.0f, +GameCamera.MoveSpeed));
                    break;
            }
        }

        private void UpdateView(bool updateCondition, Vector2f updateVector)
        {
            if (updateCondition)
            {
                this._camera.Move(updateVector);
                this.Move(updateVector);
            }
        }
    }
}
