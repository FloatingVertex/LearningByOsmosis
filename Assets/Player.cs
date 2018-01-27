using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InControl;
using UnityEngine;

namespace Assets
{
    public class Player
    {
        public Player(InputDevice inputDevice, Color color)
        {
            Device = inputDevice;
            Color = color;
            Ready = true;
        }

        public InputDevice Device { get; private set; }
        public Color Color { get; private set; }

        public bool Ready { get; private set; }
		
		//TODO:  Add current book, add hitpoint books, add head size, add powerups, etc.
    }
}
