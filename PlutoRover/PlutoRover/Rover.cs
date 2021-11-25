using System;
using System.Collections.Generic;

namespace PlutoRover
{
    public class Rover
    {
        public string CurrentDirection { get; private set; }
        private readonly List<string> _directions = new List<string>(){"N","E","S","W"};
        public int PosX { get; set; }
        public int PosY { get; set; }
        public Rover(int posX, int posY)
        {
            CurrentDirection = _directions[0];
            PosX = posX;
            PosY = posY;
        }

        public void ChangeDirection(int change)
        {
            var direction = _directions.IndexOf(CurrentDirection);
            CurrentDirection = _directions[(direction + change + _directions.Count) % _directions.Count];
        }
    }
}
