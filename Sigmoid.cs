using System;

namespace ANN
{
    public sealed class Sigmoid : IActivationFunc
    {
        public double Output(double x) => 1 / (1 + Math.Exp(-x));
        public double Derivative(double x) => x * (1 - x);
    }
}
