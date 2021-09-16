using System.Collections.Generic;
using System.Linq;

namespace PendleCodeMonkey.EnigmaLib
{
	/// <summary>
	/// Implementation of the <see cref="RotorTypes"/> class.
	/// </summary>
	public static class RotorTypes
	{

		private static readonly List<RotorSettings> _rotorSettings = new List<RotorSettings>
		{
			new RotorSettings(RotorType.I, "EKMFLGDQVZNTOWYHXUSPAIBRCJ", "Q"),
			new RotorSettings(RotorType.II, "AJDKSIRUXBLHWTMCQGZNPYFVOE", "E"),
			new RotorSettings(RotorType.III, "BDFHJLCPRTXVZNYEIWGAKMUSQO", "V"),
			new RotorSettings(RotorType.IV, "ESOVPZJAYQUIRHXLNFTGKDCMWB", "J"),
			new RotorSettings(RotorType.V, "VZBRGITYUPSDNHLXAWMJQOFECK", "Z"),
			new RotorSettings(RotorType.VI, "JPGVOUMFYQBENHZRDKASXLICTW", "ZM"),
			new RotorSettings(RotorType.VII, "NZJHGRCXMYSWBOUFAIVLPEKQDT", "ZM"),
			new RotorSettings(RotorType.VIII, "FKQHTLXOCBJSPDZRAMEWNIUYGV", "ZM"),
			new RotorSettings(RotorType.Beta, "LEYJVCNIXWPBQMDRTAKZGFUHOS", string.Empty),
			new RotorSettings(RotorType.Gamma, "FSOKANUERHMBTIYCWLQPZXVGJD", string.Empty)
		};

		public enum RotorType
		{
			I,
			II,
			III,
			IV,
			V,
			VI,
			VII,
			VIII,
			Beta,
			Gamma
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RotorTypes"/> class.
		/// </summary>
		static RotorTypes()
		{
		}

		/// <summary>
		/// Get rotor settings.
		/// </summary>
		/// <param name="type">The type of rotor for which settings should be retrieved.</param>
		/// <returns>The settings for the rotor of the specified type.</returns>
		public static RotorSettings GetSetting(RotorType type)
		{
			return _rotorSettings.FirstOrDefault(rotor => rotor.Type == type);
		}
	}
}
