using System;

namespace BRNN
{
    class Program
    {
        // Funkcja progowa
        public static double Threshold(double value)
        {
            if (value >= 1)
                return 1;
            return 0;
        }

        public static void XOR()
        {
            /* Realizacja tego przykładu:
             * https://www.youtube.com/watch?v=AuEz4Ul9tHM
             */

            // Ustawiamy funkcję aktywacji dla wszystkich neuronów, można zakomentować i przetestować co się stanie
            Network.ActivationFunction = Threshold;

            // Tablica wektorów wejściowych, mamy 4 epoki, a więc cztery wiersze. W każdej epoce podajemy dwa wejścia
            double[][] values = new double[4][];
            values[0] = new double[] { 0, 0 };
            values[1] = new double[] { 0, 1 };
            values[2] = new double[] { 1, 0 };
            values[3] = new double[] { 1, 1 };

            // Tworzymy sieć składającą się z dwóch neuronów w warstwie wejściowej i jednego w warstwie wyjściowej
            InputNeuron i1 = new InputNeuron();
            InputNeuron i2 = new InputNeuron();
            OutputNeuron o1 = new OutputNeuron();

            // Ustawiamy wagi na wejściach, pierwszy argument oznacza numer wejścia od lewej, drugi wartość wagi. Jest to operacja opcjonalna wykorzystywana
            // tylko do budowy znanej już wcześniej sieci.
            i1.SetInputWeight(0, 0.6);
            i1.SetInputWeight(1, 0.6);
            
            i2.SetInputWeight(0, 1.1);
            i2.SetInputWeight(1, 1.1);
            
            o1.SetInputWeight(0, -2);
            o1.SetInputWeight(1, 1.1);

            // Łączymy neurony, należy podać tylko wyjścia do innych neuronów
            i1.SetOutput(o1);
            i2.SetOutput(o1);

            // Przechodzimy przez wszystkie cztery epoki
            for (int epochNumber = 0; epochNumber < 4; epochNumber++)
            {
                // Podajemy do neuronów wejściowych wektory wejściowe
                i1.SetInputVector(values[epochNumber]);
                i2.SetInputVector(values[epochNumber]);
                // Aktywujemy całą sieć
                Network.Activate(epochNumber);
                // Pobieramy wektor wyjściowy
                double[] valuesVector = Network.GetOutputVector(epochNumber);
                // W warstwie wyjściowej mamy tylko jeden neuron, a więc długość wektora wyjściowego wynosi tylko 1. Wyświetlamy pierwszy element wektora.
                Console.WriteLine(valuesVector[0].ToString());
            }
        }

        static void Main(string[] args)
        {
            XOR();
            Console.ReadKey();
        }
    }
}
