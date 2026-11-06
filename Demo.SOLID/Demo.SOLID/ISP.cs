using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.SOLID
{
    public interface IMath
    {
        int Sum(int a, int b);
        int Divide(int a, int b);
        int Multiply(int a, int b);

     
    }

    public interface IMathPro : IMath
    {
        double SquareRoot(double a);
        double Power(double a, double b);
    }

    public class SimpleMath : IMath
    {
        public int Divide(int a, int b)
        {
            throw new NotImplementedException();
        }

        public int Multiply(int a, int b)
        {
            throw new NotImplementedException();
        }

        public int Sum(int a, int b)
        {
            throw new NotImplementedException();
        }
    }


}
