using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BRNN
{
    public abstract class Neuron
    {
        protected List<bool> wasActivated;
        protected List<double> values, inputWeights;
        protected Random random;
        protected double bias;        
        protected List<int> dataNeededCount;
        protected string name;

        protected Neuron()
        {
            name = String.Empty;
            Initialize();
        }

        protected Neuron(string name)
        {
            this.name = name;
            Initialize();
        }

        private void Initialize()
        {
            dataNeededCount = new List<int>();
            wasActivated = new List<bool>();
            values = new List<double>();
            inputWeights = new List<double>();
            random = new Random();
            if (Network.NeuronsHaveBias)
                bias = random.NextDouble();
            else
                bias = 0;
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
            Debug.WriteLine("Activation function: " + values[epochNumber]);
        }

        public virtual void Activate(int epochNumber)
        {
            if (epochNumber == values.Count)
            {
                values.Add(0.0);
                wasActivated.Add(false);
            }
        }

        public virtual void SetInput(Neuron input)
        {

        }
    }
}
