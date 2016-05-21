using System;
using System.Collections.Generic;

namespace BRNN
{
    public class OutputNeuron : Neuron
    {
        protected List<Neuron> inputNeurons;

        public OutputNeuron() : base()
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
        }

        public override void Activate(int epochNumber)
        {
            base.Activate(epochNumber);
            if (wasActivated[epochNumber])
                return;
            if (!IsNeuronReady(epochNumber))
                return;
            wasActivated[epochNumber] = true;
            AggregateValues(epochNumber);
            ExecuteActivationFunction(epochNumber);
        }
    }
}
