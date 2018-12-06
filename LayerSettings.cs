using System;
using System.Collections.Generic;
using System.Text;

namespace ANN
{
    public class LayerSettings
    {
        public int NodeCount { get; private set; }
        public IActivationFunc ActivationFunction { get; private set; }

        public LayerSettings()
        {

        }

        public LayerSettings(int nodeSize, IActivationFunc ActFunc) : this()
        {
            this.NodeCount = nodeSize;
            this.ActivationFunction = ActFunc;
        }
    }
}
