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
        private TurnOperation _turnOperation;
        private MovementOperation _movementOperation;
        private IRoverService _service;

        [SetUp]
        public void SetUp()
        {
            _rover = new Rover(0,0);
            _pluto = new Pluto(XSize, YSize);
            _turnOperation = new TurnOperation();
            _movementOperation = new MovementOperation();
            _service = new RoverService();
        }

        [Test]
        public void RejectBadInstructions()
        {
            var result = _service.ExecuteInstructions(_rover,"a", _pluto);
            result.Should().Be("a is not a valid Instruction");
        }

        [Test]
        public void NotCarryOutNextInstructionsIfBadInput()
        {
            var result = _service.ExecuteInstructions(_rover, "af", _pluto);

            result.Should().Be("a is not a valid Instruction");
            _rover.PosY.Should().Be(0);
            _rover.PosX.Should().Be(0);
        }

        [Test]
        public void ShouldHandleDifferentCases()
        {
            var instructionString = "fFbBlLrR";

            var result = _service.ExecuteInstructions(_rover, instructionString, _pluto);
            result.Should().Be("Success");
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
            var instruction = new Instruction(_pluto, _rover, input.ToString());
            _turnOperation.Execute(instruction);
            _rover.CurrentDirection.Should().Be(output);
        }

        [TestCase(MoveInstructions.F, 1)]
        [TestCase(MoveInstructions.B, 99)]
        public void Move(MoveInstructions input, int position)
        {
            var instruction = new Instruction(_pluto, _rover, input.ToString());
            _movementOperation.Execute(instruction);
            _rover.PosX.Should().Be(0);
            _rover.PosY.Should().Be(position);
        }

        [TestCase(TurnInstructions.R, MoveInstructions.F, 0, 1)]
        [TestCase(TurnInstructions.R, MoveInstructions.B, 0, 99)]
        [TestCase(TurnInstructions.L, MoveInstructions.F, 0, 99)]
        [TestCase(TurnInstructions.L, MoveInstructions.B, 0, 1)]
        public void TurnAndMove(TurnInstructions turnInstruction, MoveInstructions moveInstruction, int yPosition, int xPosition)
        {
            var firstInstruction = new Instruction(_pluto, _rover, turnInstruction.ToString());
            var secondInstruction = new Instruction(_pluto, _rover, moveInstruction.ToString());
            _turnOperation.Execute(firstInstruction);
            _movementOperation.Execute(secondInstruction);
            _rover.PosY.Should().Be(yPosition);
            _rover.PosX.Should().Be(xPosition);
        }

        [Test]
        public void ShouldWrapAround()
        {
            var turnInstruction = new Instruction(_pluto, _rover, TurnInstructions.L.ToString());
            var moveInstruction = new Instruction(_pluto, _rover, MoveInstructions.F.ToString());

            _turnOperation.Execute(turnInstruction);
            _movementOperation.Execute(moveInstruction);
            _rover.PosY.Should().Be(0);
            _rover.PosX.Should().Be(99);
        }

        [Test]
        public void StopIfObstacle()
        {
            var moveInstruction = new Instruction(_pluto, _rover, MoveInstructions.F.ToString());
            _pluto.Grid[0][1].IsObstacle = true;
            var result = _movementOperation.Execute(moveInstruction);
            result.Should().Be("IsObstacle");
        }

        [Test]
        public void NotCarryOutNextInstructionIfObstacle()
        {
            _pluto.Grid[0][1].IsObstacle = true;
            var result = _service.ExecuteInstructions(_rover,"F", _pluto);
            result.Should().Be("IsObstacle");
        }
    }
}
