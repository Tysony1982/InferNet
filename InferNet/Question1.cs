using Microsoft.ML.Probabilistic.Distributions;
using Microsoft.ML.Probabilistic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Range = Microsoft.ML.Probabilistic.Models.Range;
using static InferNet.Helpers;
using Microsoft.ML.Probabilistic.Factors;
using static InferNet.DistributionSimulator;

namespace InferNet
{
    public static class Question1
    {
        private static Binomial bDist => new Binomial(100, 0.025);
        private static Variable<bool> StudentCompletes = Variable<bool>.Bernoulli(0.975);
        private static Variable<bool> StudentWithdraws = Variable<bool>.Bernoulli(0.025);
        private static InferenceEngine engine = new InferenceEngine();
        public static void Testing()
        {
            //var bDist = new Binomial(100, 0.025);

            //Console.WriteLine(bDist.ProbSuccess);

            Variable<bool> student1 = Variable<bool>.Bernoulli(0.025);
            Variable<bool> student2 = Variable<bool>.Bernoulli(0.025);
            Variable<bool> student3 = Variable<bool>.Bernoulli(0.975);
            Variable<bool> student4 = Variable<bool>.Bernoulli(0.975);
            Variable<bool> twoStudents = student1 & student2;
            Variable<bool> twoStudentsOnePassOneFail = student1 & student3;
            Variable<bool> twoStudentsBothPass = student3 & student4;

            
            Console.WriteLine("Given 2 students take the course");
            Console.WriteLine("Probability of both students failing " + engine.Infer(twoStudents));
            Console.WriteLine("Probability of 1 failing and the other passing " + engine.Infer(twoStudentsOnePassOneFail));
            Console.WriteLine("Probability of both passing " + engine.Infer(twoStudentsBothPass));
        }

        public static void SummaryInfo()
        {
            Console.WriteLine("\r\nQuestion 1 Summary Info \r\n");
            Console.WriteLine(
            @"A percentage of 2.5% of students withdrawing means we have a probability of 2.5/100 for withdrawal.
            This equates to the probability of withdrawal as 0.025 and a completion probablity of 0.975"
            );
        }

        private static void MainQuestion1(string question, int sampleSize, string answerStatement, Func<BinnedData<int>, bool> dataInterestedInFunc, string chartTitle = "")
        {
            Console.WriteLine(question);

            // Lets set up the scenario 
            List<int> sampleRuns = new List<int>();
            
            List<int> sampleIndexs = Enumerable.Range(0, sampleSize).ToList();

            for (var i = 1; i <= sampleSize; i++)
            {
                List<bool> trialObservations = new List<bool>();
                var trialNumber = 100;
                for (var j = 1; j <= trialNumber; j++)
                {
                    trialObservations.Add(Bernoulli.Sample(0.975));
                }

                // Now count the withdrawals 
                var countWithdrawals = trialObservations.Where(x => x == false).Count();
                sampleRuns.Add(countWithdrawals);
            }


            Plotting.GenerateHistogram(sampleRuns.ToArray(), chartTitle);

            var binedData = Helpers.GetBinnedData(sampleRuns, x => x);
            var likelihood = binedData.Where(dataInterestedInFunc).Select(x => (decimal)x.Count).ToList().Sum() / (decimal)sampleSize;

            Console.WriteLine(answerStatement + likelihood.ToString());

        }

        public static void PartA()
        {
            var question = "\r\na) What is the probability that two or fewer students will withdraw?\r\n";
            var sampleSize = 1000000;
            var answerstatement = "Given 100 students take the course. The probablity of at least 2 withdrawals is approximately ";
            Func<BinnedData<int>, bool> dataFunc = x => x.Key <= 2;

            MainQuestion1(question, sampleSize, answerstatement, dataFunc, "Q1 - Part A"); 

        }

