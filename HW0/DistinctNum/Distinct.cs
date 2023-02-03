using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistinctNum
{
    /// <summary>
    /// Contains static methods for calculating distinct number of items in a list
    /// </summary>
    public class Distinct
    {
        /// <summary>
        /// Uses a hash table to calculate the number of distinct (unique) items in a list
        /// </summary>
        /// <param name="list"></param>
        /// <returns>The count of distinct integers in the list</returns>
        public static int hashDistinct(List<int> list)
        {
            if (list == null)
            {
                return 0;
            }
            if (list.Count == 1)
            {
                return 1;
            }

            HashSet<int> hash = new HashSet<int>();
            foreach (int i in list)
            {
                hash.Add(i);
            }
            return hash.Count;
        }

        /// <summary>
        /// Uses nested loops to calculate the distinct integers in a list.
        /// </summary>
        /// <param name="list"></param>
        /// <returns>total count of distinct integers</returns>
        public static int loopDistinct(List<int> list)
        {
            if (list == null)
            {
                return 0;
            }
            if (list.Count == 1)
            {
                return 1;
            }

            int count = 0;

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i+1; j < list.Count; j++)
                {
                    if (list[i] == list[j])
                    {
                        count--;
                        break;
                    }
                }
                count++;
            }
            return count;
        }

        /// <summary>
        /// Sorts a list and then calculate the distinct number of integers in the list
        /// </summary>
        /// <param name="list"></param>
        /// <returns>total count of distinct integers</returns>
        public static int sortDistinct(List<int> list)
        {
            if (list == null)
            {
                return 0;
            }
            if(list.Count == 1)
            {
                return 1;
            }

            int count = 1;
            list.Sort();
            for (int i = 0; i < list.Count-1; i++)
            {
                if (list[i] != list[i + 1])
                {
                    count++;
                }
            }
            return count;
        }

    }
}
