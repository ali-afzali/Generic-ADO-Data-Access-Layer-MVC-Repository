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
        

        public string TableName
        {
            get {return @"[dbo].[Applicant_Resumes]";}
        }
      
      public Dictionary<string, dynamic> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        [Key]public Guid Id
        {
            get { return (Guid)_fields["Id"];}
            set { _fields["Id"] = value;}
        }

        public Guid Applicant
        {
            get { return (Guid)_fields["Applicant"]; }
            set { _fields ["Applicant"]= value; }
        }
        public string Resume
        {
            get { return (string)_fields["Resume"]; }
            set { _fields ["Resume"]= value; }
        }
        public DateTime? LastUpdated
        {
            get { return (DateTime?)_fields["Last_Updated"]; }
            set { _fields ["Last_Updated"]= value; }
        }

        public void SetItems (List<dynamic> rawItems)
        {
            Id = (Guid) rawItems[0];
            Applicant = (Guid)rawItems[1];
            Resume = (string) rawItems[2];
            LastUpdated = (DateTime ?)rawItems[3];
        }

        
    }

}
