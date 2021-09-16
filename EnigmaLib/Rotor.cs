namespace PendleCodeMonkey.EnigmaLib
{
	/// <summary>
	/// Implementation of the <see cref="Rotor"/> class.
	/// </summary>
	public class Rotor
	{
		private readonly string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private int _position;
		private readonly int _initialPosition;

		public RotorTypes.RotorType RotorType { get; private set; }
		public string Encoding { get; private set; }
		public string TurnoverPositions { get; private set; }

		public char Ring { get; private set; }
		private int[] Map { get; set; }
		private int[] ReverseMap { get; set; }

		public int Position => _position;

		public char PositionChar => _alphabet[_position];

		/// <summary>
		/// Initializes a new instance of the <see cref="RotorTypes"/> class.
		/// </summary>
		/// <param name="type">The type of rotor to be added.</param>
		/// <param name="position">The initial character position of the rotor.</param>
		/// <param name="ring">The character position of the ring.</param>
		public Rotor(RotorTypes.RotorType type, char initialPosition, char ring)
		{
			RotorType = type;

			// Get the specified rotor's settings
			RotorSettings settings = RotorTypes.GetSetting(type);
			Encoding = settings.Encoding;
			TurnoverPositions = settings.SteppingPositions;

			int position = _alphabet.IndexOf(initialPosition);
			_initialPosition = _position = position;
			Ring = ring;

			// Populate the mapping arrays to correspond with this rotor's encoding settings.
			Map = new int[26];
			ReverseMap = new int[26];
			for (int i = 0; i < 26; i++)
			{
				int encodedCharIndex = Encoding[i] - 'A';
				Map[i] = (26 + encodedCharIndex - i) % 26;
				ReverseMap[encodedCharIndex] = (26 + i - encodedCharIndex) % 26;
			}
		}

		/// <summary>
		/// Reset the rotor back to its initial position.
		/// </summary>
		public void Reset()
		{
			_position = _initialPosition;
		}

		/// <summary>
		/// Step this rotor by one position, wrapping around when necessary.
		/// </summary>
		public void Step()
		{
			_position = (_position + 1) % 26;
		}

		/// <summary>
		/// Get the mapped value for the specified character position.
		/// </summary>
		/// <remarks>
		/// The mapping that is applied is based on the rotor's encoding settings.
		/// This method is called when traversing through the rotors in the forward direction (i.e. right-to-left) prior to passing through the reflector.
		/// </remarks>
		/// <param name="charPos">Integer position of the character.</param>
		/// <returns>The mapped value corresponding to the input character position.</returns>
		public int GetMappedValue(int charPos)
		{
			return (charPos + Map[(26 + charPos + Position - (Ring - 'A')) % 26]) % 26;
		}

		/// <summary>
		/// Get the reverse-mapped value for the specified character position.
		/// </summary>
		/// <remarks>
		/// The mapping that is applied is based on the rotor's encoding settings.
		/// This method is called when traversing through the rotors in the reverse direction (i.e. left-to-right) after passing through the reflector.
		/// </remarks>
		/// <param name="charPos">Integer position of the character.</param>
		/// <returns>The reverse-mapped value for the input character position.</returns>
		public int GetReverseMappedValue(int charPos)
		{
			return (charPos + ReverseMap[(26 + charPos + Position - (Ring - 'A')) % 26]) % 26;

		}

		/// <summary>
		/// Determine if the rotor is currently at one of the 'turnover' positions (i.e. a position which will trigger
		/// the rotor to the left of it to step)
		/// </summary>
		/// <returns><c>true</c> if the rotor is at a turnover position, otherwise <c>false</c>.</returns>
		public bool IsTurnover() => TurnoverPositions.Contains(PositionChar);

	}
}
