using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistinctNum
{
    public class Distinct
    {
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
