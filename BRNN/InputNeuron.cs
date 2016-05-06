using System;

namespace BRNN
{
    public class InputNeuron : Neuron
    {
        Neuron[] outputs;
        double[,] inputValues;
        int outputIndex;

        public InputNeuron(Func<double, double> activationFunction, int outputsCount, int inputsCount, double[,] inputValues, int stepsCount = 1) : base(activationFunction, inputsCount, stepsCount)
        {
            outputs = new Neuron[outputsCount];
            this.inputValues = inputValues;
        }

        public void SetOutput(Neuron output)
        {
            outputs[outputIndex++] = output;
            output.SetInput(this);
        }

        public override void Activate(int step)
        {
            for (int i = 0; i < inputWeights.Length; i++)
            {
                values[step] += inputValues[step, i] * inputWeights[i];
            }
            values[step] = activationFunction(values[step]);
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i].Activate(step);
            }
        }
    }
}
