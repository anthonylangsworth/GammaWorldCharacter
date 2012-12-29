Gamma World Character
===

A representation of a character in "D&D Gamma World", a role playing game
from Wizards of the Coast (http://www.wizards.com/dnd/gammaworld.aspx).

Note that much of the material; such as origin names, power names and
power descriptions; is copyright 2010 Wizards of the Coast LLC. Therefore, 
this only contains information for creating low level characters with a 
few origins. Note that additional origins, powers, higher level 
characters and so on can be added easily.

For more information on using and extending the library, see the "doc"
folder.

Structure
---
- GammaWorldCharacter: The main library containing the Core rules
and a few origins and powers.
- GammaWorldCharacter.Samples: Sample characters.
- GammaWorldCharacter.Test.Unit: Unit tests.
- GammaWorldCharacter.Test.Integration: Integration tests,
specifically testing the sample characters are constructed
correctly.
- CreateCharacterTest: A project that uses Visual Studio's T4
library to create an NUnit test class to test a character (used
to turn sample characters in GammaWorldCharater.Samples into 
tests in GammaWorldCharacter.Test.Integration).

License
---
See LICENSE.txt (Apache 2.0).

Dependencies
---
- NUnit 2.6.2 (testing only, http://nunit.org/)