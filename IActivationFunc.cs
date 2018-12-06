using System;

namespace ANN
{
    public interface IActivationFunc
    {
        double Output(double x);
        double Derivative(double x);
    }
}
