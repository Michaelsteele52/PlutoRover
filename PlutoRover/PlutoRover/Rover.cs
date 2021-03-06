﻿using System;
using System.Collections.Generic;

namespace PlutoRover
{
    public class Rover
    {
        public enum TurnInstructions
        {
            L,R
        }

        public enum MoveInstructions
        {
            F,B
        }
        public string CurrentDirection;
        public readonly List<string> Directions = new List<string>(){"N","E","S","W"};
        public int PosX;
        public int PosY;
        private bool _obstacle;
        public Rover(int posX, int posY)
        {
            CurrentDirection = Directions[0];
            PosX = posX;
            PosY = posY;
            _obstacle = false;
        }

        


        public string Instruct(string instruction, Pluto pluto)
        {
            if (Enum.IsDefined(typeof(TurnInstructions), instruction))
            {
                return Turn(instruction);
            }
            else if(Enum.IsDefined(typeof(MoveInstructions), instruction))
            {
                return Move(instruction, pluto);
            }
            return "Invalid Instruction";
        }

        public string ExecuteInstructions(string badInput, Pluto pluto)
        {
            var result = "";
            foreach (var c in badInput)
            {
                result = Instruct(c.ToString(), pluto);
                if (result == "Invalid Instruction")
                {
                    return result;
                }

                if (result == "Obstacle")
                {
                    return result;
                }
            }

            return result;
        }

        public string Turn(string instruction)
        {
            var change = 0;
            if (instruction == "L")
                change = -1;
            else if (instruction == "R")
                change = 1;

            var direction = Directions.IndexOf(CurrentDirection);
            CurrentDirection = Directions[(direction + change + Directions.Count) % Directions.Count];
            return "Turned";
        }

        public string Move(string instruction, Pluto pluto)
        {
            var displacement = 0;
            if (CurrentDirection == "N" || CurrentDirection == "S")
            {
                displacement = 1;
                if (CurrentDirection == "S")
                {
                    displacement = -1;
                }
                if (instruction == "B")
                {
                    displacement *= -1;
                }
                if(pluto.Grid[PosX][(PosY + displacement + pluto.YSize) % pluto.YSize].Obstacle)
                {
                    _obstacle = true;
                    return "Obstacle";
                }
                PosY = (PosY + displacement + pluto.YSize) % pluto.YSize;
            }

            if (CurrentDirection == "E" || CurrentDirection == "W")
            {
                displacement = 1;
                if (CurrentDirection == "W")
                {
                    displacement = -1;
                }

                if (instruction == "B")
                {
                    displacement *= -1;
                }

                if (pluto.Grid[(PosX + displacement + pluto.XSize) % pluto.XSize][PosY].Obstacle)
                {
                    _obstacle = true;
                    return "Obstacle";
                }

                PosX = (PosX + displacement + pluto.XSize) % pluto.XSize;
            }

            return "Successful Move";
        }

    }
}
