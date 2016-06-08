
namespace BRNN
{
    public class BackwardNeuron : AbstractRecurrentNeuron
    {
        public BackwardNeuron() : base()
        {
            Network.BackwardNeurons.Add(this);
        }

        protected override double GetRecurrentValue(int epochNumber)
        {
            double value = 0.0;
            int currentWeightIndex = 0;
            for (int i = 1; i <= Network.RecurrentWindowSize; i++)
            {
                int index = epochNumber + i;
                if (index == Network.EpochCount)
                    return value;
                value += values[index] * recurrentWeights[currentWeightIndex++];
            }
            return value;
        }
    }
}
