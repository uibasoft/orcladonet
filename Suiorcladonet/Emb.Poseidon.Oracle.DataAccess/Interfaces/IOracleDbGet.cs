using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emb.Poseidon.Utils.Result;
using Oracle.ManagedDataAccess.Client;

namespace Emb.Poseidon.Oracle.DataAccess.Interfaces
{
    public interface IOracleDbGet : IDisposable
    {
        /// <summary>
        /// Ejecuta un procedimiento almacenado.
        /// </summary>
        /// <param name="nameStoreProcedure">Nombre del procedimiento almacenado.</param>
        /// <param name="parameters">Lista de parámetros.</param>
        /// <returns>Resultado de la operación.</returns>
        ResultOperation IsExecuteStoreProcedure(string nameStoreProcedure, params OracleParameter[] parameters);
        /// <summary>
        /// Ejecuta un procedimiento almacenado y retorna un elemento DataSet.
        /// </summary>
        /// <param name="nameStoreProcedure">Nombre del procedimiento almacenado.</param>
        /// <param name="parameters">Lista de parámetros.</param>
        /// <returns>Elemento DataSet</returns>
        ResultElement<DataSet> DataSetExecuteStoreProcedure(string nameStoreProcedure, params OracleParameter[] parameters);
        /// <summary>
        /// Ejecuta un procedimiento almacenado y retorna un elemento DataSet.
        /// </summary>
        /// <param name="nameStoreProcedure">Nombre del procedimiento almacenado.</param>
        /// <param name="fetchSize">Tamaña de ida y vuelta para el fetch de información</param>
        /// <param name="parameters">Lista de parámetros.</param>
        /// <returns></returns>
        ResultElement<DataSet> DataSetExecuteStoreProcedure(string nameStoreProcedure, int fetchSize, params OracleParameter[] parameters);

        /// <summary>
        /// Ejecuta una consulta sql y retorna un elemento DataSet.
        /// </summary>
        /// <param name="sqlQuery">Consulta SQL.</param>
        /// <returns>Elemento DataSet resultado de la ejecución de la consulta.</returns>
        ResultElement<DataSet> DataSetExecute(string sqlQuery);
        /// <summary>
        /// Ejecuta una consulta sql y retorna un elemento DataRow.
        /// </summary>
        /// <param name="sqlQuery">Consulta SQL.</param>
        /// <returns>Elemento DataRow resultado de la ejecución de la consulta.</returns>
        ResultElement<DataRow> DataRowExecute(string sqlQuery);
    }
}
