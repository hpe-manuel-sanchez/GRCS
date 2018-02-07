using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMGI.GRCS.UI.Rights.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public DateTime Enrollment { get; set; }
    }

    // This static class serves as a repository of the student collection
    public  class ContractMaintenanceWorkQueueManager
    {
        private  readonly List<Student> Students = new List<Student>();
        public  List<Student> GetStudents() { return Students; }

        // Static constructor to initiate some students in the repository
        public ContractMaintenanceWorkQueueManager()
        {
            int total = 105;

            DateTime now = DateTime.Now;
            var scoreRand = new Random();
            var enrollmentRand = new Random();
            for (int i = 1; i <= total; i++)
            {
                var student = new Student();
                student.Id = i;
                student.Name = "Name No." + i.ToString();
                student.Score = 60
                    + Convert.ToInt16(scoreRand.NextDouble() * 40);
                student.Enrollment
                    = now.AddDays(-1 * (int)(enrollmentRand.NextDouble() * 365 * 10));
                Students.Add(student);
            }
        }
    }
}
    
