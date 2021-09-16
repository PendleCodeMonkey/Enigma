using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PendleCodeMonkey.EnigmaLib
{
	/// <summary>
	/// Implementation of the <see cref="Plugboard"/> class.
	/// </summary>
	public class Plugboard
	{

		private readonly List<(char, char)> _settings = new List<(char, char)>();

		public static readonly string Separator = ";";

		/// <summary>
		/// Initializes a new instance of the <see cref="Plugboard"/> class.
		/// </summary>
		public Plugboard()
		{
		}

		/// <summary>
		/// Set the connection between pairs of characters.
		/// </summary>
		/// <param name="characterPairs">String containing the pairs of characters to be connected (with each pair separated by a separator character).</param>
		/// <exception cref="ArgumentException">Thrown when an invalid plugboard configuration has been specified.</exception>
		public void SetConnections(string characterPairs)
		{
			if (string.IsNullOrEmpty(characterPairs))
			{
				// No plugboard connections.
				_settings.Clear();
				return;
			}

			// Check that the specified string contains only letters and the special separator character.
			// We allow lowercase letters because the whole string is converted to uppercase before being split into character pairs.
			string validChars = "A-Za-z" + Separator;
			bool valid = Regex.IsMatch(characterPairs, "^[" + validChars + "]+$");
			if (valid)
			{
				// Convert the input string to uppercase (in case any lowercase letters have been included) and split
				// it into sub-strings (at the separator characters), each of which should be a pair of letters.
				string[] pairs = characterPairs.ToUpper().Split(Separator);

				// Perform some basic validation of these sub-strings.
				if (valid = IsValid(pairs))
				{
					foreach (var pair in pairs)
					{
						if (IsDuplicate(pair[0], pair[1]))
						{
							_settings.Clear();
							valid = false;
							break;
						}
						_settings.Add((pair[0], pair[1]));
					}
				}
			}

			if (!valid)
			{
				throw new ArgumentException("Invalid plugboard settings.");
			}
		}

		/// <summary>
		/// Determine if a connection has already been set up for either of a specified pair of characters. 
		/// </summary>
		/// <param name="c1">First character being paired.</param>
		/// <param name="c2">Second character being paired.</param>
		/// <returns><c>true</c> if a connection has already been specified for one (or both) of the characters, otherwise <c>false</c>.</returns>
		private bool IsDuplicate(char c1, char c2)
		{
			return _settings.Where(pair => pair.Item1 == c1 || pair.Item1 == c2 ||
										pair.Item2 == c1 || pair.Item2 == c2).Any();
		}

		/// <summary>
		/// Perform some basic validation on the character pair strings.
		/// </summary>
		/// <param name="pairs">String containing the pairs of characters to be connected (with each pair separated by a semicolon).</param>
		/// <returns><c>true</c> if the character pair strings all appear to be valid, otherwise <c>false</c>.</returns>
		private bool IsValid(string[] pairs)
		{
			// Check for the presence of any invalid pairs (i.e. ones that are not 2 characters long or that have the same letter specified for
			// each character in the pair)
			return !pairs.Where(pair => pair.Length != 2 || pair[0] == pair[1]).Any();
		}

		/// <summary>
		/// Get the output from the plugboard for the specified character.
		/// </summary>
		/// <remarks>
		/// This method returns the character to which the input character is connected
		/// on the plugboard (e.g if a connection is made between 'A' and 'T' then an input of 'A'
		/// will result in an output of 'T'; likewise, an input of 'T' will yield an output of 'A')
		/// If there is no paired connection on the plugboard for the input character then the input
		/// is simply returned as the output as-is.
		/// </remarks>
		/// <param name="input">Input character to the plugboard.</param>
		/// <returns>Output character from the plugboard.</returns>
		public char GetOutput(char input)
		{
			foreach (var pair in _settings)
			{
				if (pair.Item1 == input)
				{
					return pair.Item2;
				}
				if (pair.Item2 == input)
				{
					return pair.Item1;
				}
			}

			// Character unaffected by plugboard configuration so just return it as-is.
			return input;
		}
	}
}
