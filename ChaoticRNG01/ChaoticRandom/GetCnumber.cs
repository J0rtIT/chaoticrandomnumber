using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;

namespace ChaoticRandom
{
    [Cmdlet(VerbsCommon.Get, "CNumber")]
    [CmdletBinding(DefaultParameterSetName = "Default")]
    public class GetCNumber : Cmdlet
    {
        [Parameter(ParameterSetName = "Default", Mandatory = false, HelpMessage = "Provide a number of random number required", Position = 1, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "Other")]
        public Int32 Numbers { get; set; }

        [Parameter(ParameterSetName = "Other", Mandatory = false, HelpMessage = "Provide a number of the transients iterations you want to archieve (the iterations must be at high enought to allow the number", Position = 2, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public Int32 Transient { get; set; }

        [Parameter(ParameterSetName = "Other", Mandatory = false, HelpMessage = "Number of iterations (bigger than transient is a must)", Position = 3, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public Int32 Iterations { get; set; }

        public const double R = 4.0;

        protected override void BeginProcessing()
        {
            //no parameters set defaults
            if (Transient == 0 && Iterations == 0)
            {
                Numbers = (Numbers == 0) ? 1 : Math.Abs(Numbers);
                Transient = (Transient == 0) ? 2000 : Math.Abs(Transient);
                Iterations = (Iterations == 0) ? 5000 : Math.Abs(Iterations);
            }

            if (Iterations < Transient + 3000)
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("The Iterations must comply the equation  'Iterations >= Transient + 3000' "), null, ErrorCategory.InvalidArgument, null));

            }
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            var rdn = new Random();

            Collection<Double> randoms = new Collection<Double>();
            Double x0 = rdn.NextDouble();

            for (int i = 0; i < (Iterations + Transient); i++)
            {
                var x1 = R * x0 * (1 - x0);
                x0 = x1;

                if (i > Transient)
                {
                    randoms.Add(x1);
                }
            }

            if (Numbers > 1)
            {
                var result = (from r in randoms
                              select r).Reverse().Take(Numbers);

                foreach (var item in result)
                {
                    WriteObject(item);
                }
            }
            else
            {
                WriteObject((from r in randoms select r).Reverse().Take(1));
            }

            base.ProcessRecord();
        }
    }
}
