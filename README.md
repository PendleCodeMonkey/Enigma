# An Enigma Machine Simulator Library #

This repo contains the code for an Enigma Machine Simulator library.

For details on how the Enigma machine worked  and how it was used, see https://www.codesandciphers.org.uk/enigma/

<br>

The Enigma solution consists of the following projects:

- **EnigmaConsoleTestApp**: A console application that demonstrates the functionality of the library.
- **EnigmaLib**: The code for the library itself.
- **Enigma.Tests**: A handful of unit tests.

<br>

### Prerequisites

- [.NET Core 3.1 SDK](https://www.microsoft.com/net/download/core)
  
<br>

### Usage

The included **EnigmaConsoleTestApp** project demonstrates how to use the Enigma Machine Simulator library.

The settings strings used within this console test application are in a comma-separated format, consisting of the following elements:
1. Plugboard settings - a semi-colon separated list of plugboard character pairs.
2. Reflector type - e.g. `A`, `B`, `C`, `BThin`, or `CThin` (with the default being `B`).
3. Rotor type - e.g. `I`, `II`, `III`, `IV`, `V`, `VI`, `VII`, `VIII`, `Beta`, or `Gamma` (with the default being `I`).
4. Initial position - Initial setting of the rotor (an alphabetic character in the range A-Z).
5. Ring position - Can be specified in alphabetic form (in the range A-Z) or numeric form (in the range 1 to 26).

Elements 3-5 are then repeated for each additional rotor configuration.

For example, the settings string "AV;BS;CG;DL;FU;HZ;IN;KM;OW;RX,B,II,B,B,IV,L,U,V,A,L" configures the Enigma Machine as follows:

Plugboard settings with the following character pairs: A-V, B-S, C-G, D-L, F-U, H-Z, I-N, K-M, O-W, R-X  
Reflector type: `B`  
1st rotor - Type: `II`, Initial position: B, Ring position: B  
2nd rotor - Type: `IV`, Initial position: L, Ring position: U  
3rd rotor - Type: `V`, Initial position: A, Ring position: L  
