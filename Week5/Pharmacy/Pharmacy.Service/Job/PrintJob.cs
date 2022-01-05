using System;

namespace Pharmacy.Service.Job
{
    public class PrintJob : IPrintJob
    {
        public void Print()
        {
            Console.WriteLine($"Hangfire recurring job!");
        }
    }
}
