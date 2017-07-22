using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataAccessLayer
{
    class Program
    {
        static void Main(string[] args)
        {
//===========================================Test Access DB ===================================================
            TestTablePoco testtablePoco = new TestTablePoco
            {
                Id = Guid.NewGuid(),
                Name = "Ali",
                Telephone = "647-222-2222",
                Age = 32
            };
            
            GenericDataAccessClass<TestTablePoco> testTableRepository = new GenericDataAccessClass<TestTablePoco>(testtablePoco);

            testTableRepository.Add(testtablePoco);
            Console.WriteLine(testTableRepository.generatedSqlCommand);
            Console.ReadKey();

            testTableRepository.GetAll();
            Console.WriteLine(testTableRepository.generatedSqlCommand);
            Console.ReadKey();

            foreach (var x in testTableRepository.GetAll())
            {
                Console.WriteLine("{0} - {1} - {2} - {3}", x.Id, x.Name, x.Telephone, x.Age);
            }
            Console.ReadKey();

            testTableRepository.Remove(testtablePoco);
            Console.WriteLine(testTableRepository.generatedSqlCommand);
            Console.ReadKey();


            testTableRepository.Update(testtablePoco);
            Console.WriteLine(testTableRepository.generatedSqlCommand);
            Console.WriteLine("All commands executed successfully \n\n");
            Console.ReadKey();

 //===========================================Test SQL DB ===================================================
            ApplicantResumePoco applicantResume = new ApplicantResumePoco
            {
                Id = Guid.NewGuid(),
                Applicant = new Guid("8500ec15-5268-6944-5937-58ad10b82807"),
                Resume = "test2",
                LastUpdated = DateTime.Now
            };


            GenericDataAccessClass<ApplicantResumePoco> applicantResumeRepository = new GenericDataAccessClass<ApplicantResumePoco>(applicantResume);

            applicantResumeRepository.Add(applicantResume);
            Console.WriteLine(applicantResumeRepository.generatedSqlCommand);
            Console.ReadKey();

            applicantResumeRepository.GetAll();
            Console.WriteLine(applicantResumeRepository.generatedSqlCommand);
            Console.ReadKey();

            foreach (var x in applicantResumeRepository.GetAll())
            {
                Console.WriteLine("{0} {1} " , x.Id, x.LastUpdated.ToString());
            }
            Console.ReadKey();

            applicantResumeRepository.Remove(applicantResume);
            Console.WriteLine(applicantResumeRepository.generatedSqlCommand);
            Console.ReadKey();

            applicantResumeRepository.Update(applicantResume);
            Console.WriteLine(applicantResumeRepository.generatedSqlCommand);
            Console.WriteLine("All commands executed successfully");
            Console.ReadKey();
        }
    }
}
