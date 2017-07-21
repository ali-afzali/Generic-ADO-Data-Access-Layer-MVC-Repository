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



            ApplicantResumePoco applicantResume = new ApplicantResumePoco();

            applicantResume.Id = Guid.NewGuid();
            applicantResume.Applicant = new Guid("8500ec15-5268-6944-5937-58ad10b82807");
            applicantResume.Resume = "test2";
            applicantResume.LastUpdated = DateTime.Now;

            GenericDataAccessClass<ApplicantResumePoco> applicantResumeRepository = new GenericDataAccessClass<ApplicantResumePoco>(applicantResume);



            applicantResumeRepository.Add(applicantResume);
            Console.WriteLine(applicantResumeRepository.generatedSqlCommand);

            applicantResumeRepository.GetAll();
            Console.WriteLine(applicantResumeRepository.generatedSqlCommand);


            foreach (var x in applicantResumeRepository.GetAll())
            {
                Console.WriteLine(x.Id + " " + x.LastUpdated.ToString());
            }

            Console.ReadKey();
            applicantResumeRepository.Remove(applicantResume);
            Console.WriteLine(applicantResumeRepository.generatedSqlCommand);

            applicantResumeRepository.Update(applicantResume);
            Console.WriteLine(applicantResumeRepository.generatedSqlCommand);

            Console.ReadKey();
        }
    }
}
