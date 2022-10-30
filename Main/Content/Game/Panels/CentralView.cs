﻿using Main.Utils;
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

        public CentralView(GameCamera camera, FloatRect viewRect) : base(viewRect)
            => (_camera) = (camera);

        private bool CanMoveLeft() => this._camera.MoveX + GameViewBorderTolerance - GameCamera.MoveSpeed >= 0.0f;
        private bool CanMoveTop() => this._camera.MoveY + GameViewBorderTolerance - GameCamera.MoveSpeed >= 0.0f;
        private bool CanMoveRight() => this._camera.MoveX - GameViewBorderTolerance + GameCamera.MoveSpeed <= 0.0f + _viewRectangle.Width; 
        private bool CanMoveBottom() => this._camera.MoveY - GameViewBorderTolerance + GameCamera.MoveSpeed <= 0.0f + _viewRectangle.Height;
    
        public override void Update()
        {
            if (!this._camera.CanMove)
                return;

            switch (this._camera.MoveDirection)
            {
                case Utils.Camera.Direction.Unknown:
                    break;
                case Utils.Camera.Direction.TopLeft:
                    this.UpdateMoveView(this.CanMoveLeft(), new Vector2f(-GameCamera.MoveSpeed, 0.0f));
                    this.UpdateMoveView(this.CanMoveTop(), new Vector2f(0.0f, -GameCamera.MoveSpeed));
                    break;
                case Utils.Camera.Direction.TopRight:
                    this.UpdateMoveView(this.CanMoveRight(), new Vector2f(+GameCamera.MoveSpeed, 0.0f));
                    this.UpdateMoveView(this.CanMoveTop(), new Vector2f(0.0f, -GameCamera.MoveSpeed));
                    break;
                case Utils.Camera.Direction.BottomRight:
                    this.UpdateMoveView(this.CanMoveRight(), new Vector2f(+GameCamera.MoveSpeed, 0.0f));
                    this.UpdateMoveView(this.CanMoveBottom(), new Vector2f(0.0f, +GameCamera.MoveSpeed));
                    break;
                case Utils.Camera.Direction.BottomLeft:
                    this.UpdateMoveView(this.CanMoveLeft(), new Vector2f(-GameCamera.MoveSpeed, 0.0f));
                    this.UpdateMoveView(this.CanMoveBottom(), new Vector2f(0.0f, +GameCamera.MoveSpeed));
                    break;
                case Utils.Camera.Direction.Left:
                    this.UpdateMoveView(this.CanMoveLeft(), new Vector2f(-GameCamera.MoveSpeed, 0.0f));
                    break;
                case Utils.Camera.Direction.Top:
                    this.UpdateMoveView(this.CanMoveTop(), new Vector2f(0.0f, -GameCamera.MoveSpeed));
                    break;
                case Utils.Camera.Direction.Right:
                    this.UpdateMoveView(this.CanMoveRight(), new Vector2f(+GameCamera.MoveSpeed, 0.0f));
                    break;
                case Utils.Camera.Direction.Bottom:
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
