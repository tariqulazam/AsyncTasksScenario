using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PppLearning.AsyncTasksLatest
{
    public static class TupleExtension
    {
        public static async Task<Tuple<T1, T2>> WhenAll<T1, T2>(this Tuple<Task<T1>, Task<T2>> tasks)
        {
            await Task.WhenAll(tasks.Item1, tasks.Item2);
            return Tuple.Create(tasks.Item1.Result, tasks.Item2.Result);
        }

        public static async Task<(T1, T2)> WhenAllLatest<T1, T2>(this (Task<T1>, Task<T2>) tasks)
        {
            await Task.WhenAll(tasks.Item1, tasks.Item2);
            return (tasks.Item1.Result, tasks.Item2.Result);
        }
    }
}
