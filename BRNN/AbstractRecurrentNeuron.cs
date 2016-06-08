using System.Collections.Generic;

namespace BRNN
{
    public abstract class AbstractRecurrentNeuron : AbstractNeuron
    {
        protected double[] recurrentWeights;

        public AbstractRecurrentNeuron() : base()
        {
            outputNeurons = new List<AbstractNeuron>();
            inputNeurons = new List<AbstractNeuron>();
            recurrentWeights = new double[Network.RecurrentWindowSize];
            for (int i = 0; i < Network.RecurrentWindowSize; i++)
                recurrentWeights[i] = random.NextDouble();
        }

        public void SetRecurrentWeight(int index, double value)
        {
            recurrentWeights[index] = value;
        }

        protected abstract double GetRecurrentValue(int epotchNumber);

        protected override void AggregateValues(int epochNumber)
        {
            SummarizeInputs(epochNumber);
            values[epochNumber] += GetRecurrentValue(epochNumber);
            base.AggregateValues(epochNumber);
        }
    }
}
