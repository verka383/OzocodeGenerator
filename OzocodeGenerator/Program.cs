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
            generateCode("input.txt", "output.ozocode");

            //colorsTest("input.txt", "output.ozocode");
        }

        /// <summary>
        /// Generates path for ozobot according to the directions in the input file.
        /// </summary>
        /// <param name="inputFile">Input file (should be text file)</param>
        /// <param name="outputFile">Output file (should have suffix .ozocode)</param>
        static void generateCode(string inputFile, string outputFile)
        {
            sr = new StreamReader(inputFile);
            sw = new StreamWriter(outputFile);
            tagsEnds = new Stack<string>();

            Basics.xml();
            Basics.block(BlockType.ozobot_go_to_next_intersection, ID++);
            Light.setTopLightColour(LightColors.xffffff);

            string line;

            // main loop for changing the direction according to the input
            while((line = sr.ReadLine()) != null)
            {
                if (line == ((int)DIRECTION.WAIT).ToString())
                {
                    Light.setTopLightColour(LightColors.xff0000);
                    Turns.wait();
                    Light.setTopLightColour(LightColors.xffff00);
                    Turns.wait();
                    Light.setTopLightColour(LightColors.x00ff00);
                    Turns.wait();
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
