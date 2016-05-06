using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRNN
{
    class Program
    {
        public static double Perceptron(double value)
        {
            if (value >= 1)
                return 1;
            return 0;
        }

        public static double Func(double value)
        {
            return value;
        }

        public static void XOR()
        {
            double[,] values = { { 0, 0 }, { 1, 1 }, { 1, 0 }, { 0, 1 } }; // cztery kroki
            InputNeuron i1 = new InputNeuron(Perceptron, 1, 2, values, 4);
            i1.SetInputWeight(0, 0.6);
            i1.SetInputWeight(1, 0.6);
            InputNeuron i2 = new InputNeuron(Perceptron, 1, 2, values, 4);
            i2.SetInputWeight(0, 1.1);
            i2.SetInputWeight(1, 1.1);
            OutputNeuron o1 = new OutputNeuron(Perceptron, 2, 4);
            o1.SetInputWeight(0, -2);
            o1.SetInputWeight(1, 1.1);
            i1.SetOutput(o1);
            i2.SetOutput(o1);

            for (int i = 0; i < 4; i++)
            {
                i1.Activate(i);
                i2.Activate(i);
                Console.WriteLine(o1.GetValue(i));
            }
        }

        public static void Bidirectional()
        {
            double[,] values = { { 1 }, { 2 }, { 3 } };
            InputNeuron i1, i2;
            BidirectionalNeuron b1, b2, b3;
            OutputNeuron o;
            i1 = new InputNeuron(Func, 2, 1, values, 3); // USUNĄC STEPSCOUNT
            i2 = new InputNeuron(Func, 3, 1, values, 3);
            b1 = new BidirectionalNeuron(Func, 1, 2, 3);
            b2 = new BidirectionalNeuron(Func, 1, 2, 3);
            b3 = new BidirectionalNeuron(Func, 1, 1, 3);
            o = new OutputNeuron(Func, 3, 3);
            i1.SetOutput(b1);
            i1.SetOutput(b2);
            i2.SetOutput(b2);
            i2.SetOutput(b3);
            b1.SetOutput(o);
            b2.SetOutput(o);
            b3.SetOutput(o);

            for (int i = 0; i < 3; i++)
            {
                i1.Activate(i);
                i2.Activate(i);
                b2.Activate(i);
                b1.Activate(i);
                Console.WriteLine(o.GetValue(i));
            }
        }

        static void Main(string[] args)
        {
            //XOR();
            Bidirectional();
            Console.ReadKey();
        }
    }
}
