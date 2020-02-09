using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace PerformanceConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Loops>();
        }
    }

    public class Md5VsSha256
    {
        private const int N = 10000;
        private readonly byte[] data;

        private readonly SHA256 sha256 = SHA256.Create();
        private readonly MD5 md5 = MD5.Create();

        public Md5VsSha256()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
        }

        [Benchmark]
        public byte[] Sha256() => sha256.ComputeHash(data);

        [Benchmark]
        public byte[] Md5() => md5.ComputeHash(data);
    }

    public class BoxingUnboxing
    {
        private const int N = 10000;
        [Benchmark]
        public void IntegerAddition()
        {
            int a = 1;
            for (int i = 0; i < N; i++)
            {
                a = a + 1;
            }
        }

        [Benchmark]
        public void BoxingUnboxingAddition()
        {
            object a = 1;
            for (int i = 0; i < N; i++)
            {
                a = (int)a + 1;
            }
        }
    }

    public class StringConcatenation
    {
        private const int N = 10000;

        [Benchmark]
        public void StringAddition()
        {
            var s = string.Empty;
            for (int i = 0; i < N; i++)
            {
                s = s + "a";
            }
        }

        [Benchmark]
        public void StringBuilder()
        {
            var sb=new StringBuilder();
            for (int i = 0; i < N; i++)
            {
                sb.Append("a");
            }

            var s = sb.ToString();
        }
    }

    public class Collections
    {
        private const int N = 10000;

        [Benchmark]
        public void MeasureArrayList()
        {
            ArrayList list= new ArrayList();
            for (int i = 0; i < N; i++)
            {
                list.Add(i);
            }
        }

        [Benchmark]
        public void MeasureGenericList()
        {
            List<int> list=new List<int>();
            for (int i = 0; i < N; i++)
            {
                list.Add(i);
            }
        }

        [Benchmark]
        public void MeasureIntegerArray()
        {
            int[] list = new int[N];
            for (int i = 0; i < N; i++)
            {
                list[i] = i;
            }
        }
    }

    public class FastArrays
    {
        private const int N = 10000;

        [Benchmark]
        public void MeasureArray1D()
        {
            var array = new int[N*N];
            for (int i = 0; i < N*N; i++)
            {
                array[i] = 1;
            }
        }

        [Benchmark]
        public void MeasureArray2D()
        {
            var array = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    array[i, j] = 1;
                }
            }
        }

        [Benchmark]
        public void MeasureArrayJagged()
        {
            var array = new int[N][];
            for (int i = 0; i < N; i++)
            {
                array[i]= new int[N];
                for (int j = 0; j < N; j++)
                {
                    array[i][j] = 1;
                }
            }
        }
    }

    public class Exceptions
    {
        private const int N = 100000;

        [Benchmark]
        public void SimpleAddition()
        {
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                count++;
            }
        }

        [Benchmark]
        public void AdditionWithException()
        {
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                try
                {
                    count++;
                    throw new InvalidOperationException();
                }
                catch (InvalidOperationException)
                {

                }
            }
        }
    }

    public class Loops
    {
        private const int N = 1000000;
        ArrayList arraylist= new ArrayList(N);
        List<int> list= new List<int>(N);
        int[] array= new int[N];

        public Loops()
        {
            var random= new Random();
            for (int i = 0; i < N; i++)
            {
                int number = random.Next(256);
                arraylist.Add(number);
                list.Add(number);
                array[i] = number;
            }
        }


        [Benchmark]
        public void ArrayListFor()
        {
            for (int i = 0; i < N; i++)
            {
                var a = arraylist[i];
            }
        }

        [Benchmark]
        public void ArrayListForeach()
        {
            foreach (var i in arraylist)
            {
                var a = arraylist[(int)i];
            }
        }

        [Benchmark]
        public void ListFor()
        {
            for (int i = 0; i < N; i++)
            {
                var a = list[i];
            }
        }

        [Benchmark]
        public void ListForeach()
        {
            foreach (var i in list)
            {
                var a = list[i];
            }
        }


        [Benchmark]
        public void ArrayFor()
        {
            for (int i = 0; i < N; i++)
            {
                var a = array[i];
            }
        }

        [Benchmark]
        public void ArrayForeach()
        {
            foreach (var i in list)
            {
                var a = array[i];
            }
        }
    }
}
