using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infer.Utilities
{
    public static class TransformHelpers
    {
        public static List<BinnedData<TKey>> GetBinnedData<T, TKey>(IEnumerable<T> list, Func<T, TKey> colFunc)
        {
            var groups = list.GroupBy(colFunc).Select(x => new BinnedData<TKey>()
            {
                Key = x.Key,
                Count = x.Count()
            });

            return groups.ToList();
        }

        public static List<int> Bucketize(this IEnumerable<double> source)
        {
            var min = Math.Floor(source.Min());
            var max = Math.Ceiling(source.Max());

            var totalBuckets = max - min;
            var bucketSize = 1;
            var buckets = new List<int>();
            foreach (var value in source)
            {
                int bucketIndex = 0;
                if (bucketSize > 0.0)
                {
                    bucketIndex = (int)((value - min) / bucketSize);
                    if (bucketIndex == totalBuckets)
                    {
                        bucketIndex--;
                    }
                }
                buckets[bucketIndex]++;
            }

            return buckets;
        }

        public static List<BinnedData<T>> Bucketize<T>(this IEnumerable<double> source)
        {
            var min = Math.Floor(source.Min());
            var max = Math.Ceiling(source.Max());

            var totalBuckets = max - min;
            var bucketSize = 1;
            var buckets = new List<BinnedData<T>>();
            foreach (var value in source)
            {
                int bucketIndex = 0;
                if (bucketSize > 0.0)
                {
                    bucketIndex = (int)((value - min) / bucketSize);
                    if (bucketIndex == totalBuckets)
                    {
                        bucketIndex--;
                    }
                }
                //buckets[bucketIndex]++;
            }

            return buckets;
        }
    }


    public class BinnedData
    {
        public string? Key { get; set; }
        public Range Range { get; set; }
        public int? Count { get; set; }

        //public static List<BinnedData<T>> CreateListOfBinnedData<T>()
        //{

        //}
    }

    public class BinnedData<T>
    {
        public T? Key { get; set; }
        public int? Count { get; set; }

    }
}
