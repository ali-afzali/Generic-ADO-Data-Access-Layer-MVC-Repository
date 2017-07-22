using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenericDataAccessLayer
{

    public class ApplicantResumePoco : IPoco
    {
        private Dictionary<string, dynamic> _fields = new Dictionary<string, dynamic>();
        public ApplicantResumePoco()
        {
            _fields.Add("Id", null);
            _fields.Add("Applicant", null);
            _fields.Add("Resume", null);
            _fields.Add("Last_Updated", null);
        }

        public string DbType => @"SQL";

        public string TableName => @"[dbo].[Applicant_Resumes]";

        public Dictionary<string, dynamic> Fields
        {
            get => _fields;
            set => _fields = value;
        }
        
        public Guid Id
        {
            get => (Guid)_fields["Id"];
            set => _fields["Id"] = value;
        }

        public Guid Applicant
        {
            get => (Guid)_fields["Applicant"];
            set => _fields["Applicant"] = value;
        }
        public string Resume
        {
            get => (string)_fields["Resume"];
            set => _fields["Resume"] = value;
        }
        public DateTime? LastUpdated
        {
            get => (DateTime?)_fields["Last_Updated"];
            set => _fields["Last_Updated"] = value;
        }

        public void SetItems(List<dynamic> rawItems)
        {
            Id = (Guid)rawItems[0];
            Applicant = (Guid)rawItems[1];
            Resume = (string)rawItems[2];
            LastUpdated = (DateTime?)rawItems[3];
        }


    }

}
