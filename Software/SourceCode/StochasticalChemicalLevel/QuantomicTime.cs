using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
   public class QuantomicTime
    {
        public double Quantoms { get; set; }//میگه برای این واکنش چند واحد زمانی لازمه
        public DrTirandazVoxel Voxel { get; set; }
        public int ReactionNumber_MustBeExecute { get; set; }
    }
}
