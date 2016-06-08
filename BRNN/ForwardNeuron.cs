using System.Collections.Generic;
using System.Diagnostics;

namespace BRNN
{
    public class ForwardNeuron : Neuron
    {
        private List<Neuron> outputNeurons, inputNeurons;
        double[] recurrentWeights;

        public ForwardNeuron() : base()
        {
            Initialize();
        }

        public ForwardNeuron(string name) : base(name)
        {
            Initialize();
        }

        private void Initialize()
        {
            outputNeurons = new List<Neuron>();
            inputNeurons = new List<Neuron>();
            recurrentWeights = new double[Network.RecurrentWindowSize];
            for (int i = 0; i < Network.RecurrentWindowSize; i++)
                recurrentWeights[i] = random.NextDouble();
            Network.AddForwardNeuron(this);
        }

        public override void SetInput(Neuron input)
        {
            inputNeurons.Add(input);
            if (inputNeurons.Count > inputWeights.Count)
                inputWeights.Add(random.NextDouble());
        }

        public void SetOutput(params Neuron[] outputs)
        {
            for (int i = 0; i < outputs.Length; i++)
                SetSingleOutput(outputs[i]);
        }

        private void SetSingleOutput(Neuron output)
        {
            outputNeurons.Add(output);
            output.SetInput(this);
        }

        public void SetRecurrentWeight(int index, double value)
        {
            recurrentWeights[index] = value;
        }

        private double GetRecurrentValue(int epochNumber)
        {
            double value = 0.0;
            int currentWeightIndex = 0;
            for (int i = Network.RecurrentWindowSize; i > 0; i--)
            {
                int index = epochNumber - i;
                if (index < 0)
                    continue;
                value += values[index] * recurrentWeights[currentWeightIndex++];
            }
            return value;
        }

        protected void AggregateValues(int epochNumber)
        {
            for (int i = 0; i < inputNeurons.Count; i++)
            {
                values[epochNumber] += inputNeurons[i].GetValue(epochNumber) * inputWeights[i];
            }
            values[epochNumber] += GetRecurrentValue(epochNumber);
            if (Network.NeuronsHaveBias)
                values[epochNumber] += bias;
            Debug.WriteLine("=== Neuron '" + name + "', epoch = " + epochNumber + " ===");
            Debug.WriteLine("Aggregated value: " + values[epochNumber]);
        }

        public override void Activate(int epochNumber)
        {
            Debug.WriteLine("BIAS: " + bias);
            AggregateValues(epochNumber);
            ExecuteActivationFunction(epochNumber);
        }
    }
}
