using System;

namespace ANN
{
    public sealed class Relu : IActivationFunc
    {
        public double Output(double x) => Math.Max(0,x);
        public double Derivative(double x) => x < 0 ? 0 : 1;
    }
}
