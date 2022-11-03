using System;
namespace Testmvc.Services
{
    public interface IMyCalculator
    {
        // Call compute method??
        // Iaction result is the type for compute..
        public double compute(string ticker, double principal, double apy, double years);
    }
}

