using System.Collections.Generic;
using System.Diagnostics;

namespace BRNN
{
    public class InputNeuron : Neuron
    {
        List<Neuron> outputNeurons;
        double inputValue;

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

        public void SetInputValue(double inputValue)
        {
            this.inputValue = inputValue;
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
            values[epochNumber] += inputValue * inputWeights[0];
            if (Network.NeuronsHaveBias)
                values[epochNumber] += bias;
            Debug.WriteLine("=== Neuron '" + name + "', epoch = " + epochNumber + " ===");
            Debug.WriteLine("Aggregated value: " + values[epochNumber]);
        }

        public override void Activate(int epochNumber)
        {
            Debug.WriteLine("BIAS: " + bias);
            base.Activate(epochNumber);
            AggregateValues(epochNumber);
            ExecuteActivationFunction(epochNumber);
        }
    }
}
