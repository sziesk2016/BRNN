using System;
using System.Collections;
using System.Collections.Generic;

namespace BRNN
{
    public static class Network
    {
        public static Func<double, double> ActivationFunction { get; set; }
        public static int RecurrentWindowSize { get; set; }
        public static int EpochCount { get; set; }
        public static double[][] Values { get; set; }
        public static bool NeuronsHaveBias { get; set; }
        public static List<InputNeuron> InputNeurons { get; set; }
        public static List<OutputNeuron> OutputNeurons { get; set; }
        public static List<ForwardNeuron> ForwardNeurons { get; set; }
        public static List<BackwardNeuron> BackwardNeurons { get; set; }

        static Network()
        {
            ActivationFunction = ActivationFunctionSet.DefaultActivationFunction;
            RecurrentWindowSize = 1;
            EpochCount = 0;
            NeuronsHaveBias = false;
            InputNeurons = new List<InputNeuron>();
            OutputNeurons = new List<OutputNeuron>();
            ForwardNeurons = new List<ForwardNeuron>();
            BackwardNeurons = new List<BackwardNeuron>();
        }

        public static void Restart()
        {
            InputNeurons.Clear();
            OutputNeurons.Clear();
            ForwardNeurons.Clear();
            BackwardNeurons.Clear();
        }

        public static double[] GetOutputVector(int epochNumber)
        {
            double[] values = new double[OutputNeurons.Count];
            for (int i = 0; i < OutputNeurons.Count; i++)
                values[i] = OutputNeurons[i].GetValue(epochNumber);
            return values;
        }

        private static void SetInputValues(int epochNumber)
        {
            Console.WriteLine("SIZE: " + InputNeurons.Count);
            for (int i = 0; i < InputNeurons.Count; i++)
                InputNeurons[i].SetInputValue(Values[epochNumber][i]);
        }

        private static void ActivateNeurons(IEnumerable neurons, int epochNumber)
        {
            foreach (var neuron in neurons)
                ((AbstractNeuron)neuron).Activate(epochNumber);
        }

        public static void Activate()
        {
            if (Values == null)
            {
                throw new ArgumentNullException("Values", "Please provide input values");
            }
            for (int epochNumber = 0; epochNumber < EpochCount; epochNumber++)
            {
                SetInputValues(epochNumber);
                ActivateNeurons(InputNeurons, epochNumber);
                ActivateNeurons(ForwardNeurons, epochNumber);
            }
            for (int epochNumber = EpochCount - 1; epochNumber >= 0; epochNumber--)
            {
                ActivateNeurons(BackwardNeurons, epochNumber);
                ActivateNeurons(OutputNeurons, epochNumber);
            }
        }
    }
}