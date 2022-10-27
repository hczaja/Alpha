﻿using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    internal enum KeyboardEventType
    {
        Unknown, KeyPressed, KeyReleased
    }

    internal record KeyboardEvent
    {
        public Keyboard.Key Key { get; init; }
        public KeyboardEventType Type { get; init; }

        public KeyboardEvent(KeyboardEventType type, Keyboard.Key key) =>
            (Type, Key) = (type, key);
    }
}