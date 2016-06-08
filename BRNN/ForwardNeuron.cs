
namespace BRNN
{
    public class ForwardNeuron : AbstractRecurrentNeuron
    {
        public ForwardNeuron() : base()
        {
            Network.ForwardNeurons.Add(this);
        }

        protected override double GetRecurrentValue(int epochNumber)
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
    }
}
