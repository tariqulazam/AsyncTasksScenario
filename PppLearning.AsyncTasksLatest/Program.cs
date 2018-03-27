using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using PppLearning.Framework.Configuration.Subscription;
using PppLearning.Framework.Consoles;


namespace PppLearning.AsyncTasksLatest
{
    using System.Diagnostics;

    class Program
    {
        static void Main(string[] args)
        {
            using (var kernel = new StandardKernel())
            {
                kernel.Load<CompositionRoot>();

                // Remove the following line if you don't want a menu in your app
                kernel.Get<IConsoleMenuPopulator>().Populate();
            }
        }
    }


    public class AsyncTasksExecutesSynchronously : ConsoleCommand
    {
        public override string DisplayText
        {
            get { return "Executes Async Tasks Synchronously"; }
        }

        public override void Execute()
        {
            var sw = new Stopwatch();
            sw.Start();

            var service = new SchoolDataService();
            service.GetSchoolSummarySync("authtoken").Wait();

            sw.Stop();

            Console.WriteLine($"Total time elasped - {sw.ElapsedMilliseconds}");
        }
    }

    public class AsyncTasksExecutesWhenAll : ConsoleCommand
    {
        public override string DisplayText
        {
            get { return "Executes Async Tasks using Task.WhenAll"; }
        }

        public override void Execute()
        {
            var sw = new Stopwatch();
            sw.Start();

            var service = new SchoolDataService();
            service.GetSchoolSummaryAsyncWhenAll("authtoken").Wait();

            sw.Stop();

            Console.WriteLine($"Total time elasped - {sw.ElapsedMilliseconds}");
        }

    }

    public class AsyncTasksExecutesWithExceptionWhenAll : ConsoleCommand
    {
        // State your dependencies as constructor parameters to have them injected.

        public override string DisplayText
        {
            get { return "Executes Async Tasks using Task.WhenAll with exception"; }
        }

        public override void Execute()
        {
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                var service = new SchoolDataService();
                service.GetSchoolSummaryAsyncWhenAllWithException("authtoken").Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured - {ex.GetType()}");
            }
            finally
            {
                sw.Stop();
                Console.WriteLine($"Total time elasped - {sw.ElapsedMilliseconds}");
            }
        }

    }

    public class AsyncTasksExecutesWithoutWhenAll : ConsoleCommand
    {
        // State your dependencies as constructor parameters to have them injected.

        public override string DisplayText
        {
            get { return "Executes Async Tasks without using Task.WhenAll"; }
        }

        public override void Execute()
        {
            var sw = new Stopwatch();
            sw.Start();

            var service = new SchoolDataService();
            service.GetSchoolSummaryAsyncWithoutWhenAll("authtoken").Wait();

            sw.Stop();

            Console.WriteLine($"Total time elasped - {sw.ElapsedMilliseconds}");
        }

    }

    public class AsyncTasksExecutesWithExceptionWithoutWhenAll : ConsoleCommand
    {
        // State your dependencies as constructor parameters to have them injected.

        public override string DisplayText
        {
            get { return "Executes Async Tasks without using Task.WhenAll with exception"; }
        }

        public override void Execute()
        {
            var sw = new Stopwatch();
            sw.Start();

            try
            {
                var service = new SchoolDataService();
                service.GetSchoolSummaryAsyncWithoutWhenAllWithException("authtoken").Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured - {ex.GetType()}");
            }
            finally
            {
                sw.Stop();
                Console.WriteLine($"Total time elasped - {sw.ElapsedMilliseconds}");
            }

        }

    }

    public class AsyncTasksExecutesWhenAllUsingTuple : ConsoleCommand
    {
        public override string DisplayText
        {
            get { return "Executes Async Tasks with WhenAll using Tuple"; }
        }

        public override void Execute()
        {
            var sw = new Stopwatch();
            sw.Start();

            var service = new SchoolDataService();
            service.GetSchoolSummaryAsyncWhenAllUsingTuple("authtoken").Wait();

            sw.Stop();

            Console.WriteLine($"Total time elasped - {sw.ElapsedMilliseconds}");
        }

    }

    public class AsyncTasksExecutesWhenAllUsingTupleLatest : ConsoleCommand
    {
        // State your dependencies as constructor parameters to have them injected.

        public override string DisplayText
        {
            get { return "Executes Async Tasks with WhenAll using Value Tuple (C# 7.0)"; }
        }

        public override void Execute()
        {
            var sw = new Stopwatch();
            sw.Start();

            var service = new SchoolDataService();
            service.GetSchoolSummaryAsyncWhenAllUsingTupleLatest("authtoken").Wait();

            sw.Stop();

            Console.WriteLine($"Total time elasped - {sw.ElapsedMilliseconds}");
        }

    }

    public class AsyncTasksExecutesWithExceptionWhenAllUsingTuple : ConsoleCommand
    {
        // State your dependencies as constructor parameters to have them injected.

        public override string DisplayText
        {
            get { return "Executes Async Tasks with exception WhenAll using Tuple"; }
        }

        public override void Execute()
        {
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                var service = new SchoolDataService();
                service.GetSchoolSummaryAsyncWithExceptionWhenAllUsingTupleLatest("authtoken").Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured - {ex.GetType()}");
            }
            finally
            {
                sw.Stop();
                Console.WriteLine($"Total time elasped - {sw.ElapsedMilliseconds}");
            }
        }

    }

    public class ClearWindowCommand : ConsoleCommand
    {
        // State your dependencies as constructor parameters to have them injected.

        public override string DisplayText
        {
            get { return "Clear Window"; }
        }

        public override void Execute()
        {
            Console.Clear();
        }

    }

}
