using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaoticRNG01.Models
{
    public class ChaoticRNG
    {
        const int iterations = 2000;
        const int transcient = 1000;
        const double r = 4.0; //r for chaotic behavior of logistic map

        public double SingleNumber { get; set; }
        public double[] ArrayNumber { get; set; }

        public ChaoticRNG()
        {
            SingleNumber = 0.0;
            ArrayNumber = new double[iterations];
        }

        public double GetSingleRN()
        {
            //generate the seed (pseudo random number)
            Random rd = new Random();
            double x0 = rd.Next(0, int.MaxValue);
            x0/= int.MaxValue;
            double x1 = 0.0;
            for (int i = 0; i < iterations; i++)
            {
                //Logistic Map
                //Xn+1 = r * Xn * (1 - Xn) 
                x1 = r * x0 * (1.0 - x0);
                x0 = x1;
            }
            return x1;
        }

        public double[] GetArrayRN()
        {
            //generate the seed (pseudo random number)
            Random rd = new Random();
            double x0 = (double) rd.Next(0, int.MaxValue);
            x0 /= int.MaxValue;

            double x1 = 0.0;
            for (int i = 0; i < iterations + transcient; i++)
            {
                //Logistic Map
                //Xn+1 = r * Xn * (1 - Xn) 
                x1 = r * x0 * (1.0 - x0);
                if (i >= transcient)
                {
                    ArrayNumber[i - transcient] = x0;
                }
                x0 = x1;
            }
            return ArrayNumber;
        }

    }
}
