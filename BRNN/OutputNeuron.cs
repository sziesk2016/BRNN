using System.Collections.Generic;
using System.Diagnostics;

namespace BRNN
{
    public class OutputNeuron : Neuron
    {
        protected List<Neuron> inputNeurons;

        public OutputNeuron() : base()
        {
            Initialize();
        }

        public OutputNeuron(string name) : base(name)
        {
            Initialize();
        }

        private void Initialize()
        {
            inputNeurons = new List<Neuron>();
            Network.AddOutputNeuron(this);
        }

        public override void SetInput(Neuron input)
        {
            inputNeurons.Add(input);
            if (inputNeurons.Count > inputWeights.Count)
                inputWeights.Add(random.NextDouble());
        }

        protected virtual void AggregateValues(int epochNumber)
        {
            for (int i = 0; i < inputNeurons.Count; i++)
            {
                values[epochNumber] += inputNeurons[i].GetValue(epochNumber) * inputWeights[i];
            }
            if (Network.NeuronsHaveBias)
                values[epochNumber] += bias;
            Debug.WriteLine("=== Neuron '" + name + "', epoch = " + epochNumber + " ===");
            Debug.WriteLine("Aggregated value: " + values[epochNumber]);
        }

        public override void Activate(int epochNumber)
        {
            Debug.WriteLine("BIAS: " + bias);
            base.Activate(epochNumber);
            AggregateValues(epochNumber);
            ExecuteActivationFunction(epochNumber);
        }
    }
}
