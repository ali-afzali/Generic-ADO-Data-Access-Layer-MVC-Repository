using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenericDataAccessLayer
{
    
    public class TestTablePoco : IPoco
    {
       
        private Dictionary<string, dynamic> _fields = new Dictionary<string, dynamic>();
        public TestTablePoco()
        {
            _fields.Add("Id", null);
            _fields.Add("Name", null);
            _fields.Add("Telephone", null);
            _fields.Add("Age", null);
        }


        public string DbType
        {
            get { return @"OleDB"; }
        }

        public string TableName
        {
            get { return @"[TestTable]"; }
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

        public string Name
        {
            get { return (string)_fields["Name"]; }
            set { _fields ["Name"]= value; }
        }
        public string Telephone
        {
            get { return (string)_fields["Telephone"]; }
            set { _fields ["Telephone"] = value; }
        }
        public int Age
        {
            get { return (int)_fields["Age"]; }
            set { _fields ["Age"]= value; }
        }

        public void SetItems (List<dynamic> rawItems)
        {
            Id = (Guid) rawItems[0];
            Name = (string)rawItems[1];
            Telephone = (string) rawItems[2];
            Age = (int)rawItems[3];
        }

        
    }

}
