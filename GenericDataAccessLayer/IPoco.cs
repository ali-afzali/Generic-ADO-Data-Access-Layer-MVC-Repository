using System;
using System.Collections.Generic;

namespace GenericDataAccessLayer
{
    public interface IPoco
    {
        Guid Id { get; set; }
        Dictionary<string, dynamic> Fields { get; set; }
        void SetItems(List<dynamic> rawItems);
        string TableName { get; }
        string DbType { get; }
    }
}