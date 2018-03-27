using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PppLearning.AsyncTasksLatest
{
    public class SchoolSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<string> ClassroomNames { get; set; }

        public IEnumerable<string> StudentNames { get; set; }
    }
}
