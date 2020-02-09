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
            var summary = BenchmarkRunner.Run<Collections>();
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
}
