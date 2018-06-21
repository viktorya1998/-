using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Японский_кроссворд
{
    public abstract class Figure
    {
        static public Color Color_Line = Color.White;
        static public int Size_Line = 1;
        static public int Size_Сell = 15;

        protected Graphics Field;
        protected Figure(){}
        protected Figure(Graphics _Field)
        {
            Field = _Field;
        }

        public abstract void Draw(int a, int b, Color C);
    }
}