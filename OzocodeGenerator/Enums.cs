using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzocodeGenerator
{
    enum BlockType
    {
        ozobot_go_to_next_intersection, ozobot_choose_way_at_intersection, ozobot_stopMotors, ozobot_evo_say_colour, system_get_surface_color, ozobot_evo_say_direction_with_dropdown,
        ozobot_LED_colour_picker, system_turn_off_leds, system_delay, math_number, variables_set, controls_whileUntil, logic_operation, logic_compare, ozobot_evo_read_proximity_sensor,
        variables_get, arrays_declaration, arrays_set_element
    }

    enum FieldName
    {
        DIRECTION, VALUE, COLOUR, NUM, VAR, MODE, OP, REGISTER, NAME, SIZE
    }

    enum DIRECTION
    {
        DIRECTION_LEFT = 2, DIRECTION_RIGHT = 4, DIRECTION_FORWARD = 1, DIRECTION_BACKWARD = 8, WAIT = 0
    }

    enum ValueName
    {
        VALUE, TIME_DELAY, BOOL, A, B, INDEX
    }

    /// <summary>
    /// When using you need to replace the leading "x" with "#". 
    /// </summary>
    enum LightColors
    {
        xffffff, xffff00, x8000ff, x00ffff, x80ff00, xff8000, xff00ff, x0080ff, x00ff00, xff0000, xff0077, x0000ff
    }

    enum Variables
    {
        x
    }

    enum FieldMode
    {
        WHILE
    }

    enum Sensors
    {
        LF, RF
    }
}
