using System.Collections.Generic;

namespace BRNN
{
    public class ForwardNeuron : OutputNeuron
    {
        List<Neuron> outputNeurons;
        double[] recurrentWeights;

        public ForwardNeuron(int outputsCount)
        {
            outputNeurons = new List<Neuron>();
            inputNeurons = new List<Neuron>();
            recurrentWeights = new double[Network.RecurrentWindowSize];
            for (int i = 0; i < Network.RecurrentWindowSize; i++)
                recurrentWeights[i] = random.NextDouble();
        }

        public void SetOutput(Neuron output)
        {
            outputNeurons.Add(output);
            output.SetInput(this);
        }

        public void SetRecurrentWeight(int index, double value)
        {
            recurrentWeights[index] = value;
        }

        private double GetRecurrentValue(int epochNumber) // możliwa optymalizacja
        {
            double value = 0.0;
            int currentWeight = 0;
            for (int i = Network.RecurrentWindowSize; i > 0; i--)
            {
                int index = epochNumber - i;
                if (index < 0)
                    continue;
                value += values[i] * recurrentWeights[currentWeight++];
            }
            return value;
        }

        protected override void AggregateValues(int epochNumber)
        {
            base.AggregateValues(epochNumber);
            values[epochNumber] += GetRecurrentValue(epochNumber);
        }

        private void PropagateSignal(int epochNumber)
        {
            for (int i = 0; i < outputNeurons.Count; i++)
            {
                outputNeurons[i].Activate(epochNumber);
            }
        }

        public override void Activate(int epochNumber)
        {
            base.Activate(epochNumber);
            if (!IsNeuronReady(epochNumber))
                return;
            AggregateValues(epochNumber);
            ExecuteActivationFunction(epochNumber);
            PropagateSignal(epochNumber);
        }
    }
}
