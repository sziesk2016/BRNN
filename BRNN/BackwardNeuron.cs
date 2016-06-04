using System.Collections.Generic;
using System.Diagnostics;

namespace BRNN
{
    public class BackwardNeuron : Neuron
    {
        private List<Neuron> outputNeurons, inputNeurons;
        double[] recurrentWeights;

        public BackwardNeuron() : base()
        {
            Initialize();
        }

        public BackwardNeuron(string name) : base(name)
        {
            Initialize();
        }

        private void Initialize()
        {
            outputNeurons = new List<Neuron>();
            inputNeurons = new List<Neuron>();
            recurrentWeights = new double[Network.RecurrentWindowSize];
            for (int i = 0; i < Network.RecurrentWindowSize; i++)
                recurrentWeights[i] = random.NextDouble();
        }

        public override void SetInput(Neuron input)
        {
            inputNeurons.Add(input);
            if (inputNeurons.Count > inputWeights.Count)
                inputWeights.Add(random.NextDouble());
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

        protected void AggregateValues(int epochNumber)
        {
            for (int i = 0; i < inputNeurons.Count; i++)
            {
                values[epochNumber] += inputNeurons[i].GetValue(epochNumber) * inputWeights[i];
            }
            values[epochNumber] += GetRecurrentValue(epochNumber);
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
            Debug.WriteLine("BIAS: " + bias);
            if (epochNumber == values.Count)
            {
                values.Add(0.0);
                wasActivated.Add(false);
            }
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
