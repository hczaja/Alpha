using Main.Utils;
using Main.Utils.Camera;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Content.Game.Panels
{
    internal class CentralView : GamePanelView
    {
        private readonly GameCamera _camera;

        public static readonly float GameViewBorderTolerance = 32.0f;
        public readonly (int columns, int rows) _gridDimensions;

        public CentralView(GameCamera camera, FloatRect viewRect, GridSize gridSize) : base(viewRect)
            => (_camera, _gridDimensions) = (camera, Grid.GetGridDimensions(gridSize));

        private bool CanMoveLeft(int playerId)
        {
            float cameraXLeft = this._viewRectangle.Left + this._camera.MoveX[playerId] - GameCamera.MoveSpeed;
            float borderXLeft = -GameViewBorderTolerance;
            return cameraXLeft >= borderXLeft;
        }

        private bool CanMoveRight(int playerId)
        {
            float cameraXRight = this._viewRectangle.Left + this._viewRectangle.Width + this._camera.MoveX[playerId] + GameCamera.MoveSpeed;
            float borderXRight = this._gridDimensions.columns * Cell._CellSizeX + GameViewBorderTolerance;
            return cameraXRight <= borderXRight;
        }

        private bool CanMoveTop(int playerId)
        {
            float cameraYTop = this._viewRectangle.Top + this._camera.MoveY[playerId] - GameCamera.MoveSpeed;
            float borderYTop = -GameViewBorderTolerance;
            return cameraYTop >= borderYTop;
        }

        private bool CanMoveBottom(int playerId)
        {
            float cameraYBottom = this._viewRectangle.Top + this._viewRectangle.Height + this._camera.MoveY[playerId] + GameCamera.MoveSpeed;
            float borderYBottom = this._gridDimensions.rows * Cell._CellSizeY + GameViewBorderTolerance;
            return cameraYBottom <= borderYBottom;
        }

        public override void Update(int playerId)
        {
            if (!this._camera.CanMove)
                return;

            switch (this._camera.MoveDirection)
            {
                case Direction.Unknown:
                    break;
                case Direction.TopLeft:
                    this.UpdateMoveView(playerId, this.CanMoveLeft(playerId), new Vector2f(-GameCamera.MoveSpeed, 0.0f));
                    this.UpdateMoveView(playerId, this.CanMoveTop(playerId), new Vector2f(0.0f, -GameCamera.MoveSpeed));
                    break;
                case Direction.TopRight:
                    this.UpdateMoveView(playerId, this.CanMoveRight(playerId), new Vector2f(+GameCamera.MoveSpeed, 0.0f));
                    this.UpdateMoveView(playerId, this.CanMoveTop(playerId), new Vector2f(0.0f, -GameCamera.MoveSpeed));
                    break;
                case Direction.BottomRight:
                    this.UpdateMoveView(playerId, this.CanMoveRight(playerId), new Vector2f(+GameCamera.MoveSpeed, 0.0f));
                    this.UpdateMoveView(playerId, this.CanMoveBottom(playerId), new Vector2f(0.0f, +GameCamera.MoveSpeed));
                    break;
                case Direction.BottomLeft:
                    this.UpdateMoveView(playerId, this.CanMoveLeft(playerId), new Vector2f(-GameCamera.MoveSpeed, 0.0f));
                    this.UpdateMoveView(playerId, this.CanMoveBottom(playerId), new Vector2f(0.0f, +GameCamera.MoveSpeed));
                    break;
                case Direction.Left:
                    this.UpdateMoveView(playerId, this.CanMoveLeft(playerId), new Vector2f(-GameCamera.MoveSpeed, 0.0f));
                    break;
                case Direction.Top:
                    this.UpdateMoveView(playerId, this.CanMoveTop(playerId), new Vector2f(0.0f, -GameCamera.MoveSpeed));
                    break;
                case Direction.Right:
                    this.UpdateMoveView(playerId, this.CanMoveRight(playerId), new Vector2f(+GameCamera.MoveSpeed, 0.0f));
                    break;
                case Direction.Bottom:
                    this.UpdateMoveView(playerId, this.CanMoveBottom(playerId), new Vector2f(0.0f, +GameCamera.MoveSpeed));
                    break;
            }
        }

        private void UpdateMoveView(int playerId, bool updateCondition, Vector2f updateVector)
        {
            if (updateCondition)
            {
                this._camera.Move(updateVector, playerId);
                this.Move(updateVector);
            }
        }

        public void ResetView(int playerId, int previousPlayerId)
        {
            this.Move(new Vector2f(-this._camera.MoveX[previousPlayerId], -this._camera.MoveY[previousPlayerId]));
            this.Move(new Vector2f(this._camera.MoveX[playerId], this._camera.MoveY[playerId]));
        }
    }
}
