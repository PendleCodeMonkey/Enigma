using PendleCodeMonkey.EnigmaLib;
using Xunit;

namespace PendleCodeMonkey.Enigma.Tests
{
	public class EnigmaMachineTests
	{

		[Fact]
		public void AddingDuplicateRotorTypes_ReturnsFalse()
		{
			EnigmaMachine machine = new EnigmaMachine();

			bool success = machine.AddRotor(RotorTypes.RotorType.I, 'A', 'A');
			Assert.True(success);
			success = machine.AddRotor(RotorTypes.RotorType.I, 'A', 'A');
			Assert.False(success);
		}

	}
}
