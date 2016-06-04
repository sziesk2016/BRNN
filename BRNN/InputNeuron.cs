using System.Collections.Generic;
using System.Diagnostics;

namespace BRNN
{
    public class InputNeuron : Neuron
    {
        List<Neuron> outputNeurons;
        double[] inputVector;

        public InputNeuron() : base()
        {
            Initialize();
        }

        public InputNeuron(string name) : base(name)
        {
            Initialize();
        }

        private void Initialize()
        {
            outputNeurons = new List<Neuron>();
            Network.AddInputNeuron(this);
        }

        public void SetInputVector(double[] inputVector)
        {
            this.inputVector = inputVector;
        }

        public void SetOutput(params Neuron[] outputs)
        {
            for (int i = 0; i < outputs.Length; i++)
                SetSingleOutput(outputs[i]);
        }

        private void SetSingleOutput(Neuron output)
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
            values[epochNumber] += bias;
            Debug.WriteLine("=== Neuron '" + name + "', epoch = " + epochNumber + " ===");
            Debug.WriteLine("Aggregated value: " + values[epochNumber]);
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
            Debug.WriteLine("BIAS: " + bias);
            base.Activate(epochNumber);
            AggregateValues(epochNumber);
            ExecuteActivationFunction(epochNumber);
            PropagateSignal(epochNumber);
        }
    }
}
