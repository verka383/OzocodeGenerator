using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzocodeGenerator
{
    static class Sound
    {
        public static void saySurfaceColor()
        {
            Basics.next();
            Basics.block(BlockType.ozobot_evo_say_colour, Program.ID++);
            Basics.GetSurfaceColorValue(ValueName.VALUE);   
        }

        public static void sayDirection(DIRECTION direction)
        {
            Basics.next();
            Basics.block(BlockType.ozobot_evo_say_direction_with_dropdown, Program.ID++);
            Basics.field(FieldName.VALUE, ((int)direction).ToString());
        }

    }
}
