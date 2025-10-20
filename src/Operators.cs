using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Calculator_.src
{
    class Add(Button val) : AOperator(val)
    {
        public override double Evaluate<T>(List<T> args)
        {
            if (args.Count != 2)
            {
                throw new UseMeCorrectlyException();
            }
            double? res1 = args[0] as double?;
            double? res2 = args[1] as double?;
            return (double)(res1! + res2!);
        }
        public override double GetWeight()
        {
            return 1;
        }
    }
    class Subtract(Button val) : AOperator(val)
    {
        public override double Evaluate<T>(List<T> args)
        {
            if (args.Count != 2)
            {
                throw new UseMeCorrectlyException();
            }
            double? res1 = args[0] as double?;
            double? res2 = args[1] as double?;
            return (double)(res1! - res2!);
        }
        public override double GetWeight()
        {
            return 2;
        }
    }
    class Multiply(Button val) : AOperator(val)
    {
        public override double Evaluate<T>(List<T> args)
        {
            if (args.Count != 2)
            {
                throw new UseMeCorrectlyException();
            }
            double? res1 = args[0] as double?;
            double? res2 = args[1] as double?;
            return (double)(res1! * res2!);
        }
        public override double GetWeight()
        {
            return 4;
        }
    }
    class Divide(Button val) : AOperator(val)
    {
        public override double Evaluate<T>(List<T> args)
        {
            if (args.Count != 2)
            {
                throw new UseMeCorrectlyException();
            }
            double? res1 = args[0] as double?;
            double? res2 = args[1] as double?;
            if ((res2!) == 0)
            {
                MessageBox.Show("Cannot divide by zero");
                return 0;
            }
            return (double)(res1! / res2!);
        }
        public override double GetWeight()
        {
            return 6;
        }
    }
    class Percent(Button val) : AOperator(val)
    {
        public override double Evaluate<T>(List<T> args)
        {
            if (args.Count != 2)
            {
                throw new UseMeCorrectlyException();
            }
            double? res1 = args[0] as double?;
            double? res2 = args[1] as double?;
            return ((double)res1! / 100) * (double)res2!;
        }
        public override double GetWeight()
        {
            return 10;
        }
    }
    class Square(Button val) : AOperator(val)
    {
        public override double Evaluate<T>(List<T> args)
        {
            throw new NotImplementedException();
        }
        public override double GetWeight()
        {
            return 25;
        }
    }
    class Root(Button val) : AOperator(val)
    {
        public override double Evaluate<T>(List<T> args)
        {
            if (args.Count != 2)
            {
                throw new UseMeCorrectlyException();
            }
            double? res1 = args[0] as double?;
            double? res2 = args[1] as double?;
            return Math.Pow((double)res1!, 1 / (double)res2!);
        }
        public override double GetWeight()
        {
            return 30;
        }
    }
    class Logarithm(Button val) : AOperator(val)
    {
        public override double Evaluate<T>(List<T> args)
        {
            if (args.Count != 2)
            {
                throw new UseMeCorrectlyException();
            }
            double? res1 = args[0] as double?;
            double? res2 = args[1] as double?;
            return Math.Log((double)res1!, (double)res2!);
        }
        public override double GetWeight()
        {
            return 50;
        }
    }
    class Polynomial(Button val) : AOperator(val)
    {
        public override double Evaluate<T>(List<T> args)
        {
            if (args.Count != 2)
            {
                throw new UseMeCorrectlyException();
            }
            double? res1 = args[0] as double?;
            double? res2 = args[1] as double?;
            return (double)((res1! * res1!) + (res2! * res1!) + res2!);
        }
        public override double GetWeight()
        {
            return 100;
        }
    }
}