        public static void PartB()
        {
            var question = "\r\nb) What is the probability that exactly 5 students will withdraw?\r\n";
            var sampleSize = 1000000;
            var answerstatement = "Given 100 students take the course. The probablity of exactly 5 withdrawals is approximately ";
            Func<BinnedData<int>, bool> dataFunc = x => x.Key == 5;

            MainQuestion1(question, sampleSize, answerstatement, dataFunc, "Q1 - Part B");
        }

        public static void PartC()
        {
            var question = "\r\nc) What is the probability that more than 3 students will withdraw?\r\n";
            var sampleSize = 1000000;
            var answerstatement = "Given 100 students take the course. The probablity of more than 3 withdrawals is approximately ";
            Func<BinnedData<int>, bool> dataFunc = x => x.Key > 3;

            MainQuestion1(question, sampleSize, answerstatement, dataFunc, "Q1 - Part C");
        }

        //public static void PartA1()
        //{
        //    Console.WriteLine("\r\nWhat is the probability that two or fewer students will withdraw?\r\n");

        //    // Lets set up the scenario 
        //    List<int> sampleRuns = new List<int>();
        //    var sampleSize = 1000000;
        //    List<int> sampleIndexs = Enumerable.Range(0, sampleSize).ToList();

        //    for (var i = 1; i <= sampleSize; i++)
        //    {
        //        List<bool> trialObservations = new List<bool>();
        //        var trialNumber = 100;
        //        for (var j = 1; j <= trialNumber; j++)
        //        {
        //            trialObservations.Add(Bernoulli.Sample(0.975));
        //        }

        //        // Now count the withdrawals 
        //        var countWithdrawals = trialObservations.Where(x => x == false).Count();
        //        sampleRuns.Add(countWithdrawals);
        //    }


        //    Plotting.GenerateHistogram(sampleRuns.ToArray());

        //    var binedData = Helpers.GetBinnedData(sampleRuns,x => x);
        //    var likelihood = (decimal)binedData.Where(x => x.Key <= 2).Select(x => x.Count).ToList().Sum() / (decimal)sampleSize;

        //    Console.WriteLine("Given 100 students take the course. The probablity of at least 2 withdrawals is approximately " + likelihood.ToString());

        //}





    }

    public static class Question2
    {
        private static InferenceEngine engine = new InferenceEngine();
        public static void SummaryInfo()
        {
            Console.WriteLine("\r\nQuestion 2 Summary Info \r\n");
            Console.WriteLine(
            @"Suppose that the return for a particular investment is normally distributed with a population mean of 10.1% and a population standard deviation of 5.4%.");
        }

        

        public static void PartAandB()
        {
            var dist = Variable.GaussianFromMeanAndVariance(10.1, (5.4 * 5.4));
            var dist2 = Variable.GaussianFromMeanAndPrecision(10.1, 5.4);


            // Lets set up the scenario 
            List<double> sampleRuns = new List<double>();
            int sampleSize = 1000000;
            List<int> sampleIndexs = Enumerable.Range(0, sampleSize).ToList();

            for (var i = 1; i <= sampleSize; i++)
            {

                // Now sample the distribution and add to list 
                //sampleRuns.Add(Gaussian.Sample(10.1, 5.4));

                var sample = Stats.NormalDist.SampleNormalDist(10.1, 5.4);

                sampleRuns.Add(sample);

            }

            Plotting.GenerateHistogram(sampleRuns.ToArray(), "Q2 - Part A and B");

            var dataSatisfiesConditionForA = (double)sampleRuns.Where(x => x >= 20).Count() / (double)sampleSize;

            Console.WriteLine($"Given mean 10.1 and precision of 5.4, there is a {dataSatisfiesConditionForA} probability that the return will be 20% or more");

            var dataSatisfiesConditionForB = (double)sampleRuns.Where(x => x <= 10).Count() / (double)sampleSize;

            Console.WriteLine($"Given mean 10.1 and precision of 5.4, there is a {dataSatisfiesConditionForB} probability that the return will be 10% or less");

        }

