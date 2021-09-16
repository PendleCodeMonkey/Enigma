using System.Collections.Generic;
using System.Text;

namespace PendleCodeMonkey.EnigmaLib
{
	/// <summary>
	/// Implementation of the <see cref="EnigmaMachine"/> class.
	/// </summary>
	public class EnigmaMachine
	{
		private Plugboard _plugboard;
		private readonly List<Rotor> _rotors = new List<Rotor>();
		private ReflectorConfigurations.ReflectorType _reflectorType;

		private List<int> _reflectorMap;

		private readonly string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		/// <summary>
		/// Enumerated values for the direction in which characters are passing through the rotors.
		/// </summary>
		private enum Direction
		{
			LeftToRight,
			RightToLeft
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EnigmaMachine"/> class.
		/// </summary>
		public EnigmaMachine()
		{
			_plugboard = new Plugboard();
			SetPlugboard(string.Empty);     // Default - no plugboard connections.
		}

		/// <summary>
		/// Ad a rotor to the Enigma machine.
		/// </summary>
		/// <remarks>
		/// Rotors should be added in left-to-right order.
		/// </remarks>
		/// <param name="type">The type of rotor to be added.</param>
		/// <param name="initialPosition">The initial character position of the rotor.</param>
		/// <param name="ring">The character position of the ring.</param>
		/// <returns><c>true</c> if the rotor was successfully added, otherwise <c>false</c>.</returns>
		public bool AddRotor(RotorTypes.RotorType type, char initialPosition, char ring)
		{
			foreach (var rotor in _rotors)
			{
				if (rotor.RotorType == type)
				{
					return false;
				}
			}
			_rotors.Add(new Rotor(type, initialPosition, ring));
			return true;
		}

		/// <summary>
		/// Set the reflector configuration to be used.
		/// </summary>
		/// <param name="type">The type of reflector to be used.</param>
		public void SetReflector(ReflectorConfigurations.ReflectorType type)
		{
			_reflectorType = type;
			var reflectorConfig = ReflectorConfigurations.GetReflector(type);
			if (string.IsNullOrEmpty(reflectorConfig))
			{
				_reflectorMap = null;
				return;
			}

			// Populate the reflector mapping array.
			_reflectorMap = new List<int>();
			for (int i = 0; i < 26; i++)
			{
				_reflectorMap.Add((26 + (reflectorConfig[i] - 'A') - i) % 26);
			}

		}

		/// <summary>
		/// Set the plugboard configuration settings.
		/// </summary>
		/// <remarks>
		/// The supplied settings string should contain pairs of characters which are
		/// to be connected on the plugboard, separated by semicolon characters (for example
		/// "AT;FM;PX" indicates plugboard connections for the letters A and T, F and M, and P and X)
		/// </remarks>
		/// <param name="settings">String containing the pairs of characters that are to be connected, separated by semicolon characters.</param>
		public void SetPlugboard(string settings)
		{
			_plugboard.SetConnections(settings);
		}

		/// <summary>
		/// Reset all rotors back to their initial settings.
		/// </summary>
		public void Reset()
		{
			foreach (var rotor in _rotors)
			{
				rotor.Reset();
			}
		}

		/// <summary>
		/// Encode/decode a string.
		/// </summary>
		/// <remarks>
		/// This method can also be called to decode a previously encoded string.
		/// </remarks>
		/// <param name="s">The string to be encoded/decoded. Must contain only uppercase letters.</param>
		/// <returns>The encoded/decoded string.</returns>
		public string EncodeString(string s)
		{
			// A valid Enigma machine must have 3 or more rotors and a valid reflector.
			if (_rotors.Count < 3 || _reflectorMap == null)
			{
				throw new System.Exception("Invalid rotor or reflector configuration");
			}

			// Encode each character in the string, building up the encoded result string.
			StringBuilder sb = new StringBuilder();
			foreach (var c in s)
			{
				sb.Append(EncodeCharacter(c));
			}

			return sb.ToString();
		}

		/// <summary>
		/// Rotate the rotor configuration by a single step, performing 'turnover' steps on the
		/// two left-most rotors when required.
		/// </summary>
		/// <remarks>
		/// On an Enigma machine having 4 rotors, the left-hand rotor does not step.
		/// </remarks>
		private void RotateRotors()
		{
			// There must be 3 or more rotors.
			if (_rotors.Count >= 3)
			{
				int rightHandRotorIndex = _rotors.Count - 1;

				if (_rotors[rightHandRotorIndex - 1].IsTurnover())
				{
					// If 2nd rotor from right is at a turn-over point then the 2nd and 3rd rotors from the right both step.
					_rotors[rightHandRotorIndex - 2].Step();
					_rotors[rightHandRotorIndex - 1].Step();
				}
				else if (_rotors[rightHandRotorIndex].IsTurnover())
				{
					// If right-hand rotor is at a turn-over point then the rotor to its left steps.
					_rotors[rightHandRotorIndex - 1].Step();
				}

				// Right-hand rotor always steps.
				_rotors[rightHandRotorIndex].Step();
			}
		}

		/// <summary>
		/// Apply the rotor mapping to the specified character (using all rotors)
		/// </summary>
		/// <param name="c">The character entering the rotors.</param>
		/// <param name="direction">The direction through the rotors that the character should pass.</param>
		/// <returns>The resulting character after passing through the rotors.</returns>
		private char ApplyRotorMapping(char c, Direction direction)
		{
			int cPos = c - 'A';
			if (direction == Direction.RightToLeft)
			{
				for (int i = _rotors.Count - 1; i >= 0; i--)
				{
					cPos = _rotors[i].GetMappedValue(cPos);
				}
			}
			else
			{
				for (int i = 0; i < _rotors.Count; i++)
				{
					cPos = _rotors[i].GetReverseMappedValue(cPos);
				}
			}

			return _alphabet[cPos];
		}

		/// <summary>
		/// Apply reflector mapping to the specified character.
		/// </summary>
		/// <remarks>
		/// The reflector is applied between the first pass through the rotors (in a right-to-left direction) and
		/// the second pass back through the rotors (in a left-to-right direction).
		/// </remarks>
		/// <param name="c">The character entering the reflector.</param>
		/// <returns>The resulting character after the reflector has been applied.</returns>
		private char ApplyReflectorMapping(char c)
		{
			if (_reflectorMap == null)
			{
				// No reflector so just return the input character.
				return c;
			}

			int cPos = c - 'A';
			cPos = (cPos + _reflectorMap[cPos]) % 26;
			return _alphabet[cPos];
		}

		/// <summary>
		/// Encode a single character.
		/// </summary>
		/// <param name="c">The character to be encoded.</param>
		/// <returns>The encoded character after passing through the entire Enigma machine.</returns>
		private char EncodeCharacter(char c)
		{
			// Rotate the rotors prior to encoding this character.
			RotateRotors();

			// First we apply the plugboard mapping (if any).
			c = _plugboard.GetOutput(c);

			// Then pass through each of the rotors (from right to left - i.e 3rd, then 2nd, then 1st)
			c = ApplyRotorMapping(c, Direction.RightToLeft);

			// Reflect at the end (so we don't just encode back to the original input character)
			// Note: if the following function call is commented out then the encoded message will be identical to the original input message.
			c = ApplyReflectorMapping(c);

			// Pass back through the rotors in reverse order (i.e. left to right - 1st, 2nd, then 3rd)
			c = ApplyRotorMapping(c, Direction.LeftToRight);

			// And apply the final plugboard mapping (if any).
			c = _plugboard.GetOutput(c);

			// Return the encoded character.
			return c;
		}

	}
}
