using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferNet
{
    public static class Helpers
    {
        public static List<BinnedData<TKey>> GetBinnedData<T,TKey>(IEnumerable<T> list, Func<T,TKey> colFunc)
        {
            var groups = list.GroupBy(colFunc).Select(x => new BinnedData<TKey>()
            {
                Key = x.Key,
                Count = x.Count()
            });

            return groups.ToList();
        }

        public static List<int> Bucketize(this IEnumerable<double> source, int totalBuckets)
        {
            var min = source.Min();
            var max = source.Max();
            var buckets = new List<int>();

            var bucketSize = (max - min) / totalBuckets;
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
    }



    public class BinnedData<T>
    {
        public T? Key { get; set; }
        public int? Count { get; set; }
    }
}
