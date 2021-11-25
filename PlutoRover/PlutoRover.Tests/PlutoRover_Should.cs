using System;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using NUnit.Framework;
using PlutoRover.Instructions;
using PlutoRover.Strategies;

namespace PlutoRover.Tests
{
    [TestFixture]
    public class PlutoRover_Should
    {
        private Rover _rover;
        private Pluto _pluto;
        private readonly int XSize = 100;
        private readonly int YSize = 100;
        [SetUp]
        public void SetUp()
        {
            _rover = new Rover(0,0);
            _pluto = new Pluto(XSize, YSize);
        }

        [Test]
        public void RejectBadInstructions()
        {
            var result = RoverService.ExecuteInstructions(_rover,"BadInput", _pluto);
            result.Should().Be("Invalid Instruction");
        }

        [Test]
        public void NotCarryOutNextInstructionsIfBadInput()
        {
            var result = RoverService.ExecuteInstructions(_rover, "BadInput", _pluto);
            result.Should().Be("Invalid Instruction");
        }

        [TestCase(1, "E")]
        [TestCase(-1, "W")]
        [TestCase(2, "S")]
        [TestCase(-2, "S")]
        public void ChangeDirection(int directionChange, string expectedDirection)
        {
            _rover.ChangeDirection(directionChange);
            _rover.CurrentDirection.Should().Be(expectedDirection);
        }

        [TestCase(TurnInstructions.L,"W")]
        [TestCase(TurnInstructions.R, "E")]
        public void Turn(TurnInstructions input, string output)
        {
            TurnStrategy.Turn(_rover, input);
            _rover.CurrentDirection.Should().Be(output);
        }

        [TestCase(MoveInstructions.F, 1)]
        [TestCase(MoveInstructions.B, 99)]
        public void Move(MoveInstructions instruction, int position)
        {
            MovementStrategy.Move(_rover, instruction, _pluto);
            _rover.PosX.Should().Be(0);
            _rover.PosY.Should().Be(position);
        }

        [TestCase(TurnInstructions.R, MoveInstructions.F, 0, 1)]
        [TestCase(TurnInstructions.R, MoveInstructions.B, 0, 99)]
        [TestCase(TurnInstructions.L, MoveInstructions.F, 0, 99)]
        [TestCase(TurnInstructions.L, MoveInstructions.B, 0, 1)]
        public void TurnAndMove(TurnInstructions turnInstruction, MoveInstructions moveInstruction, int yPosition, int xPosition)
        {
            TurnStrategy.Turn(_rover, turnInstruction);
            MovementStrategy.Move(_rover, moveInstruction, _pluto);
            _rover.PosY.Should().Be(yPosition);
            _rover.PosX.Should().Be(xPosition);
        }

        [Test]
        public void ShouldWrapAround()
        {
            TurnStrategy.Turn(_rover, TurnInstructions.L);
            MovementStrategy.Move(_rover, MoveInstructions.F, _pluto);
            _rover.PosY.Should().Be(0);
            _rover.PosX.Should().Be(99);
        }

        [Test]
        public void StopIfObstacle()
        {
            _pluto.Grid[0][1].IsObstacle = true;
            var result = MovementStrategy.Move(_rover, MoveInstructions.F, _pluto);
            result.Should().Be("IsObstacle");
        }

        [Test]
        public void NotCarryOutNextInstructionIfObstacle()
        {
            _pluto.Grid[0][1].IsObstacle = true;
            var result = RoverService.ExecuteInstructions(_rover,"F", _pluto);
            result.Should().Be("IsObstacle");
        }
    }
}
