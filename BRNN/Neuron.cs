using System;

namespace BRNN
{
    public abstract class Neuron
    {
        protected Func<double, double> activationFunction; // delegacja na funkcję aktywacji
        protected double[] values, inputWeights; // values - wartości neuronu w poszczególnych krokach, inputWeights - wagi na wejściu

        public Neuron(Func<double, double> activationFunction, int inputsCount, int stepsCount)
        {
            this.activationFunction = activationFunction;
            values = new double[stepsCount];
            inputWeights = new double[inputsCount];
        }

        public double GetValue(int step)
        {
            return values[step];
        }

        public void SetInputWeight(int index, double weight)
        {
            inputWeights[index] = weight;
        }

        public abstract void Activate(int step);

        public virtual void SetInput(Neuron input)
        {

        }
    }
}
