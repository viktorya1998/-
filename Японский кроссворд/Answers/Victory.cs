using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Японский_кроссворд
{
    public class Victory : IАnswer
    {
        public void answer()
        {
            MessageBox.Show("Кроссворд решен.", "ОК!", MessageBoxButtons.OK  );
        }
    }
}