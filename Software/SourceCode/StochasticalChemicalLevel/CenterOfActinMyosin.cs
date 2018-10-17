using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
    public class CenterOfActinMyosin
    {
        public double acmulatedOfActin_X = 0;
        public double acmulatedOfActin_Y = 0;
        public double actin_W = 0;

        public double acmulatedOfMyosin_X = 0;
        public double acmulatedOfMyosin_Y = 0;
        public double myosin_W = 0;

        public double CenterOfActin_x
        {
            get { return acmulatedOfActin_X / actin_W; }
        }
        public double CenterOfActin_Y
        {
            get { return acmulatedOfActin_Y / actin_W; }
        }

        public double CenterOfMyosin_x
        {
            get { return acmulatedOfMyosin_X / myosin_W; }
        }
        public double CenterOfMyosin_Y
        {
            get { return acmulatedOfMyosin_Y / myosin_W; }
        }

        public double actinMyosinLocationID { get { return 1000 * Math.Floor(CenterOfActin_x) + 100 * Math.Floor(CenterOfActin_Y) + 10 * Math.Floor(CenterOfMyosin_x) + Math.Floor(CenterOfMyosin_Y); } }
    }
}
