namespace PppLearning.AsyncTasksLatest
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class SchoolDataService
    {
        public async Task<SchoolSummary> GetSchoolSummarySync(string token)
        {
            var schoolDetail = await this.GetSchoolDetailAsync(token);
            var schoolId = schoolDetail.Item1;
            var regionId = schoolDetail.Item2;

            var classrooms = await this.GetClassroomsAsync(schoolId, regionId);
            var students = await this.GetStudentsAsync(schoolId, regionId);

            return new SchoolSummary()
                       {
                           Id = schoolId,
                           Name = schoolDetail.Item3,
                           ClassroomNames = classrooms,
                           StudentNames = students
                       };
        }

        public async Task<SchoolSummary> GetSchoolSummaryAsyncWhenAll(string token)
        {
            var schoolDetail = await this.GetSchoolDetailAsync(token);
            var schoolId = schoolDetail.Item1;
            var regionId = schoolDetail.Item2;

            var getClassroomsTask = this.GetClassroomsAsync(schoolId, regionId);
            var getStudentsTask = this.GetStudentsAsync(schoolId, regionId);

            await Task.WhenAll(getClassroomsTask, getStudentsTask);

            return new SchoolSummary()
                       {
                           Id = schoolId,
                           Name = schoolDetail.Item3,
                           ClassroomNames = getClassroomsTask.Result,
                           StudentNames = getStudentsTask.Result
                       };
        }

        public async Task<SchoolSummary> GetSchoolSummaryAsyncWhenAllWithException(string token)
        {
            var schoolDetail = await GetSchoolDetailAsync(token);
            var schoolId = schoolDetail.Item1;
            var regionId = schoolDetail.Item2;

            var getStudentsTask = this.GetStudentsAsync(schoolId, regionId);
            var getClassroomsTask = this.GetClassroomsWithExceptionsAsync(schoolId, regionId);

            await Task.WhenAll(getClassroomsTask, getStudentsTask);

            return new SchoolSummary()
                       {
                           Id = schoolId,
                           Name = schoolDetail.Item3,
                           ClassroomNames = getClassroomsTask.Result,
                           StudentNames = getStudentsTask.Result
            };
        }

        public async Task<SchoolSummary> GetSchoolSummaryAsyncWithoutWhenAll(string token)
        {
            var schoolDetail = await GetSchoolDetailAsync(token);
            var schoolId = schoolDetail.Item1;
            var regionId = schoolDetail.Item2;

            var getClassroomsTask = this.GetClassroomsAsync(schoolId, regionId);
            var getStudentsTask = this.GetStudentsAsync(schoolId, regionId);

            return new SchoolSummary()
                       {
                           Id = schoolId,
                           Name = schoolDetail.Item3,
                           ClassroomNames = await getClassroomsTask,
                           StudentNames = await getStudentsTask
                       };
        }

        public async Task<SchoolSummary> GetSchoolSummaryAsyncWithoutWhenAllWithException(string token)
        {
            var schoolDetail = await GetSchoolDetailAsync(token);
            var schoolId = schoolDetail.Item1;
            var regionId = schoolDetail.Item2;

            var getStudentsTask = this.GetStudentsAsync(schoolId, regionId);
            var getClassroomsTask = this.GetClassroomsWithExceptionsAsync(schoolId, regionId);

            return new SchoolSummary()
                       {
                           Id = schoolId,
                           Name = schoolDetail.Item3,
                           ClassroomNames = await getClassroomsTask,
                           StudentNames = await getStudentsTask
                       };
        }

        public async Task<SchoolSummary> GetSchoolSummaryAsyncWhenAllUsingTuple(string token)
        {
            var schoolDetail = await GetSchoolDetailAsync(token);
            var schoolId = schoolDetail.Item1;
            var regionId = schoolDetail.Item2;

            var schoolSummary = await Tuple.Create(
                                    this.GetClassroomsAsync(schoolId, regionId),
                                    this.GetStudentsAsync(schoolId, regionId)).WhenAll();

            return new SchoolSummary()
                       {
                           Id = schoolId,
                           Name = schoolDetail.Item3,
                           ClassroomNames = schoolSummary.Item1,
                           StudentNames = schoolSummary.Item2
                       };
        }

        public async Task<SchoolSummary> GetSchoolSummaryAsyncWhenAllUsingTupleLatest(string token)
        {
            var (schoolId, regionId, schoolName) = await this.GetSchoolDetailAsync(token);

            var (classrooms, students) =
                await (this.GetClassroomsAsync(schoolId, regionId), this.GetStudentsAsync(schoolId, regionId)).WhenAllLatest();

            return new SchoolSummary()
                       {
                           Id = schoolId,
                           Name = schoolName,
                           ClassroomNames = classrooms,
                           StudentNames = students
                       };
        }

        public async Task<SchoolSummary> GetSchoolSummaryAsyncWithExceptionWhenAllUsingTupleLatest(string token)
        {
            var (schoolId, regionId, schoolName) = await this.GetSchoolDetailAsync(token);

            var (classrooms, students) =
                await (this.GetClassroomsWithExceptionsAsync(schoolId, regionId), this.GetStudentsAsync(schoolId, regionId)).WhenAllLatest();

            return new SchoolSummary()
                       {
                           Id = schoolId,
                           Name = schoolName,
                           ClassroomNames = classrooms,
                           StudentNames = students
                       };
        }

        private async Task<(int, int, string)> GetSchoolDetailAsync(string token)
        {
            await Task.Delay(100);
            Console.WriteLine($"GetSchoolDetailAsync ThreadId - {Thread.CurrentThread.ManagedThreadId}");
            return (1, 8, "Test School");
        }

        private async Task<IEnumerable<string>> GetClassroomsAsync(int schoolId, int regionId)
        {
            await Task.Delay(300);
            Console.WriteLine($"GetClassroomsAsync ThreadId - {Thread.CurrentThread.ManagedThreadId}");
            return new[] { "1A", "1B", "1C", "1D" };
        }

        private async Task<IEnumerable<string>> GetStudentsAsync(int schoolId, int regionId)
        {
            await Task.Delay(500);
            Console.WriteLine($"GetStudentsAsync ThreadId - {Thread.CurrentThread.ManagedThreadId}");
            return new[] { "Student One", "Student Two", "Student Three", "Student Four" };
        }

        private async Task<IEnumerable<int>> GetTeachersAsync(int schoolId, int regionId)
        {
            await Task.Delay(500);
            Console.WriteLine($"GetTeachersAsync ThreadId - {Thread.CurrentThread.ManagedThreadId}");
            return new[] { 1, 2, 3 };
        }

        private async Task<IEnumerable<string>> GetClassroomsWithExceptionsAsync(int schoolId, int regionId)
        {
            throw new UnauthorizedAccessException("You are not an authorize user from this school");
        }
    }
}
