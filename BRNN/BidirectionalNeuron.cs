using System;

namespace BRNN
{
    public class BidirectionalNeuron : Neuron
    {
        Neuron[] outputs, inputs;
        int[] dataNeededCount;
        double prevoiusWeight, nextWeight;
        int inputIndex, outputIndex;

        public BidirectionalNeuron(Func<double, double> activationFunction, int outputsCount, int inputsCount, int stepsCount = 1) : base(activationFunction, inputsCount, stepsCount)
        {
            outputs = new Neuron[outputsCount];
            inputs = new Neuron[inputsCount];
            dataNeededCount = new int[stepsCount];
            for (int i = 0; i < stepsCount - 1; i++)
            {
                dataNeededCount[i] = inputsCount + 1; // potrzebujemy dodatkowo informacji z przyszłości
            }
            dataNeededCount[stepsCount - 1] = inputsCount;
        }

        public void SetOutput(Neuron output)
        {
            outputs[outputIndex++] = output;
            output.SetInput(this);
        }

        public override void SetInput(Neuron input)
        {
            inputs[inputIndex++] = input;
        }

        public void SetWeight(WeightType weightType, double value)
        {
            if (weightType == WeightType.Previous)
                prevoiusWeight = value;
            else if (weightType == WeightType.Next)
                nextWeight = value;
        }

        public override void Activate(int step)
        {
            dataNeededCount[step]--;
            if (dataNeededCount[step] > 1) // wykonujmey gdy brakuje tylko informacji z następnego kroku
                return;
            double previousValue = 0, nextValue = 0;
            if (step > 0) // jeśli nie jest to pierwszy krok
            {
                previousValue = values[step - 1];
            }
            for (int i = 0; i < inputs.Length; i++)
            {
                values[step] += inputs[i].GetValue(step) * inputWeights[i];
            }
            values[step] += previousValue * prevoiusWeight + nextValue * nextWeight;
            values[step] = activationFunction(values[step]);
            if (dataNeededCount[step] > 0) // nie może aktywować wyjściowych neuronów dopóki nie będzie mieć wszystkich danych
                return;
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i].Activate(step);
            }
        }
    }

    public enum WeightType { Previous, Next }
}
