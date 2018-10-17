using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Vafadar_GOL
{
    interface IWorld
    {
        void FillOneEmptyOtherOne();
        void Initiate(int r, int c, int dRows, int dCols);
    }
}
