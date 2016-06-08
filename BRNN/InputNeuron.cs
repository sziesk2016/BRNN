using System.Collections.Generic;

namespace BRNN
{
    public class InputNeuron : AbstractNeuron
    {
        double inputValue;

        public InputNeuron() : base()
        {
            Initialize();
        }

        private void Initialize()
        {
            outputNeurons = new List<AbstractNeuron>();
            Network.InputNeurons.Add(this);
        }

        public void SetInputValue(double inputValue)
        {
            this.inputValue = inputValue;
        }

        protected override void AggregateValues(int epochNumber)
        {
            values[epochNumber] += inputValue * inputWeights[0];
            base.AggregateValues(epochNumber);
        }
    }
}
