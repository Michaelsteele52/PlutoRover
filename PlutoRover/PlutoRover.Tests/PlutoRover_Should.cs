using System;
using FluentAssertions;
using NUnit.Framework;

namespace PlutoRover.Tests
{
    [TestFixture]
    public class PlutoRover_Should
    {
        private Rover _rover;
        [SetUp]
        public void SetUp()
        {
            _rover = new Rover();
        }

        [Test]
        public void RejectBadInstructions()
        {
            var result = _rover.Instruct("BadInput");
            result.Should().Be("Invalid Instruction");
        }
    }
}
