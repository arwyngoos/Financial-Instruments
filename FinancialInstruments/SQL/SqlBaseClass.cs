using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.SQL
{
    public static class SqlBaseClass
    {
        internal static readonly string _connectionString =
            "server=" + Settings.DataBaseEngine + ";" + // Network address
            "Trusted_connection=true;" + // Secured by Windows Authentication or SSPI
            "database=" + Settings.DataBase + ";" + // Select 'database' associated with teh connection server
            "connection timeout = 30;" + // Connection time-out
            "Integrated Security=SSPI";
    }
}
