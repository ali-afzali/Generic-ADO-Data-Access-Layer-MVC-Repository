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

            TestTablePoco testtablePoco = new TestTablePoco();

            testtablePoco.Id = Guid.NewGuid();
            testtablePoco.Name = "Ali";
            testtablePoco.Telephone = "647-222-2222";
            testtablePoco.Age = 32;

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

            //testTableRepository.Remove(testtablePoco);
            //Console.WriteLine(testTableRepository.generatedSqlCommand);
            //Console.ReadKey();


            testTableRepository.Update(testtablePoco);
            Console.WriteLine(testTableRepository.generatedSqlCommand);
            Console.ReadKey();





            ApplicantResumePoco applicantResume = new ApplicantResumePoco();

            applicantResume.Id = Guid.NewGuid();
            applicantResume.Applicant = new Guid("8500ec15-5268-6944-5937-58ad10b82807");
            applicantResume.Resume = "test2";
            applicantResume.LastUpdated = DateTime.Now;

            GenericDataAccessClass<ApplicantResumePoco> applicantResumeRepository = new GenericDataAccessClass<ApplicantResumePoco>(applicantResume);

            applicantResumeRepository.Add(applicantResume);
            Console.WriteLine(applicantResumeRepository.generatedSqlCommand);
            Console.ReadKey();

            applicantResumeRepository.GetAll();
            Console.WriteLine(applicantResumeRepository.generatedSqlCommand);
            Console.ReadKey();

            foreach (var x in applicantResumeRepository.GetAll())
            {
                Console.WriteLine(x.Id + " " + x.LastUpdated.ToString());
            }
            Console.ReadKey();

            applicantResumeRepository.Remove(applicantResume);
            Console.WriteLine(applicantResumeRepository.generatedSqlCommand);
            Console.ReadKey();

            applicantResumeRepository.Update(applicantResume);
            Console.WriteLine(applicantResumeRepository.generatedSqlCommand);
            Console.ReadKey();
        }
    }
}
