using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AllinTool.Data.Repository.Interfaces
    {
    public interface ISQLConversionRepository
        {
        string ConvertMYSQLQueryTOMSSQLQuery(string mySqlQuery);
        string ConvertMSSQLQueryToMYSQLQuery(string msSQLQuery);

        }
    }