        public static void PartC()
        {
            var sampler = Stats.NormalDist.SampleNormalDist;

            // Lets set up the scenario 
            List<double> sampleRuns = new List<double>();
            int sampleSize = 1000000;
            List<int> sampleIndexs = Enumerable.Range(0, sampleSize).ToList();

            for (var i = 1; i <= sampleSize; i++)
            {

                // Now sample the distribution and add to list

                sampleRuns.Add(sampler(100,15));

            }

            Plotting.GenerateHistogram(sampleRuns.ToArray(), "Q2 - Part C");
            List<(double x, double probability)> probabilities = new List<(double, double)>();
            for (var i = 100.0; i < 250; i = i + 0.1)
            {
                var probability = (double)sampleRuns.Where(x => x < i).Count() / (double)sampleSize;
                probabilities.Add((i, probability));    
            }

            var expectedCuttoff = probabilities.Where(x => Math.Round((x.probability), 2) == 0.95).FirstOrDefault();
            
        }
    }

    public static class Question3
    {
        public static void SummaryInfo()
        {
            Console.WriteLine("\r\nQuestion 3 Summary Info \r\n");
            Console.WriteLine(
            @"According to a recent study, the average night’s sleep is 8 hours. Assume that the standard deviation is 1.1 hours and that the probability distribution is normal.");
        }


        public static void PartA()
        {
            _ = GenerateAndSimulateGaussian(8.0, 1.1, "person sleeps more than 8 hours", x => x > 8);
            

        }

        public static void PartB()
        {
            var probability = GenerateAndSimulateGaussian(8.0, 1.1, "person will sleep", x => x >= 7 && x <= 9);

            Console.WriteLine($"Thus the percentage of people that sleep between 7 and 9 hours is {probability * 100}");
        }


        

    }

    public static class Question4
    {
        public static void SummaryInfo()
        {
            Console.WriteLine("\r\nQuestion 3 Summary Info \r\n");
            Console.WriteLine(
            @"According to a recent study, the average night’s sleep is 8 hours. Assume that the standard deviation is 1.1 hours and that the probability distribution is normal.");
        }
        private static double mean => 160.0;
        private static double sd => 25.0;

        public static void PartA()
        {
            _ = GenerateAndSimulateGaussian(mean, sd, "student will complete the exam in 120 minutes or less", x => x <= 120, "Q3 - Part A");


        }

        public static void PartB()
        {
            var probability = GenerateAndSimulateGaussian(mean, sd, "student will complete the exam in more than 120 minutes but less than 150 minutes", x => x > 120 && x < 150, "Q3 - Part B");

            
        }

        public static void PartC()
        {
            var probability = GenerateAndSimulateGaussian(mean, sd, "student will complete the exam in more than 100 minutes but less than 170 minutes", x => x > 100 && x < 170, "Q3 - Part C");

        }

    }

    public static class DistributionSimulator
    {

        /// <summary>
        /// Generate and simulate a number of trials. Produce a histogram of the findings
        /// </summary>
        /// <param name="mean"></param>
        /// <param name="sd"></param>
        /// <param name="probabilitStatement">probability that the ...{your string here}</param>
        /// <returns>Probability that is calculated</returns>
        public static double GenerateAndSimulateGaussian(double mean, double sd, string probabilityStatement, Func<double, bool> probabilityFunction, string chartTitle = "")
        {

            var sampler = Stats.NormalDist.SampleNormalDist;

            // Lets set up the scenario 
            List<double> sampleRuns = new List<double>();
            int sampleSize = 1000000;
            List<int> sampleIndexs = Enumerable.Range(0, sampleSize).ToList();

            for (var i = 1; i <= sampleSize; i++)
            {

                // Now sample the distribution and add to list

                sampleRuns.Add(sampler(mean, sd));

            }

            Plotting.GenerateHistogram(sampleRuns.ToArray(), chartTitle);

            var probabilityValue = (double)sampleRuns.Where(probabilityFunction).Count() / (double)sampleSize;

            Console.WriteLine($"Given mean {mean} and standard deviation of {sd} there is a {probabilityValue} probability that the {probabilityStatement}");

            return (probabilityValue);

        }
    }
}
