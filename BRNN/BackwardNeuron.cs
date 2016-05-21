using System.Collections.Generic;

namespace BRNN
{
    public class BackwardNeuron : OutputNeuron
    {
        List<Neuron> outputNeurons;
        double[] recurrentWeights;

        public BackwardNeuron()
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

        private double GetRecurrentValue(int epochNumber)
        {
            double value = 0.0;
            int currentWeightIndex = 0;
            for (int i = 1; i <= recurrentWeights.Length; i++)
            {
                int index = epochNumber + i;
                if (index == Network.EpochCount)
                    return value;
                value += values[index] * recurrentWeights[currentWeightIndex++];
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

        private bool IsLastEpoch(int epochNumber)
        {
            return epochNumber == Network.EpochCount - 1;
        }

        private void ResetDataNeededCount(int epochNumber)
        {
            dataNeededCount[epochNumber] = inputWeights.Count;
        }

        public override void Activate(int epochNumber)
        {
            base.Activate(epochNumber);
            if (wasActivated[epochNumber])
                return;
            if (!IsNeuronReady(epochNumber))
                return;
            if (!IsLastEpoch(epochNumber) && !Network.IsBackPropagation)
            {
                ResetDataNeededCount(epochNumber);
                return;
            }
            wasActivated[epochNumber] = true;
            AggregateValues(epochNumber);
            ExecuteActivationFunction(epochNumber);
            PropagateSignal(epochNumber);
        }
    }
}
