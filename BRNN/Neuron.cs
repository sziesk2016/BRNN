using System;
using System.Collections.Generic;

namespace BRNN
{
    public abstract class Neuron
    {
        protected List<double> values, inputWeights;
        protected Random random;
        protected double bias;
        List<int> dataNeededCount;

        public Neuron()
        {
            dataNeededCount = new List<int>();
            values = new List<double>();
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

        public void SetInputWeight(int index, double weight)
        {
            if (index == inputWeights.Count)
                inputWeights.Add(weight);
            inputWeights[index] = weight;
        }

        protected bool IsNeuronReady(int epochNumber)
        {
            if (epochNumber == dataNeededCount.Count)
                dataNeededCount.Add(inputWeights.Count);
            dataNeededCount[epochNumber]--;
            return dataNeededCount[epochNumber] == 0;
        }

        protected void ExecuteActivationFunction(int epochNumber)
        {
            values[epochNumber] = Network.ActivationFunction(values[epochNumber]);
        }

        public virtual void Activate(int epochNumber)
        {
            if (epochNumber == values.Count)
                values.Add(0.0);
        }

        public virtual void SetInput(Neuron input)
        {

        }
    }
}
