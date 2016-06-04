using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BRNN
{
    public static class Network
    {
        public static Func<double, double> ActivationFunction { get; set; }
        public static int RecurrentWindowSize { get; set; }
        public static bool NeuronsHaveBias { get; set; }
        public static int EpochCount { get; set; }
        public static bool IsBackPropagation { get; set; }
        private static List<InputNeuron> inputNeurons;
        private static List<OutputNeuron> outputNeurons;

        static Network()
        {
            ActivationFunction = DefaultActivationFunction;
            RecurrentWindowSize = 1;
            EpochCount = 0;
            NeuronsHaveBias = false;
            IsBackPropagation = false;
            inputNeurons = new List<InputNeuron>();
            outputNeurons = new List<OutputNeuron>();
        }

        public static void AddInputNeuron(InputNeuron inputNeuron)
        {
            inputNeurons.Add(inputNeuron);
        }

        public static void AddOutputNeuron(OutputNeuron outputNeuron)
        {
            outputNeurons.Add(outputNeuron);
        }

        public static double[] GetOutputVector(int epochNumber)
        {
            double[] values = new double[outputNeurons.Count];
            for (int i = 0; i < outputNeurons.Count; i++)
                values[i] = outputNeurons[i].GetValue(epochNumber);
            return values;
        }

        public static void DisplayOutputValues(int epochNumber)
        {
            double[] values = GetOutputVector(epochNumber);
            for (int i = 0; i < outputNeurons.Count; i++)
                Console.WriteLine(values[i]);
        }

        public static void Activate(int epochNumber)
        {

            for (int i = 0; i < inputNeurons.Count; i++)
                inputNeurons[i].Activate(epochNumber);
        }

        private static double DefaultActivationFunction(double value)
        {
            return value;
        }
    }
}