using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Collections;

namespace Utility.Test
{
    class CollectionTest
    {
        public static void TestArray()//Main(string[] args)
        {
            TestSort();

            Console.WriteLine();
            Console.WriteLine("array");
            Array<int> vs = new Array<int>(Comparer<int>.Default) { 1, 2, 3, 46, 7, 1 };
            //vs.Sort();
            foreach (var item in vs)
            {
                Console.Write(item + " ");
            }
            //push
            vs.Dequeue();
            //vs.Pop();
            vs.Push(11);
            Console.WriteLine(vs.Exists(46));
            Console.WriteLine(vs.FindIndex(1));
            Console.WriteLine();
            foreach (var item in vs)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine("collection");
            Collection<int, string> keyValuePairs = new Collection<int, string>(Comparer<int>.Default,EqualityComparer<string>.Default) { [1]="b",[5]="a", [6] = "a", [3] = "a" };
            //Collection<int, string> keyValuePairs = new Collection<int, string>(Comparer<int>.Default, EqualityComparer<string>.Default) { [1] = "b", [5] = "a", [6] = "a", [3] = "a" };
            Console.WriteLine();
            foreach (var item in keyValuePairs)
            {
                Console.Write(item.Key + " "+item.Value+" ");
            }
        }



        public static void TestSort()
        {
            int[] a = new int[] { 3, 5, 19, 2, 10, 50, 22, 10, 11, 8, 7, 6, 8, 10, 11, 7 };
            foreach (var item in a)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            SortHelper.CountingSort(a, null);
            Console.WriteLine("sort ");
            SortHelper.BucketSort(a);

            foreach (var item in a)
            {
                Console.Write(item + " ");
            }
        }
    }
}
