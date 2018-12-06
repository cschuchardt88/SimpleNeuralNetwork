using System;

namespace ANN
{
    public class Neuron
    {
        public Guid ID { get; private set; }

        private double ValueValue = 0.00d;
        public double Value
        {
            get
            {
                return this.ValueValue;
            }
            set
            {
                this.prevValue = this.ValueValue;
                this.ValueValue = value;
            }
        }
        public double prevValue { get; set; }

        private double BiasValue = 0.00d;
        public double Bias
        {
            get
            {
                return this.BiasValue;
            }
            set
            {
                this.prevBias = this.BiasValue;
                this.BiasValue = value;
            }
        }
        public double prevBias { get; private set; }

        private double DeltaValue = 0.00d;
        public double Delta
        {
            get
            {
                return this.DeltaValue;
            }
            set
            {
                this.prevDelta = this.DeltaValue;
                this.DeltaValue = value;
            }
        }
        public double prevDelta { get; private set; }
        public Weight[] Weights { get; set; }

        private Random rnd = new Random(Environment.TickCount);

        public Neuron()
        {
            this.ID = Guid.NewGuid();
            this.Bias = rnd.NextDouble();
        }

        public Neuron(int ConnectionSize) : this()
        {
            this.Weights = new Weight[ConnectionSize];
            for(int i = 0; i < this.Weights.Length; i++)
            {
                this.Weights[i] = new Weight();
                this.Weights[i].Value = rnd.NextDouble();
            }
        }

        public override string ToString()
    {
        string strout = String.Empty;
        strout += String.Format("Neuron: {0}\r\n", this.ID);
        strout += String.Format("Value: {0}\r\n", this.Value);
        strout += String.Format("Bias: {0}\r\n", this.Bias);
        strout += String.Format("prevBias: {0}\r\n", this.prevBias);
        strout += String.Format("Delta: {0}\r\n", this.Delta);
        strout += String.Format("prevDelta: {0}\r\n", this.prevDelta);
        if (this.Weights == null) return strout;
        if (this.Weights.Length == 0) return strout;
        strout += String.Format("Weights:\r\n");
        for(int i = 0; i < this.Weights.Length; i++)
        {
            strout += String.Format("\t{0} : [ Max: {1}, Min: {2}, Value: {3}, prevValue: {4}]\r\n", i, this.Weights[i].Max, this.Weights[i].Min, this.Weights[i].Value, this.Weights[i].prevValue);
        }
        return strout;
    }
    }
}