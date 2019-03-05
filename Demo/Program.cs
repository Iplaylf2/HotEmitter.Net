using System;
using System.Linq;
using HotEmitter;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            (var emit, var lineA) = new Emitter<Int32>();
            lineA.Filter(x => x > 0)
                .Connect(x => Console.WriteLine(x));
            foreach (var x in Enumerable.Range(-2, 5))
            {
                emit(x);
            }
        }
    }
}
