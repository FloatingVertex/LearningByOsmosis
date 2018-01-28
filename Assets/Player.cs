using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InControl;
using UnityEngine;

namespace Assets
{
    public enum PlayerColor
    {
        Red,
        Green,
        Blue,
        Yellow,
    }

    public class Player
    {
        private readonly InputDevice _device;

		public bool[] activeEffects;

        public RigidbodyController controller;

        public Player(InputDevice inputDevice, Color color,PlayerColor playerColor)
        {
            _device = inputDevice;
            Color = color;
            PlayerColor = playerColor;
            Ready = true;
			activeEffects = new bool[6];
        }

        public InputDevice Device
        {
            get { return _device; }
        }

        public Color Color { get; private set; }

        public PlayerColor PlayerColor { get; private set; }

        public bool Ready { get; private set; }
		
		//TODO:  Add current book, add hitpoint books, add head size, add powerups, etc.
		public bool isAffected(BookBehavior.KnowledgeType kt){
			return activeEffects [(int)kt];
		}
		public void SetEffect(BookBehavior.KnowledgeType kt){
			activeEffects [(int)kt] = true;
		}
    }
}
