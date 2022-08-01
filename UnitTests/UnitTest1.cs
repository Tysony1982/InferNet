using Infer.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML.Probabilistic.Distributions;
using Microsoft.ML.Probabilistic.Models;
using static Infer.Utilities.DataGen;
namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            TestBucketize();
        }
    }
}