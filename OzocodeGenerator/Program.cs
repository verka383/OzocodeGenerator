using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzocodeGenerator
{
    class Program
    {
        /// <summary>
        /// StreamReader for reading from the input file.
        /// </summary>
        public static StreamReader sr;

        /// <summary>
        /// StreamWriter for writing into the output file.
        /// </summary>
        public static StreamWriter sw;

        /// <summary>
        /// Stack for holding the ends of tags that should be written at the end.
        /// </summary>
        public static Stack<string> tagsEnds;

        /// <summary>
        /// ID of the next written block (globally unique).
        /// </summary>
        public static int ID = 1;

        static void Main(string[] args)
        {
            int[] input = { 1, 2, 3, 4, 5, 6, 9, 10, 12, 13, 14, 15 };
            /*
            for (int i = 0; i < 1; i++)
            {
                generateCode("../../../../TestCases/CBSOutputs/Res" + input[i] + "-0.txt", "../../../../TestCases/GeneratorOutputs/Code" + input[i] + "-0.txt");
                generateCode("../../../../TestCases/CBSOutputs/Res" + input[i] + "-1.txt", "../../../../TestCases/GeneratorOutputs/Code" + input[i] + "-1.txt");
                Console.WriteLine("Code {0} generated.", i);
            }*/

            //generateCode("input.txt", "output.ozocode");

            generateWithArray(@"C:\Users\VSk\OneDrive\MFF UK\MGR\OzobotiClanek\OzobotiClanek\bin\Debug\clanek\turning/1.txt0.txt", @"C:\Users\VSk\OneDrive\MFF UK\MGR\OzobotiClanek\OzobotiClanek\bin\Debug\clanek\turning\1\0.ozocode");
            generateWithArray(@"C:\Users\VSk\OneDrive\MFF UK\MGR\OzobotiClanek\OzobotiClanek\bin\Debug\clanek\turning/1.txt1.txt", @"C:\Users\VSk\OneDrive\MFF UK\MGR\OzobotiClanek\OzobotiClanek\bin\Debug\clanek\turning\1\1.ozocode");
            generateWithArray(@"C:\Users\VSk\OneDrive\MFF UK\MGR\OzobotiClanek\OzobotiClanek\bin\Debug\clanek\turning/1.txt2.txt", @"C:\Users\VSk\OneDrive\MFF UK\MGR\OzobotiClanek\OzobotiClanek\bin\Debug\clanek\turning\1\2.ozocode");
            generateWithArray(@"C:\Users\VSk\OneDrive\MFF UK\MGR\OzobotiClanek\OzobotiClanek\bin\Debug\clanek\turning/1.txt3.txt", @"C:\Users\VSk\OneDrive\MFF UK\MGR\OzobotiClanek\OzobotiClanek\bin\Debug\clanek\turning\1\3.ozocode");

            //colorsTest("input.txt", "output.ozocode");
        }

        /// <summary>
        /// Generates path for ozobot according to the directions in the input file.
        /// </summary>
        /// <param name="inputFile">Input file (should be text file)</param
        /// <param name="outputFile">Output file (should have suffix .ozocode)</param>
        static void generateCode(string inputFile, string outputFile)
        {
            sr = new StreamReader(inputFile);
            sw = new StreamWriter(outputFile);
            tagsEnds = new Stack<string>();

            Basics.xml();
            Basics.setVariable(Variables.x, 1.ToString());
            Basics.next();
            Basics.block(BlockType.ozobot_go_to_next_intersection, ID++);
            Light.setTopLightColour(LightColors.xffffff);

            string line;

            // main loop for changing the direction according to the input
            while((line = sr.ReadLine()) != null)
            {
                if (line == ((int)DIRECTION.WAIT).ToString())
                {
                    Light.setTopLightColour(LightColors.xff0000);
                    Turns.wait(120);
                    Light.setTopLightColour(LightColors.xffff00);
                    Turns.wait(120);
                    Light.setTopLightColour(LightColors.x00ff00);
                    Turns.wait(120);
                    Light.setTopLightColour(LightColors.xffffff);
                }
                else
                {
                    Turns.turnAndContinueToJunction(((DIRECTION)Enum.Parse(typeof(DIRECTION), line)).ToString(), true);
                }
            }

            Basics.next();
            Basics.block(BlockType.ozobot_stopMotors, ID++);
            Basics.PopTagsEnds();

            sr.Close();
            sw.Close();
        }

        /// <summary>
        /// Generates path for ozobot according to the directions in the input file.
        /// Uses arrays and simple obstacle detection - moves one node back if the path forward
        /// is not free.
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="outputFile"></param>
        static void generateWithArray(string inputFile, string outputFile)
        {
            sr = new StreamReader(inputFile);
            sw = new StreamWriter(outputFile);
            tagsEnds = new Stack<string>();
            List<int> values = new List<int>();

            Basics.xml();
            
            string line;

            // main loop for changing the direction according to the input
            while ((line = sr.ReadLine()) != null)
            {
                values.Add(int.Parse(line));  
            }

            Arrays.ArrayDeclaration(values.Count);

            copyFromFile("codeParts/funkcesCom.ozocode");

            for (int i = 0; i < values.Count; i++)
            {
                Arrays.ArrayElementsSet(i, values[i]);
                if (i != values.Count - 1)
                {
                    Basics.next();
                }
            }

            //("codeParts/telosKom.ozocode");
            //copyFromFile("codeParts/telobezKom.ozocode");
            //copyFromFile("codeParts/telobezDet.ozocode");
            //copyFromFile("codeParts/telobezOtocek.ozocode");
            copyFromFile("codeParts/turningexample.ozocode");
            Basics.PopTagsEnds();

            sr.Close();
            sw.Close();
        }

        /// <summary>
        /// Copies given file to the output file.
        /// </summary>
        /// <param name="fileName"></param>
        public static void copyFromFile(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                sw.WriteLine(line);
            }
        }

        /// <summary>
        /// Repeats to turn left, right, forward and back 4x. On every junction changes color (to show every possible color at least once) and says color of the surface.
        /// </summary>
        /// <param name="inputFile">Input file (txt)</param>
        /// <param name="outputFile">Output file (ozocode)</param>
        static void colorsTest(string inputFile, string outputFile)
        {
            sr = new StreamReader(inputFile);
            sw = new StreamWriter(outputFile);
            tagsEnds = new Stack<string>();

            Basics.xml();
            Basics.block(BlockType.ozobot_go_to_next_intersection, ID++);

            string line;

            // main loop for changing the direction according to the input
            while ((line = sr.ReadLine()) != null)
            {
                Turns.turnAndContinueToJunction(((DIRECTION)Enum.Parse(typeof(DIRECTION), line)).ToString(), false);
                Sound.saySurfaceColor();
                Light.setTopLightColour((LightColors)Enum.Parse(typeof(LightColors), (Program.ID % 12).ToString()));
            }

            Basics.next();
            Basics.block(BlockType.ozobot_stopMotors, ID++);
            Basics.PopTagsEnds();

            sr.Close();
            sw.Close();
        }
    }
}
