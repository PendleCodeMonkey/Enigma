using PendleCodeMonkey.EnigmaLib;
using System;

namespace PendleCodeMonkey.EnigmaConsoleTestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			CheckKnownMessages();

			Console.WriteLine();

			string settings = ",B,I,A,A,II,E,A,III,T,A";
			EncodeMessage(settings, "ENIGMAXMACHINEXEMULATORXBYXPENDLECODEMONKEY");
			Console.WriteLine();
			EncodeMessage(settings, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");

			Console.ReadKey();
		}

		private static void CheckKnownMessages()
		{
			// Check Enigma machine functionality using some known sample messages (see http://wiki.franklinheath.co.uk/index.php/Enigma/Sample_Messages).
			// Note that the decoded messages are in German

			// "Operation Barbarossa, 1941" messages
			string settings = "AV;BS;CG;DL;FU;HZ;IN;KM;OW;RX,B,II,B,B,IV,L,U,V,A,L";
			string decoded = DecodeMessage(settings, "EDPUDNRGYSZRCXNUYTPOMRMBOFKTBZREZKMLXLVEFGUEYSIOZVEQMIKUBPMMYLKLTTDEISMDICAGYKUACTCDOMOHWXMUUIAUBSTSLRNBZSZWNRFXWFYSSXJZVIJHIDISHPRKLKAYUPADTXQSPINQMATLPIFSVKDASCTACDPBOPVHJK");
			bool success = decoded.Equals("AUFKLXABTEILUNGXVONXKURTINOWAXKURTINOWAXNORDWESTLXSEBEZXSEBEZXUAFFLIEGERSTRASZERIQTUNGXDUBROWKIXDUBROWKIXOPOTSCHKAXOPOTSCHKAXUMXEINSAQTDREINULLXUHRANGETRETENXANGRIFFXINFXRGTX");
			Console.WriteLine("Decoding message 1: " + (success ? "SUCCEEDED" : "FAILED!"));

			settings = "AV;BS;CG;DL;FU;HZ;IN;KM;OW;RX,B,II,L,B,IV,S,U,V,D,L";
			decoded = DecodeMessage(settings, "SFBWDNJUSEGQOBHKRTAREEZMWKPPRBXOHDROEQGBBGTQVPGVKBVVGBIMHUSZYDAJQIROAXSSSNREHYGGRPISEZBOVMQIEMMZCYSGQDGRERVBILEKXYQIRGIRQNRDNVRXCYYTNJR");
			success = decoded.Equals("DREIGEHTLANGSAMABERSIQERVORWAERTSXEINSSIEBENNULLSEQSXUHRXROEMXEINSXINFRGTXDREIXAUFFLIEGERSTRASZEMITANFANGXEINSSEQSXKMXKMXOSTWXKAMENECXK");
			Console.WriteLine("Decoding message 2: " + (success ? "SUCCEEDED" : "FAILED!"));


			// Enigma Instruction Manual, 1930
			settings = "AM;FI;NV;PS;TU;WZ,A,II,A,X,I,B,M,III,L,V";
			decoded = DecodeMessage(settings, "GCDSEAHUGWTQGRKVLFGXUCALXVYMIGMMNMFDXTGNVHVRMMEVOUYFZSLRHDRRXFJWCFHUHMUNZEFRDISIKBGPMYVXUZ");
			success = decoded.Equals("FEINDLIQEINFANTERIEKOLONNEBEOBAQTETXANFANGSUEDAUSGANGBAERWALDEXENDEDREIKMOSTWAERTSNEUSTADT");
			Console.WriteLine("Decoding message 3: " + (success ? "SUCCEEDED" : "FAILED!"));


			// U-264 (Kapitänleutnant Hartwig Looks), 1942
			settings = "AT;BL;DF;GJ;HM;NW;OP;QY;RZ;VX,BThin,Beta,V,A,II,J,A,IV,N,A,I,A,V";
			decoded = DecodeMessage(settings, "NCZWVUSXPNYMINHZXMQXSFWXWLKJAHSHNMCOCCAKUQPMKCSMHKSEINJUSBLKIOSXCKUBHMLLXCSJUSRRDVKOHULXWCCBGVLIYXEOAHXRHKKFVDREWEZLXOBAFGYUJQUKGRTVUKAMEURBVEKSUHHVOYHABCJWMAKLFKLMYFVNRIZRVVRTKOFDANJMOLBGFFLEOPRGTFLVRHOWOPBEKVWMUQFMPWPARMFHAGKXIIBG");
			success = decoded.Equals("VONVONJLOOKSJHFFTTTEINSEINSDREIZWOYYQNNSNEUNINHALTXXBEIANGRIFFUNTERWASSERGEDRUECKTYWABOSXLETZTERGEGNERSTANDNULACHTDREINULUHRMARQUANTONJOTANEUNACHTSEYHSDREIYZWOZWONULGRADYACHTSMYSTOSSENACHXEKNSVIERMBFAELLTYNNNNNNOOOVIERYSICHTEINSNULL");
			Console.WriteLine("Decoding message 4: " + (success ? "SUCCEEDED" : "FAILED!"));


			// Scharnhorst (Konteradmiral Erich Bey), 1943
			// NOTE: settings are "AN;EZ;HK;IJ;LR;MQ;OT;PV;SW;UX,B,III,U,A,VI,Z,H,VIII,V,M" - the following settings string replaces some of the Ring position
			// alphabetic characters with their numeric equivalents (e.g. "H" with 8, "M" with 13) to check that either alphabetic or numeric
			// Ring position values can be specified.
			settings = "AN;EZ;HK;IJ;LR;MQ;OT;PV;SW;UX,B,III,U,1,VI,Z,8,VIII,V,13";
			decoded = DecodeMessage(settings, "YKAENZAPMSCHZBFOCUVMRMDPYCOFHADZIZMEFXTHFLOLPZLFGGBOTGOXGRETDWTJIQHLMXVJWKZUASTR");
			success = decoded.Equals("STEUEREJTANAFJORDJANSTANDORTQUAAACCCVIERNEUNNEUNZWOFAHRTZWONULSMXXSCHARNHORSTHCO");
			Console.WriteLine("Decoding message 5: " + (success ? "SUCCEEDED" : "FAILED!"));
		}

		private static string DecodeMessage(string settings, string message)
		{
			EnigmaMachine machine = CreateMachine(settings);

			// Decode the string.
			string decoded = machine.EncodeString(message);
			return decoded;
		}

		private static void EncodeMessage(string settings, string message)
		{
			EnigmaMachine machine = CreateMachine(settings);

			Console.WriteLine("Original message: " + message);

			// Encode the string.
			string encoded = machine.EncodeString(message);
			Console.WriteLine("Encoded as:       " + encoded);

			// Reset the machine back to its original state (so we are decoding using the same settings).
			// This basically just involves resetting all rotors to their original positions.
			machine.Reset();

			// Pass the encoded message back through the Enimga machine (to decode it) - which should get us back to the original message. 
			string decoded = machine.EncodeString(encoded);
			Console.WriteLine("Decoded back as:  " + decoded);
		}

		private static EnigmaMachine CreateMachine(string settings)
		{
			EnigmaMachine machine = new EnigmaMachine();

			string[] s = settings.Split(',');
			if (s.Length < 11 || (s.Length - 2) % 3 != 0)
			{
				Console.WriteLine("Invalid configuration settings.");
				return null;
			}
			int numRotors = (s.Length - 2) / 3;

			int index = 0;
			string plugboardConfig = s[index++];
			machine.SetPlugboard(plugboardConfig);

			string reflectorType = s[index++];
			var refType = reflectorType switch
			{
				"A" => ReflectorConfigurations.ReflectorType.A,
				"C" => ReflectorConfigurations.ReflectorType.C,
				"BThin" => ReflectorConfigurations.ReflectorType.BThin,
				"CThin" => ReflectorConfigurations.ReflectorType.CThin,
				_ => ReflectorConfigurations.ReflectorType.B                // Default - reflector "B"
			};
			machine.SetReflector(refType);

			for (int rotor = 0; rotor < numRotors; rotor++)
			{
				var type = s[index++] switch
				{
					"II" => RotorTypes.RotorType.II,
					"III" => RotorTypes.RotorType.III,
					"IV" => RotorTypes.RotorType.IV,
					"V" => RotorTypes.RotorType.V,
					"VI" => RotorTypes.RotorType.VI,
					"VII" => RotorTypes.RotorType.VII,
					"VIII" => RotorTypes.RotorType.VIII,
					"Beta" => RotorTypes.RotorType.Beta,
					"Gamma" => RotorTypes.RotorType.Gamma,
					_ => RotorTypes.RotorType.I,                    // Default - rotor "I"
				};
				char initPosition = s[index].Length > 0 ? s[index][0] : 'A';
				index++;

				char ring = 'A';            // Default to 'A' (i.e. a ring with a numeric position of 1)
				if (s[index].Length > 0)
				{
					if (char.IsDigit(s[index][0]))
					{
						// Looks like a numeric ring setting so attempt to parse the value.
						if (int.TryParse(s[index], out int ringValue))
						{
							if (ringValue >= 1 && ringValue <= 26)
							{
								ring = (char)(ringValue - 1 + 'A');
							}
						}
					}
					else
					{
						ring = s[index][0];
					}
				}
				if (!machine.AddRotor(type, initPosition, ring))
				{
					Console.WriteLine("Duplicate rotor types cannot be specified.");
					return null;
				}
				index++;
			}

			return machine;
		}
	}
}
