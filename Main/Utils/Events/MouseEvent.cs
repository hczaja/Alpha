using SFML.Window;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    public enum MouseEventType
    {
        Unknown, MouseMoved, ButtonPressed, ButtonReleased
    }

    public record MouseEvent
    {
        public float X { get; init; }
        public float Y { get; init; }
        public Mouse.Button Button { get; init; }
        public MouseEventType Type { get; init; }

        public MouseEvent(MouseEventType type, float x, float y, Mouse.Button button) => 
            (Type, X, Y, Button) = (type, x, y, button);

        public static bool IsMouseEventRaisedIn(FloatRect rect, MouseEvent e) =>
            rect.Left < e.X && e.X < rect.Left + rect.Width &&
            rect.Top < e.Y && e.Y < rect.Top + rect.Height;
    }
}
