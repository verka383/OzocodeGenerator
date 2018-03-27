using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzocodeGenerator
{
    static class Turns
    {
        /// <summary>
        /// One turn of the ozobot at the junction to the given direction.
        /// </summary>
        /// <param name="direction">Direction that should be turned.</param>
        /// <param name="debug">If true, the ozobot says the turned direction.</param>
        public static void turn(string direction, bool debug = false)
        {
            Basics.next();
            Basics.block(BlockType.ozobot_choose_way_at_intersection, Program.ID++);
            Basics.field(FieldName.DIRECTION, direction);
            if (debug)
            {
                Sound.sayDirection(((DIRECTION)Enum.Parse(typeof(DIRECTION), direction)));
            }
        }

        /// <summary>
        /// One turn of the ozobot at the junction to the given direction. After turn ozobot continues to next junction.
        /// </summary>
        /// <param name="direction">Direction that should be turned.</param>
        /// <param name="debug">If true, the ozobot says the turned direction.</param>
        public static void turnAndContinueToJunction(string direction, bool debug = false)
        {
            turn(direction, debug);
            Basics.next();
            Basics.block(BlockType.ozobot_go_to_next_intersection, Program.ID++);
        }
    }
}
