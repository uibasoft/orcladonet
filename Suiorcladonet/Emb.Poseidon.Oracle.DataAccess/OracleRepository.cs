using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emb.Poseidon.Oracle.DataAccess.Interfaces;

namespace Emb.Poseidon.Oracle.DataAccess
{
    public class OracleRepository : IOracleRepository
    {
        #region Constructor
        public OracleRepository(IOracleDbGet sqlDbGet, IOracleDbPut sqlDbPut)
        {
            DbGet = sqlDbGet;
            DbPut = sqlDbPut;
        }
        #endregion

        #region Propiedades

        public IOracleDbGet DbGet { get; }

        public IOracleDbPut DbPut { get; }

        #endregion

        #region Dispose

        public void Dispose()
        {
            DbGet.Dispose();
            DbPut.Dispose();
        }

        #endregion
    }
}
