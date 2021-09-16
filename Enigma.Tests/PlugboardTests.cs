using PendleCodeMonkey.EnigmaLib;
using System;
using Xunit;

namespace PendleCodeMonkey.Enigma.Tests
{
	public class PlugboardTests
	{
		private readonly string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		[Fact]
		public void PlugboardWithNoConnections_OutputAlwaysEqualsInput()
		{
			Plugboard pb = new Plugboard();

			foreach (char c in _alphabet)
			{
				char output = pb.GetOutput(c);
				Assert.Equal(c, output);
			}
		}

		[Fact]
		public void PlugboardWithValidConnections_OutputIsCorrect()
		{
			// Alphabet with characters switched to correspond with "AM EH KP" plugboard connections (i.e. alphabet
			// with A and M switched, E and H switched, and K and P switched).
			string resultAlphabet = "MBCDHFGEIJPLANOKQRSTUVWXYZ";

			Plugboard pb = new Plugboard();
			pb.SetConnections("AM;EH;KP");

			for (int i = 0; i < _alphabet.Length; i++)
			{
				char output = pb.GetOutput(_alphabet[i]);
				Assert.Equal(resultAlphabet[i], output);
			}
		}

		[Fact]
		public void PlugboardWithDuplicateConnections_ThrowsException()
		{
			Plugboard pb = new Plugboard();

			// Set connections to include two pairs containing the same letter ('A')
			Assert.Throws<ArgumentException>(() => pb.SetConnections("AM;EH;AP"));
		}

		[Fact]
		public void PlugboardWithInvalidConfiguration_ThrowsException()
		{
			Plugboard pb = new Plugboard();

			// Invalid connections - has three letters in its first pair.
			Assert.Throws<ArgumentException>(() => pb.SetConnections("ABC;EH;AP"));
		}

		[Fact]
		public void PlugboardWithNonAlphabeticConfiguration_ThrowsException()
		{
			Plugboard pb = new Plugboard();

			// Invalid connections - has three letters in its first pair.
			Assert.Throws<ArgumentException>(() => pb.SetConnections("AB;EH;2P"));
		}

		[Fact]
		public void PlugboardWithLowerCaseAlphabeticCharacters_IsAllowed()
		{
			Plugboard pb = new Plugboard();

			// Configuration contains lowercase letters.
			pb.SetConnections("Ac;eH;zp");

			// Check that getting the output for 'A' yields the output of an uppercase C.
			char output = pb.GetOutput('A');
			Assert.Equal('C', output);
		}

	}
}
