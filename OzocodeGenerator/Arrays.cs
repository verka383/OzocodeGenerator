using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzocodeGenerator
{
    static class Arrays
    {
        public static void ArrayDeclaration(int size)
        {
            Program.sw.Write("<block type=\"{0}\" id=\"{1}\">", BlockType.arrays_declaration, Program.ID++);
            Basics.field(FieldName.NAME, "cesty");
            Basics.field(FieldName.SIZE, size.ToString());
            Program.sw.Write("</block>");
        }

        public static void ArrayElementsSet(int ind, int val)
        {
            Program.sw.WriteLine("<block type=\"{0}\" id=\"{1}\">", BlockType.arrays_set_element, Program.ID++);
            Basics.field(FieldName.NAME, "cesty");
            Program.sw.WriteLine();
            arrayElement(ind, val);
            Program.tagsEnds.Push("</block>");
        }

        public static void arrayElement(int index, int value)
        {
            Basics.ValueWithMath(ValueName.INDEX, BlockType.math_number, index.ToString(), FieldName.NUM);
            Basics.ValueWithMath(ValueName.VALUE, BlockType.math_number, value.ToString(), FieldName.NUM);
        }
    }
}
