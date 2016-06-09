﻿using System;

namespace BRNN
{
    class Program
    {
        public static void DisplayOutputValues(int epochNumber)
        {
            double[] values = Network.GetOutputVector(epochNumber);
            for (int i = 0; i < Network.OutputNeurons.Count; i++)
                Console.WriteLine(values[i]);
        }

        public static void BRNN()
        {
            Network.EpochCount = 3;
            Network.RecurrentWindowSize = 1;
            Network.NeuronsHaveBias = true;

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

        private static void Main(string[] args)
        {
            BRNN();
            Console.ReadKey();
        }
    }
}
