using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRNN
{
    public class Synapse
    {
        public double Value { get; set; }

        public Synapse()
        {
            Value = 0.0;
        }

        public Synapse(double value)
        {
            Value = value;
        }
    }
}
