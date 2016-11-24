using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emb.Poseidon.Oracle.DataAccess.Interfaces
{
    public interface IOracleRepository : IDisposable
    {
        /// <summary>
        /// Propiedad que permite consultar datos en el repositorio de datos Oracle.
        /// </summary>
        IOracleDbGet DbGet { get; }
        /// <summary>
        /// Propiedad que permite persistir datos en el repositorio de datos Oracle.
        /// </summary>
        IOracleDbPut DbPut { get; }
    }
}
