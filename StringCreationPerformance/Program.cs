using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Text;

namespace StringCreationPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchy>();
        }
    }

    [MemoryDiagnoser]
    public class Benchy
    {
        private const string ClearValue = "Password123!";

        [Benchmark]
        public string NaiveMask()
        {
            var firstChars = ClearValue.Substring(0, 3);
            var length = ClearValue.Length - 3;

            for (int i = 0; i < length; i++)
            {
                firstChars += "*";
            }

            return firstChars;
        }

        [Benchmark]
        public string StringBuilderMask()
        {
            var firstChars = ClearValue.Substring(0, 3);
            var length = ClearValue.Length - 3;
            var stringBuilder = new StringBuilder(firstChars);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append("*");
            }

            return firstChars;
        }

        [Benchmark]
        public string NewStringMask()
        {
            var firstChars = ClearValue.Substring(0, 3);
            var length = ClearValue.Length - 3;
            var asterisks = new string('*', length);

            return firstChars + asterisks;
        }

        [Benchmark]
        public string StringCreateMask()
        {

            return string.Create(ClearValue.Length, ClearValue, (span, value) => {
                value.AsSpan().CopyTo(span);
                span[3..].Fill('*');
            });
        }



    }

}
