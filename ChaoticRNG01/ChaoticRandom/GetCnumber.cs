using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;

namespace ChaoticRandom
{
    [Cmdlet(VerbsCommon.Get, "CNumber")]
    public class GetCNumber : Cmdlet
    {
        [Parameter(Mandatory = false, HelpMessage = "Provide a number of random number required", Position = 1,
            ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public Int32 Numbers { get; set; }

        [Parameter(Mandatory = false,
            HelpMessage =
                "Provide a number of the trasients iterations you want to archieve (the iterations must be at high enought to allow the number",
            Position = 2, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public Int32 Transien { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Number of iterations (bigger than trasient is a must)",
            Position = 3, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public Int32 Iterations { get; set; }

        public const double R = 4.0;

        protected override void BeginProcessing()
        {
            //no parameters set defaults
            if (Transien == 0 && Iterations == 0)
            {
                Numbers = (Numbers == 0) ? 1 : Math.Abs(Numbers);
                Transien = (Transien == 0) ? 2000 : Math.Abs(Transien);
                Iterations = (Iterations == 0) ? 5000 : Math.Abs(Iterations);
            }


            if (Iterations < Transien + 3000)
            {
                ThrowTerminatingError(new ErrorRecord(
                    new Exception("The Iterations must be the transcients plus 3k at least"), null,
                    ErrorCategory.InvalidArgument, null));

            }
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            var rdn = new Random();

            Collection<Double> Randoms = new Collection<Double>();

            for (int i = 0; i < (Iterations + Transien); i++)
            {

                var x0 = rdn.NextDouble();
                var x1 = R * x0 * (1 - x0);

                x0 = x1;

                if (i > Transien)
                {
                    if (Numbers <= 1)
                    {
                        WriteObject(x1);
                        break;
                    }
                    else
                    {
                        Randoms.Add(x1);
                    }

                }
            }

            if (Numbers > 1)
            {
                var result = (from r in Randoms
                              select r).Reverse().Take(Numbers);

                foreach (var item in result)
                {
                    WriteObject(item);
                }
            }
            base.ProcessRecord();
        }
        /*
                protected override void EndProcessing()
                {
                    base.EndProcessing();
                }

                protected override void StopProcessing()
                {
                    base.StopProcessing();
                }
            }
            */
    }
}
