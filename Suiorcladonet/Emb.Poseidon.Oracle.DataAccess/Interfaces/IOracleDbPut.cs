using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emb.Poseidon.Utils.Result;
using Oracle.ManagedDataAccess.Client;

namespace Emb.Poseidon.Oracle.DataAccess.Interfaces
{
    public interface IOracleDbPut : IDisposable
    {
        /// <summary>
        /// Ejecuta una consulta SQL a partir de una transacción.
        /// </summary>
        /// <param name="sqlQuery">Consulta SQL.</param>
        /// <param name="oracleTransaction">Transaccion relacionada.</param>
        /// <returns>Resultado de la operación de la ejecución de la consulta SQL.</returns>
        ResultElement<int> PutData(string sqlQuery, OracleTransaction oracleTransaction);
        /// <summary>
        /// Ejecuta una consulta SQL a partir de una conexion.
        /// </summary>
        /// <param name="sqlQuery">Consulta SQL.</param>
        /// <param name="oracleConexion">Conexion relacionada.</param>
        /// <returns>Resultado de la operación de la ejecución de la consulta SQL.</returns>
        ResultElement<int> PutData(string sqlQuery, OracleConnection oracleConexion);
    }
}
