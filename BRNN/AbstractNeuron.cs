using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BRNN
{
    public abstract class AbstractNeuron
    {
        public string Name { get; set; }
        public Func<double, double> ActivationFunction { get; set; }
        protected List<AbstractNeuron> inputNeurons, outputNeurons;
        protected List<double> inputWeights;
        protected double[] values;
        protected Random random;
        protected double bias;

        protected AbstractNeuron()
        {
            ActivationFunction = null;
            values = new double[Network.EpochCount];
            inputWeights = new List<double>();
            random = new Random();
            if (Network.NeuronsHaveBias)
                bias = random.NextDouble();
        }

        public void SetBias(double value)
        {
            bias = value;
        }

        public double GetValue(int epochNumber)
        {
            return values[epochNumber];
        }

        public void SetInputWeight(double weight)
        {
            SetInputWeight(0, weight);
        }

        public void SetInputWeight(int index, double weight)
        {
            if (index == inputWeights.Count)
                inputWeights.Add(weight);
            inputWeights[index] = weight;
        }

        protected void ExecuteActivationFunction(int epochNumber)
        {
            if (ActivationFunction != null)
                values[epochNumber] = ActivationFunction(values[epochNumber]);
            else
                values[epochNumber] = Network.ActivationFunction(values[epochNumber]);
            Debug.WriteLine("Activation function: " + values[epochNumber]);
        }

        protected virtual void SummarizeInputs(int epochNumber)
        {
            for (int i = 0; i < inputNeurons.Count; i++)
            {
                values[epochNumber] += inputNeurons[i].GetValue(epochNumber) * inputWeights[i];
            }
        }


        public void SetOutput(params AbstractNeuron[] outputs)
        {
            for (int i = 0; i < outputs.Length; i++)
                SetSingleOutput(outputs[i]);
        }

        private void SetSingleOutput(AbstractNeuron output)
        {
            outputNeurons.Add(output);
            output.SetInput(this);
        }

        protected virtual void AggregateValues(int epochNumber)
        {
            if (Network.NeuronsHaveBias)
                values[epochNumber] += bias;
            Debug.WriteLine("=== Neuron '" + Name + "', epoch = " + epochNumber + " ===");
            Debug.WriteLine("Aggregated value: " + values[epochNumber]);
        }

        public void Activate(int epochNumber)
        {
            Debug.WriteLine("BIAS: " + bias);
            AggregateValues(epochNumber);
            ExecuteActivationFunction(epochNumber);
        }

        public void SetInput(AbstractNeuron input)
        {
            inputNeurons.Add(input);
            if (inputNeurons.Count > inputWeights.Count)
                inputWeights.Add(random.NextDouble());
        }
    }
}
