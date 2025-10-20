using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Calculator_.src
{
    [Flags]
    public enum Operators
    {
        None =      0x00000000,
        Multiply =  0x00000001,
        Divide =    0x00000002,
        Square =    0x00000004,
        Root =      0x00000008,
        Log =       0x00000010,
        Poly =      0x00000020,
        Percent =   0x00000040
    }
    public abstract class AOperator(Button val) : IOperator
    {
        private readonly Button myButton = val;

        public abstract double Evaluate<T>(List<T> args) where T : INumber<T>;
        public virtual void Enable()
        {
            this.myButton.IsEnabled = true;
        }
        public abstract double GetWeight();
    }
    interface IOperator
    {
        public abstract double Evaluate<T>(List<T> args) where T : INumber<T>;
        void Enable();
    }
}
