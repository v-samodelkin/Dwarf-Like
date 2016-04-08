using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public class Attribute
    {
        public Attribute()
        {
            current = max = 0;
        }
        public Attribute(double current, double max)
        {
            Max = max;
            Current = current;
        }


        private double max;
        public double Max
        {
            get
            {
                return max;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Аргумент не может быть меньше нуля");
                if (Current > value)
                    Current = value;
                max = value;

            }
        }

        private double current;
        public double Current
        {
            get
            {
                return current;
            }
            set
            {
                if ((value > Max && !(Max == 0))|| value < 0)
                    throw new ArgumentOutOfRangeException(String.Format("Аргумент должен лежать в [0:{1}]", Max));
                current = value;
            }
        }
    }
}
