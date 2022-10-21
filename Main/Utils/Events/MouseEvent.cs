using SFML.Window;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    internal enum MouseEventType
    {
        Unknown, Move, ButtonPressed, ButtonReleased
    }

    internal record MouseEvent
    {
        public float X { get; init; }
        public float Y { get; init; }
        public Mouse.Button Button { get; init; }
        public MouseEventType Type { get; init; }

        public MouseEvent(MouseEventType _type, float _x, float _y, Mouse.Button _button) => 
            (Type, X, Y, Button) = (_type, _x, _y, _button);

        public static bool IsMouseEventRaisedIn(FloatRect rect, MouseEvent e) =>
            rect.Left < e.X && e.X < rect.Left + rect.Width &&
            rect.Top < e.Y && e.Y < rect.Top + rect.Height;
    }
}
