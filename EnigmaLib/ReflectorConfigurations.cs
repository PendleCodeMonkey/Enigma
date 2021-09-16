using System.Collections.Generic;

namespace PendleCodeMonkey.EnigmaLib
{
	/// <summary>
	/// Implementation of the <see cref="ReflectorConfigurations"/> class.
	/// </summary>
	public static class ReflectorConfigurations
	{
		private static readonly Dictionary<ReflectorType, string> _reflectors = new Dictionary<ReflectorType, string> {
			{ ReflectorType.A, "EJMZALYXVBWFCRQUONTSPIKHGD" },
			{ ReflectorType.B, "YRUHQSLDPXNGOKMIEBFZCWVJAT" },
			{ ReflectorType.C, "FVPJIAOYEDRZXWGCTKUQSBNMHL" },
			{ ReflectorType.BThin, "ENKQAUYWJICOPBLMDXZVFTHRGS" },
			{ ReflectorType.CThin, "RDOBJNTKVEHMLFCWZAXGYIPSUQ" }
		};

		/// <summary>
		/// Enumerated values for the possible types of reflector.
		/// </summary>
		public enum ReflectorType
		{
			A,
			B,
			C,
			BThin,
			CThin
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReflectorConfigurations"/> class.
		/// </summary>
		static ReflectorConfigurations()
		{
		}

		/// <summary>
		/// Get the configuration for the reflector of the given type.
		/// </summary>
		/// <param name="type">The type of reflector to be retrieved.</param>
		/// <returns>String containing the reflector configuration (or an empty string for an invalid reflector).</returns>
		public static string GetReflector(ReflectorType type)
		{
			return _reflectors.ContainsKey(type) ? _reflectors[type] : string.Empty;
		}
	}
}
