using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzocodeGenerator
{
    /// <summary>
    /// The basic blocks for generation of simple path.
    /// </summary>
    static class Basics
    {
        public static void xml()
        {
            Program.sw.Write("<xml xmlns=\"http://www.w3.org/1999/xhtml\">");
            Program.tagsEnds.Push("</xml>");
        }

        public static void next()
        {
            Program.sw.Write("<next>");
            Program.tagsEnds.Push("</next>");
        }

        public static void block(BlockType type, int id)
        {
            Program.sw.Write("<block type=\"{0}\" id=\"{1}\">", type, id);
            Program.tagsEnds.Push("</block>");
        }

        /// <summary>
        /// Writes block that is immediately closed.
        /// </summary>
        public static void blockClosed(BlockType type, int id)
        {
            Program.sw.Write("<block type=\"{0}\" id=\"{1}\">", type, id);
            Program.sw.Write("</block>");
        }

        public static void field(FieldName name, string value)
        {
            Program.sw.Write("<field name=\"{0}\">{1}</field>", name, value);
        }

        public static void value(ValueName name)
        {
            Program.sw.Write("<value name=\"{0}\">", name);
            Basics.blockClosed(BlockType.system_get_surface_color, Program.ID++);
            Program.sw.Write("</value>");
        }

        /// <summary>
        /// Write ends of the tags.
        /// </summary>
        public static void PopTagsEnds()
        {
            while (Program.tagsEnds.Count > 0)
            {
                Program.sw.Write(Program.tagsEnds.Pop());
            }
        }
    }
}
