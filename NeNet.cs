using System;
using System.Linq;

namespace ANN
{
    public class NeNet
    {
        public NeuralLayer[] Layers { get; set; }
        public double Alpha { get; private set; }

        public NeNet(double LearningRate, params LayerSettings[] netlayers)
        {
            this.Alpha = LearningRate;
            this.Layers = new NeuralLayer[netlayers.Length];
            for(int l = 0; l < this.Layers.Length; l++)
            {
                this.Layers[l] = new NeuralLayer(netlayers[l].NodeCount)
                {
                    ActivationFunc = netlayers[l].ActivationFunction
                };
                for(int i = 0; i < this.Layers[l].Neurons.Length; i++)
                {
                    if (l == 0) // input layer
                        this.Layers[l].Neurons[i] = new Neuron();
                    else // hidden and output layers
                        this.Layers[l].Neurons[i] = new Neuron(netlayers[l-1].NodeCount);
                }
            }
        }

        public double[] FeedForward(params double[] input)
        {
            NeuralLayer inputLayer = this.Layers[0];
            if (input.Length != inputLayer.Neurons.Length) throw new ApplicationException("Invalid length");
            // set input to neurons
            for(int i = 0; i < inputLayer.Neurons.Length; i++)
                inputLayer.Neurons[i].Value = input[i];
            
            for(int l = 1; l < this.Layers.Length; l++) // note: started at hidden layer
            {
                NeuralLayer hiddenLayer = this.Layers[l]; // ex: hidden
                NeuralLayer prehiddenLayer = this.Layers[l-1]; // ex: input
                for(int i = 0; i < hiddenLayer.Neurons.Length; i++)
                {
                    Neuron hiddenNeuron = hiddenLayer.Neurons[i];
                    hiddenNeuron.Value = 0.00d;
                    for(int j = 0; j < hiddenNeuron.Weights.Length; j++)
                    {
                        Neuron prehiddenNeuron = prehiddenLayer.Neurons[j];
                        hiddenNeuron.Value += hiddenNeuron.Weights[j].Value * prehiddenNeuron.Value;
                    }
                    hiddenNeuron.Value += hiddenNeuron.Bias;
                    hiddenNeuron.Value = hiddenLayer.ActivationFunc.Output(hiddenNeuron.Value);
                }
            }

            NeuralLayer outputLayer = this.Layers[this.Layers.Length - 1];
            return outputLayer.Neurons.Select(s => s.Value).ToArray();
        }

        public double Train(params double[] targets)
        {
            double dblerror = 0.00d;
            NeuralLayer outputLayer = this.Layers[this.Layers.Length - 1];
            for(int i = 0; i < outputLayer.Neurons.Length; i++)
            {
                Neuron outputNeuron = outputLayer.Neurons[i];
                outputNeuron.Delta = outputLayer.ActivationFunc.Derivative(outputNeuron.Value) * (targets[i] - outputNeuron.Value); // update cost of output layer
                dblerror += Math.Pow((targets[i] - outputNeuron.Value), 2); // calulate erorr
            }

            for(int l = this.Layers.Length - 1; l >= 1; l--)
            {
                NeuralLayer hiddenLayer = this.Layers[l]; // Current Layer ex: output
                NeuralLayer prehiddenLayer = this.Layers[l-1]; // previous layer ex: hidden
                for(int i = 0; i < hiddenLayer.Neurons.Length; i++)
                {
                    Neuron hiddenNeuron = hiddenLayer.Neurons[i];
                    for(int j = 0; j < hiddenNeuron.Weights.Length; j++)
                    {
                        Neuron prehiddenNeuron = prehiddenLayer.Neurons[j];
                        prehiddenNeuron.Delta += hiddenLayer.ActivationFunc.Derivative(prehiddenNeuron.Value) * hiddenNeuron.Weights[j].Value * hiddenNeuron.Delta; // update previous layers delta
                        hiddenNeuron.Weights[j].Value += this.Alpha * prehiddenNeuron.Value * hiddenNeuron.Delta; // update weights (hidden)
                    }
                    hiddenNeuron.Bias += this.Alpha * hiddenNeuron.Delta; // update bias (output)
                    hiddenNeuron.Delta = 0.00d; // clear current layer's delta (for next training past)
                }
            }

            return dblerror;
        }
    }
}