using System;

namespace BRNN
{
    public static class ActivationFunctionSet
    {
        public static double DefaultActivationFunction(double value)
        {
            return value;
        }

        public static double Threshold(double value)
        {
            if (value >= 1)
                return 1;
            return 0;
        }

        public static double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
    }
}
