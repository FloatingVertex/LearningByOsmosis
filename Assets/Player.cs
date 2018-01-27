using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InControl;

namespace Assets
{
    class Player
    {
        public Player(InputDevice inputDevice)
        {
            Device = inputDevice;
			Ready = true;
        }

        public InputDevice Device { get; set; }

		public bool Ready { get; set; }
		
		//TODO:  Add current book, add hitpoint books, add head size, add powerups, etc.
    }
}
