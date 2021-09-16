using PendleCodeMonkey.EnigmaLib;
using Xunit;

namespace PendleCodeMonkey.Enigma.Tests
{
	public class RotorTests
	{
		[Fact]
		public void RotorMappingTest()
		{
			Rotor rotor = new Rotor(RotorTypes.RotorType.I, 'A', 'A');

			// Check that reverse mapping is actually the reverse of the value mapping functionality.
			for (int i = 0; i < 26; i++)
			{
				int mapped = rotor.GetMappedValue(i);
				int reverseMapped = rotor.GetReverseMappedValue(mapped);
				Assert.Equal(reverseMapped, i);
			}
		}

		[Fact]
		public void RotorStepTest()
		{
			Rotor rotor = new Rotor(RotorTypes.RotorType.I, 'A', 'A');

			// Step the rotor on 3 positions and confirm that the position has actually advanced 3 positions.
			rotor.Step();
			rotor.Step();
			rotor.Step();
			Assert.Equal('D', rotor.PositionChar);
		}

		[Fact]
		public void RotorStepWraparoundTest()
		{
			// Initial position of rotor is 'Z'
			Rotor rotor = new Rotor(RotorTypes.RotorType.I, 'Z', 'A');

			// Step the rotor on one position and confirm that the position has wrapped around to 'A'.
			rotor.Step();
			Assert.Equal('A', rotor.PositionChar);
		}

		[Fact]
		public void RotorResetTest()
		{
			char initialPosition = 'D';
			Rotor rotor = new Rotor(RotorTypes.RotorType.I, initialPosition, 'A');

			// Step the rotor on by 3 positions.
			rotor.Step();
			rotor.Step();
			rotor.Step();

			// Reset rotor and confirm that it has been reset to its initial position.
			rotor.Reset();
			Assert.Equal(initialPosition, rotor.PositionChar);
		}

		[Fact]
		public void RotorTurnoverTest()
		{
			// Note: Rotor II has a turnover point at letter 'E'
			Rotor rotor = new Rotor(RotorTypes.RotorType.II, 'D', 'A');

			// Initial rotor position is 'D' so confirm we're not at the turnover point.
			Assert.False(rotor.IsTurnover());

			// Step rotor on by one position (to 'E') - confirming that we are then at the turnover point.
			rotor.Step();
			Assert.True(rotor.IsTurnover());

			// Step rotor on by one position again (to 'F') - confirming we are no longer at the turnover point.
			rotor.Step();
			Assert.False(rotor.IsTurnover());
		}

	}
}
