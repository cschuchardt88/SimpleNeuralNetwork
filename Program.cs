using System;

namespace ANN
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random(Environment.TickCount);
            NeNet nn = new NeNet(0.10d,
                new LayerSettings(2, null),
                new LayerSettings(3, new Sigmoid()),
                new LayerSettings(1, new Sigmoid())
            );

            double[][] inputs = new double[][]
            {
                new double[] { 0, 0, 0 },
                new double[] { 0, 1, 1 },
                new double[] { 1, 0, 1 },
                new double[] { 1, 1, 0 }
            };
            int epochMax = 5000000;
            double totalError = 0.00d;
            for (int epoch = 0; epoch <= epochMax; epoch++)
            {
                double[] input = inputs[rnd.Next(0, inputs.Length)];
                double[] output = nn.FeedForward(input[0], input[1]);
                totalError = nn.Train(input[input.Length - 1]);
                if (totalError == 0.00d) break;
            }

            double[] outputs = nn.FeedForward(0, 0);
            Console.WriteLine("Err: {0};", totalError);
            Console.WriteLine("{0} XOR {1} = {2}", 0, 0, outputs[0]);
            outputs = nn.FeedForward(0, 1);
            Console.WriteLine("{0} XOR {1} = {2}", 0, 1, outputs[0]);
            outputs = nn.FeedForward(1, 0);
            Console.WriteLine("{0} XOR {1} = {2}", 1, 0, outputs[0]);
            outputs = nn.FeedForward(1, 1);
            Console.WriteLine("{0} XOR {1} = {2}", 1, 1, outputs[0]);
            Console.WriteLine("Done!");
            Console.Read();
        }
    }
}
