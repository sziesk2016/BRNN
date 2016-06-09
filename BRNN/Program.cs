using System;

namespace BRNN
{
    class Program
    {
        public static void DisplayOutputValues(int epochNumber)
        {
            double[] values = Network.GetOutputVector(epochNumber);
            for (int i = 0; i < Network.OutputNeurons.Count; i++)
                Console.WriteLine(values[i]);
            Console.WriteLine();
        }

        public static void BRNN()
        {
            Network.EpochCount = 3;
            Network.RecurrentWindowSize = 1;
            //Network.NeuronsHaveBias = true;

            double[][] values = new double[Network.EpochCount][];
            values[0] = new double[] { 1 };
            values[1] = new double[] { 0 };
            values[2] = new double[] { 1 };

            Network.Values = values;

            InputNeuron i1 = new InputNeuron();
            i1.Name = "I1";

            ForwardNeuron f1 = new ForwardNeuron();
            f1.Name = "F1";

            BackwardNeuron b1 = new BackwardNeuron();
            b1.Name = "B1";

            OutputNeuron o1 = new OutputNeuron();
            o1.Name = "O1";

            i1.SetOutput(f1, b1);

            f1.SetOutput(o1);
            b1.SetOutput(o1);

            i1.SetInputWeight(1);
            i1.SetBias(1);

            f1.SetInputWeight(2);
            b1.SetInputWeight(2);
            f1.SetRecurrentWeight(0, 1);
            b1.SetRecurrentWeight(0, 1);
            f1.SetBias(1);
            b1.SetBias(1);

            o1.SetInputWeight(0, 2);
            o1.SetInputWeight(1, 2);
            o1.SetBias(1);

            try
            {
                Network.Activate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            for (int epochNumber = 0; epochNumber < Network.EpochCount; epochNumber++)
            {
                DisplayOutputValues(epochNumber);
            }
        }

        public static void BRNN2(double weight)
        {
            Network.EpochCount = 5;
            Network.RecurrentWindowSize = 3;

            double[][] values = new double[Network.EpochCount][];
            values[0] = new double[] { 1, 0 };
            values[1] = new double[] { 0, 0 };
            values[2] = new double[] { 1, 1 };
            values[3] = new double[] { 1, 1 };
            values[4] = new double[] { 0, 1 };

            Network.Values = values;

            InputNeuron[] inputs = new InputNeuron[]
            {
                new InputNeuron() { Name = "I1" },
                new InputNeuron() { Name = "I2" }
            };

            inputs[0].SetInputWeight(1);
            inputs[1].SetInputWeight(0.5);

            ForwardNeuron[] forwards = new ForwardNeuron[]
            {
                new ForwardNeuron() { Name = "F1" },
                new ForwardNeuron() { Name = "F2" },
                new ForwardNeuron() { Name = "F3" }
            };

            forwards[0].SetInputWeight(1);
            forwards[1].SetInputWeight(weight);
            forwards[2].SetInputWeight(0.5);

            BackwardNeuron[] backwards = new BackwardNeuron[]
            {
                new BackwardNeuron() { Name = "B1" },
                new BackwardNeuron() { Name = "B2" },
                new BackwardNeuron() { Name = "B3" }
            };

            backwards[0].SetInputWeight(1.5);
            backwards[1].SetInputWeight(1);
            backwards[2].SetInputWeight(2);
            backwards[2].SetInputWeight(1, 3);

            OutputNeuron[] outputs = new OutputNeuron[]
            {
                new OutputNeuron() { Name = "O1" },
                new OutputNeuron() { Name = "O2" }
            };

            outputs[0].SetInputWeight(1);
            outputs[0].SetInputWeight(1, 2);
            outputs[1].SetInputWeight(1);
            outputs[1].SetInputWeight(1, 0.25);
            outputs[1].SetInputWeight(2, 0.75);

            inputs[0].SetOutput(forwards[0], backwards[0]);
            inputs[1].SetOutput(forwards[1], backwards[1]);
            forwards[0].SetOutput(forwards[2]);
            backwards[0].SetOutput(backwards[2]);
            forwards[1].SetOutput(outputs[1]);
            backwards[1].SetOutput(backwards[2]);
            forwards[2].SetOutput(outputs[0], outputs[1]);
            backwards[2].SetOutput(outputs[0], outputs[1]);

            Network.ActivationFunction = ActivationFunctionSet.Sigmoid;

            Network.Activate();

            for (int epochNumber = 0; epochNumber < Network.EpochCount; epochNumber++)
            {
                Console.WriteLine("=== Epoch = " + (epochNumber + 1) + " ===");
                DisplayOutputValues(epochNumber);
            }
        }

        private static void Main(string[] args)
        {
            while (true)
            {
                double i = Double.Parse(Console.ReadLine());
                BRNN2(i);
                Network.Restart();
            }
            Console.ReadKey();
        }
    }
}
