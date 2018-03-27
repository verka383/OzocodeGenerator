using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzocodeGenerator
{
    static class Light
    {
        /// <summary>
        /// Sets the top light color to the given color.
        /// </summary>
        /// <param name="color">Required color of the top light.</param>
        public static void setTopLightColour(LightColors color)
        {
            Basics.next();
            Basics.block(BlockType.ozobot_LED_colour_picker, Program.ID++);
            Basics.field(FieldName.COLOUR, color.ToString().Replace('x', '#'));
        }

        /// <summary>
        /// Turns the top light off.
        /// </summary>
        public static void turnOffLight()
        {
            Basics.next();
            Basics.blockClosed(BlockType.system_turn_off_leds, Program.ID++);
        }
    }
}
