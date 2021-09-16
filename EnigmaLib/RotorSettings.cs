namespace PendleCodeMonkey.EnigmaLib
{
	/// <summary>
	/// Implementation of the <see cref="RotorSettings"/> class.
	/// </summary>
	public class RotorSettings
	{
		/// <summary>
		/// The rotor type.
		/// </summary>
		public RotorTypes.RotorType Type { get; private set; }

		/// <summary>
		/// The character encoding string.
		/// </summary>
		public string Encoding { get; private set; }

		/// <summary>
		/// String containing the characters at which 'carry-over' stepping occurs.
		/// </summary>
		public string SteppingPositions { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="RotorSettings"/> class.
		/// </summary>
		/// <param name="type">The rotor type.</param>
		/// <param name="encoding">The character encoding string for the rotor.</param>
		/// <param name="steppingPos">String containing the characters at which 'carry-over' stepping occurs.</param>
		public RotorSettings(RotorTypes.RotorType type, string encoding, string steppingPos)
		{
			Type = type;
			Encoding = encoding;
			SteppingPositions = steppingPos;
		}
	}
}
