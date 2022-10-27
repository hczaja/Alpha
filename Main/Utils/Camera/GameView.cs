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
            if (!this._camera.CanMove)
                return;

            switch (this._camera.MoveDirection)
            {
                case GameCameraMoveDirection.NoMove:
                    break;
                case GameCameraMoveDirection.TopLeft:
                    this.UpdateMoveView(this.CanMoveLeft(), new Vector2f(-GameCamera.MoveSpeed, 0.0f));
                    this.UpdateMoveView(this.CanMoveTop(), new Vector2f(0.0f, -GameCamera.MoveSpeed));
                    break;
                case GameCameraMoveDirection.TopRight:
                    this.UpdateMoveView(this.CanMoveRight(), new Vector2f(+GameCamera.MoveSpeed, 0.0f));
                    this.UpdateMoveView(this.CanMoveTop(), new Vector2f(0.0f, -GameCamera.MoveSpeed));
                    break;
                case GameCameraMoveDirection.BottomRight:
                    this.UpdateMoveView(this.CanMoveRight(), new Vector2f(+GameCamera.MoveSpeed, 0.0f));
                    this.UpdateMoveView(this.CanMoveBottom(), new Vector2f(0.0f, +GameCamera.MoveSpeed));
                    break;
                case GameCameraMoveDirection.BottomLeft:
                    this.UpdateMoveView(this.CanMoveLeft(), new Vector2f(-GameCamera.MoveSpeed, 0.0f));
                    this.UpdateMoveView(this.CanMoveBottom(), new Vector2f(0.0f, +GameCamera.MoveSpeed));
                    break;
                case GameCameraMoveDirection.Left:
                    this.UpdateMoveView(this.CanMoveLeft(), new Vector2f(-GameCamera.MoveSpeed, 0.0f));
                    break;
                case GameCameraMoveDirection.Top:
                    this.UpdateMoveView(this.CanMoveTop(), new Vector2f(0.0f, -GameCamera.MoveSpeed));
                    break;
                case GameCameraMoveDirection.Right:
                    this.UpdateMoveView(this.CanMoveRight(), new Vector2f(+GameCamera.MoveSpeed, 0.0f));
                    break;
                case GameCameraMoveDirection.Bottom:
                    this.UpdateMoveView(this.CanMoveBottom(), new Vector2f(0.0f, +GameCamera.MoveSpeed));
                    break;
            }

            // scroll
        }

        private void UpdateMoveView(bool updateCondition, Vector2f updateVector)
        {
            if (updateCondition)
            {
                this._camera.Move(updateVector);
                this.Move(updateVector);
            }
        }
    }
}
