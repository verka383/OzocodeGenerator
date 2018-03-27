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
        ozobot_LED_colour_picker, system_turn_off_leds
    }

    enum FieldName
    {
        DIRECTION, VALUE, COLOUR
    }

    enum DIRECTION
    {
        DIRECTION_LEFT = 2, DIRECTION_RIGHT = 4, DIRECTION_FORWARD = 1, DIRECTION_BACKWARD = 8
    }

    enum ValueName
    {
        VALUE
    }

    /// <summary>
    /// When using you need to replace the leading "x" with "#". 
    /// </summary>
    enum LightColors
    {
        xffffff, xffff00, x8000ff, x00ffff, x80ff00, xff8000, xff00ff, x0080ff, x00ff00, xff0000, xff0077, x0000ff
    }
}
