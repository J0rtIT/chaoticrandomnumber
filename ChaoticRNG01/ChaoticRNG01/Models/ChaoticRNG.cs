using System;
using System.Linq;

namespace ChaoticRNG01.Models
{
    public class ChaoticRng
    {
        const int Iterations = 2000;
        const int Transcient = 1000;
        const double R = 4.0; //r for chaotic behavior of logistic map

        public double Average { get; set; }
        public double DesStad { get; set; }

        public double SingleNumber { get; set; }
        public double[] ArrayNumber { get; set; }

        public ChaoticRng()
        {
            SingleNumber = 0.0;
            ArrayNumber = new double[Iterations];
        }

        public double GetSingleRn()
        {
            //generate the seed (pseudo random number)
            Random rd = new Random();
            double x0 = rd.Next(0, int.MaxValue);
            x0 /= int.MaxValue;
            double x1 = 0.0;
            for (int i = 0; i < Iterations; i++)
            {
                //Logistic Map
                //Xn+1 = r * Xn * (1 - Xn) 
                x1 = R * x0 * (1.0 - x0);
                x0 = x1;
            }
            return x1;
        }

        public double[] GetArrayRn()
        {
            //generate the seed (pseudo random number)
            Random rd = new Random();
            double x0 = rd.Next(0, int.MaxValue);
            x0 /= int.MaxValue;

            for (int i = 0; i < Iterations + Transcient; i++)
            {
                //Logistic Map
                //Xn+1 = r * Xn * (1 - Xn) 
                var x1 = R * x0 * (1.0 - x0);
                if (i >= Transcient)
                {
                    ArrayNumber[i - Transcient] = x0;
                }
                x0 = x1;
            }
            Average = ArrayNumber.Average();
            DesStad = Devest(ArrayNumber);
            return ArrayNumber;
        }


        public double Devest(Double[] inputArray)
        {
            double average = inputArray.Average();
            double sumOfDerivation = 0;
            foreach (double value in inputArray)
            {
                sumOfDerivation += (value) * (value);
            }

            double sumOfDerivationAverage = sumOfDerivation / (inputArray.Length - 1);
            return Math.Sqrt(sumOfDerivationAverage - (average * average));
        }

    }


}
