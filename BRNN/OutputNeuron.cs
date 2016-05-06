using System;

namespace BRNN
{
    public class OutputNeuron : Neuron
    {
        Neuron[] inputs;
        int[] dataNeededCount;
        int inputIndex;

        public OutputNeuron(Func<double, double> activationFunction, int inputsCount, int stepsCount = 1) : base(activationFunction, inputsCount, stepsCount)
        {
            inputs = new Neuron[inputsCount];
            dataNeededCount = new int[stepsCount];
        }

        public override void SetInput(Neuron input)
        {
            inputs[inputIndex++] = input;
        }

        public override void Activate(int step)
        {
            dataNeededCount[step]--;
            if (dataNeededCount[step] > 0) // jeśli do neuronu nie dotarły jeszcze wszystkie niezbędne informacje
                return;
            for (int i = 0; i < inputs.Length; i++)
            {
                values[step] += inputs[i].GetValue(step) * inputWeights[i];
            }
            values[step] = activationFunction(values[step]);
            if (step == values.Length)
                step = 0;
        }
    }
}
