using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Японский_кроссворд
{
    public class Loss : IАnswer
    {
        public void answer()
        {
            MessageBox.Show("Кроссворд не решен, т.к имеет множество решение.", "NO!", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}