using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Projekat_PR38_2019.Model
{
    public class MatrixPosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MatrixPosition() { }


        public MatrixPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (((MatrixPosition)obj).X == X && ((MatrixPosition)obj).Y == Y)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
