using System;
namespace Testmvc.Services
{
    public class MyCalculator : IMyCalculator
    {
        public MyCalculator()
        {
            //public static void compute(string ticker, double principal, double apy, double years)
            //{
            //    double result = principal * Math.Pow(1.0 + (apy / 100.0), years);
            //    Math.Round((principal * Math.Pow(1.0 + (apy / 100.0), years)), 2);
            //}

        }

        public double compute(string ticker, double principal, double apy, double years)
        {
            double result = principal * Math.Pow(1.0 + (apy / 100.0), years);
            return Math.Round(result, 2);
        }
    }
}

