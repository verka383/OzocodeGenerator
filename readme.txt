OZOCODE GENERATOR
- generates simple ozocode according to the direction given as input
- generated code is possible to load into ozobot without any corrections

Program.cs
- main program of the application
- according to the directions in file "input.txt" generates code for ozobot such that ozobot goes directions in input file and after that stops at the next junction
- in the input file there is one number representing the direction at each row
- constants for directions (correspond to constants used in ozoblockly)
	- 1 = FORWARD (STRAIGHT)
	- 2 = LEFT
	- 4 = RIGHT
	- 8 = BACKWARD
	- 0 = WAIT

Basics.cs
- functions for generating basic blocks
	- xml, next, block, field, value
- to avoid recursion, end tags of the blocks are saved in a stack and are written at the end of the program

Enums.cs
- enums with values that are used in attributes of xml tags

Turns.cs
- implementation of the turn to the given direction

Sound.cs
- implementation of saySurfaceColor and SayDirection (good for debug)

Light.cs
- implementation of setTopLightColour to every color supported by ozobot and of turning the top light of