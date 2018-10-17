using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StochasticalChemicalLevel
{
    public interface ICellBody
    {
        void UpdateVoxels();
        void InitiateMolecularNumbers();
        void GetStepLengthAndDirection(out double stepLength, out double angle);
    }
}
