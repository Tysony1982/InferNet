using Microsoft.ML.Probabilistic.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infer.Utilities
{
    public static class DataGen
    {
        public static bool TestBucketize()
        {
            var sampler = Stats.NormalDist.SampleNormalDist;
            //var sampler2 = Gaussian (x,y) => new Gaussian(x,y);

            // Lets set up the scenario 
            List<double> sampleRuns = new List<double>();
            int sampleSize = 1000000;
            List<int> sampleIndexs = Enumerable.Range(0, sampleSize).ToList();

            for (var i = 1; i <= sampleSize; i++)
            {

                // Now sample the distribution and add to list

                sampleRuns.Add(sampler(100, 15));

            }

            TransformHelpers.Bucketize(sampleRuns);  // , ((int)Math.Floor(sampleRuns.Max()) - (int)Math.Ceiling(sampleRuns.Min()))

            Plotting.GenerateHistogram(sampleRuns.ToArray(), "TestingBucketize");
            List<(double x, double probability)> probabilities = new List<(double, double)>();
            for (var i = 100.0; i < 250; i = i + 0.1)
            {
                var probability = (double)sampleRuns.Where(x => x < i).Count() / (double)sampleSize;
                probabilities.Add((i, probability));
            }

            var expectedCuttoff = probabilities.Where(x => Math.Round((x.probability), 2) == 0.95).FirstOrDefault();

            return true;
        }
    }
}
