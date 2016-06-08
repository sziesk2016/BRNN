using System.Collections.Generic;

namespace BRNN
{
    public class OutputNeuron : AbstractNeuron
    {
        public OutputNeuron() : base()
        {
            Initialize();
        }

        private void Initialize()
        {
            inputNeurons = new List<AbstractNeuron>();
            Network.OutputNeurons.Add(this);
        }

        protected override void AggregateValues(int epochNumber)
        {
            SummarizeInputs(epochNumber);
            base.AggregateValues(epochNumber);
        }
    }
}
