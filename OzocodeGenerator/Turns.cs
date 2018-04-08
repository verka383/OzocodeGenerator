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
            Turns.WhileObstacle();
            Basics.next();
            Basics.block(BlockType.ozobot_go_to_next_intersection, Program.ID++);
        }

        /// <summary>
        /// Implemenation of waiting for 120 seconds.
        /// </summary>
        public static void wait()
        {
            Basics.next();
            Basics.block(BlockType.system_delay, Program.ID++);
            Program.sw.Write("<value name=\"{0}\">", ValueName.TIME_DELAY);
            Program.sw.Write("<block type=\"{0}\" id=\"{1}\">", BlockType.math_number, Program.ID++);
            Basics.field(FieldName.NUM, 120.ToString());
            Program.sw.Write("</block>");
            Program.sw.Write("</value>");
        }

        /// <summary>
        /// Controls if there is an obstacle and waits until it disappears.
        /// Repeats control every second.
        /// </summary>
        public static void WhileObstacle()
        {
            Basics.next();
            Basics.block(BlockType.controls_whileUntil, Program.ID++);
            Basics.field(FieldName.MODE, FieldMode.WHILE.ToString());
            Program.sw.Write("<value name=\"{0}\">", ValueName.BOOL);
            Program.sw.Write("<block type=\"{0}\" id=\"{1}\">", BlockType.logic_operation, Program.ID++);
            Basics.field(FieldName.OP, "OR");
            ValueCompare(ValueName.A, Sensors.LF);
            ValueCompare(ValueName.B, Sensors.RF);
            Program.sw.Write("</block>");
            Program.sw.Write("</value>");
            Turns.Statement();
        }

        /// <summary>
        /// Statement for WhileObstacle function.
        /// </summary>
        public static void Statement()
        {
            Program.sw.Write("<statement name=\"DO\">");
            Program.sw.Write("<block type=\"{0}\" id=\"{1}\">", BlockType.system_delay, Program.ID++);
            Program.sw.Write("<value name=\"{0}\">", ValueName.TIME_DELAY);
            Program.sw.Write("<block type=\"{0}\" id=\"{1}\">", BlockType.math_number, Program.ID++);
            Basics.field(FieldName.NUM, 100.ToString());
            Program.sw.Write("</block>");
            Program.sw.Write("</value>");
            Program.sw.Write("</block>");
            Program.sw.Write("</statement>");
        }

        /// <summary>
        /// Part of the WhileObstacle function.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sensor"></param>
        public static void ValueCompare(ValueName name, Sensors sensor)
        {
            Program.sw.Write("<value name=\"{0}\">", name);
            Program.sw.Write("<block type=\"{0}\" id=\"{1}\">", BlockType.logic_compare, Program.ID++);
            Basics.field(FieldName.OP, "GT");
            ValueBlock(ValueName.A, BlockType.ozobot_evo_read_proximity_sensor, FieldName.REGISTER, sensor.ToString());
            ValueBlock(ValueName.B, BlockType.variables_get, FieldName.VAR, Variables.x.ToString());
            Program.sw.Write("</block>");
            Program.sw.Write("</value>");
        }

        /// <summary>
        /// Part of the ValueCompare function.
        /// </summary>
        /// <param name="vName"></param>
        /// <param name="type"></param>
        /// <param name="fName"></param>
        /// <param name="value"></param>
        public static void ValueBlock(ValueName vName, BlockType type, FieldName fName, string value)
        {
            Program.sw.Write("<value name=\"{0}\">", vName);
            Program.sw.Write("<block type=\"{0}\" id=\"{1}\">", type, Program.ID++);
            Basics.field(fName, value);
            Program.sw.Write("</block>");
            Program.sw.Write("</value>");
        }
    }
}
