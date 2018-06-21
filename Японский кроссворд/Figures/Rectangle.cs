using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Японский_кроссворд
{
    class Rectangle : Figure
    {
        public Point Corner1{get; set;}

        public Point Corner2{get; set;}

        public Rectangle(Graphics G) : base(G) {}

        public override void Draw(int x, int y, Color C)
        {
            Field.FillRectangle(new SolidBrush(C), x, y, Size_Сell, Size_Сell);
            Field.DrawRectangle(new Pen(Color_Line, Size_Line), x, y, Size_Сell, Size_Сell);
        }
    }
}