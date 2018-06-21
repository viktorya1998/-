using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Японский_кроссворд
{
    public class Error : IАnswer
    {
        public void answer()
        {
            MessageBox.Show( "Кроссворд не решен, т.к в условие найдено противоречие", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}