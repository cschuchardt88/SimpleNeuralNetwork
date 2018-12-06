using System;

namespace ANN
{
    public class Weight
    {
        public double Max { get; private set; }
        public double Min { get; private set; }
        public double prevValue { get; private set; }

        private double WeightValue = 0.00d;
        public double Value
        {
            get
            {
                return this.WeightValue;
            }
            set
            {
                this.Max = Math.Max(value, WeightValue);
                this.Min = Math.Min(value, WeightValue);
                this.prevValue = this.WeightValue;
                this.WeightValue = value;
            }
        }

        public override string ToString()
        {
            string strout = String.Empty;

            strout += String.Format("Max: {0}\r\n", this.Max);
            strout += String.Format("Min: {0}\r\n", this.Min);
            strout += String.Format("Value: {0}\r\n", this.Value);

            return strout;
        }
        
    }
}