using System.Collections.Generic;

namespace BRNN
{
    public class InputNeuron : Neuron
    {
        List<Neuron> outputNeurons;
        double[] inputVector;

        public InputNeuron() : base()
        {
            outputNeurons = new List<Neuron>();
            Network.AddInputNeuron(this);
        }

        public void SetInputVector(double[] inputVector)
        {
            this.inputVector = inputVector;
        }

        public void SetOutput(Neuron output)
        {
            outputNeurons.Add(output);
            output.SetInput(this);
        }

        private void AggregateValues(int epochNumber)
        {
            for (int i = 0; i < inputVector.Length; i++)
            {
                values[epochNumber] += inputVector[i] * inputWeights[i];
            }
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
            AggregateValues(epochNumber);
            ExecuteActivationFunction(epochNumber);
            PropagateSignal(epochNumber);
        }
    }
}
