namespace Stats
open FSharp.Stats
open FSharp.Stats.Distributions

module NormalDist =
    let SampleNormalDist (mean:double, sd:double) =
        let nd = Continuous.normal mean sd
        nd.Sample()

    let CalculateProbablityFromDistribution (mean:double, sd:double, value: double) = 
        let dist = Continuous.normal mean sd
        dist.CDF value