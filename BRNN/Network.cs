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
        public static double[][] Values { get; set; }
        private static List<InputNeuron> inputNeurons;
        private static List<OutputNeuron> outputNeurons;
        private static List<ForwardNeuron> forwardNeurons;
        private static List<BackwardNeuron> backwardNeurons;

        static Network()
        {
            ActivationFunction = DefaultActivationFunction;
            RecurrentWindowSize = 1;
            EpochCount = 0;
            NeuronsHaveBias = false;
            inputNeurons = new List<InputNeuron>();
            outputNeurons = new List<OutputNeuron>();
            forwardNeurons = new List<ForwardNeuron>();
            backwardNeurons = new List<BackwardNeuron>();
        }

        public static void AddInputNeuron(InputNeuron inputNeuron)
        {
            inputNeurons.Add(inputNeuron);
        }

        public static void AddOutputNeuron(OutputNeuron outputNeuron)
        {
            outputNeurons.Add(outputNeuron);
        }

        public static void AddForwardNeuron(ForwardNeuron forwardNeuron)
        {
            forwardNeurons.Add(forwardNeuron);
        }

        public static void AddBackWardNeuron(BackwardNeuron backwardNeuron)
        {
            backwardNeurons.Add(backwardNeuron);
        }

        private static double[] GetOutputVector(int epochNumber)
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

        private static void ActivateInputNeurons(int epochNumber)
        {
            for (int i = 0; i < inputNeurons.Count; i++)
                inputNeurons[i].Activate(epochNumber);
        }

        private static void ActivateForwardNeurons(int epochNumber)
        {
            for (int i = 0; i < forwardNeurons.Count; i++)
                forwardNeurons[i].Activate(epochNumber);
        }

        private static void ActivateBacwardNeurons(int epochNumber)
        {
            for (int i = 0; i < backwardNeurons.Count; i++)
                backwardNeurons[i].Activate(epochNumber);
        }

        private static void ActivateOutputNeurons(int epochNumber)
        {
            for (int i = 0; i < outputNeurons.Count; i++)
                outputNeurons[i].Activate(epochNumber);
        }

        private static void SetInputValies(int epochNumber)
        {
            for (int i = 0; i < inputNeurons.Count; i++)
                inputNeurons[i].SetInputValue(Values[epochNumber][i]);
        }

        public static void Activate()
        {
            if (Values == null)
            {
                throw new ArgumentNullException("Values", "Please provide input values");
            }
            for (int epochNumber = 0; epochNumber < EpochCount; epochNumber++)
            {
                SetInputValies(epochNumber);
                ActivateInputNeurons(epochNumber);
                ActivateForwardNeurons(epochNumber);
            }
            for (int epochNumber = EpochCount - 1; epochNumber >= 0; epochNumber--)
            {
                ActivateBacwardNeurons(epochNumber);
                ActivateOutputNeurons(epochNumber);
            }
        }

        private static double DefaultActivationFunction(double value)
        {
            return value;
        }
    }
}