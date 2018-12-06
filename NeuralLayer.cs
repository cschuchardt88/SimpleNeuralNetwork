using System;

namespace ANN
{
    public class NeuralLayer
    {
        public Neuron[] Neurons { get; set; }
        public IActivationFunc ActivationFunc { get; set; }

        public NeuralLayer()
        {

        }
        public NeuralLayer(int NodeSize) : this()
        {
            this.Neurons = new Neuron[NodeSize];
        }
    }
}