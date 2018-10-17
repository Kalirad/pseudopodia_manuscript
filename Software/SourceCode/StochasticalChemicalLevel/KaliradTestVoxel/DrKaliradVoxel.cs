using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
  public class DrKaliradVoxel
    {
        public int A { get; set; } 
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }

        public static double Area { get; set; }
        public static double Volume { get; internal set; }
        public static double Size { get; internal set; }

        public bool IsLeftBoundry { get; internal set; } = false;
        public bool IsRightBoundry { get; internal set; } = false;
        public bool IsTopBoundry { get; internal set; } = false;
        public bool IsBottonBoundry { get; internal set; } = false;

        public int Row { get; set; }
        public int Col { get; set; }
        public double QuantomixClock { get; set; }
    }
}
