using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emb.Poseidon.Oracle.DataAccess.Interfaces;
using Emb.Poseidon.Oracle.DataAccess.Resources;
using Emb.Poseidon.Utils.Result;
using Oracle.ManagedDataAccess.Client;

namespace Emb.Poseidon.Oracle.DataAccess
{
    public class OracleDbPut : OracleDbConexion, IOracleDbPut
    {
        #region Atributos

        private readonly string _codigoErrorException = ResourceOracleMessages.CodigoErrorException;
        private OracleCommand _sqlComand;

        #endregion

        #region Constructor

        public OracleDbPut(string connectionString) : base(connectionString)
        {

        }

        #endregion

        #region Core

        public ResultElement<int> PutData(string sqlQuery, OracleTransaction oracleTransaction)
        {
            var result = new ResultElement<int>();
            try
            {
                _sqlComand = new OracleCommand(sqlQuery, oracleTransaction.Connection)
                {
                    Transaction = oracleTransaction
                };
                var executeScalarResult = _sqlComand.ExecuteScalar();
                result.Element = Convert.ToInt32(executeScalarResult);
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                result.Errors.Add(_codigoErrorException);
                result.Errors.Add(mensaje);
                if (Logger != null)
                {
                    var msj = $"Error en {GetType().Name}.{System.Reflection.MethodBase.GetCurrentMethod().Name}: {mensaje}.";
                    Logger.Error(msj);
                }
            }
            finally
            {
                _sqlComand = null;
            }
            return result;
        }

        public ResultElement<int> PutData(string sqlQuery, OracleConnection oracleConexion)
        {
            var result = new ResultElement<int>();
            try
            {
                var moComand = new OracleCommand(sqlQuery, oracleConexion);
                result.Element = moComand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                result.Errors.Add(_codigoErrorException);
                result.Errors.Add(mensaje);
                if (Logger != null)
                {
                    var msj = $"Error en {GetType().Name}.{System.Reflection.MethodBase.GetCurrentMethod().Name}: {mensaje}.";
                    Logger.Error(msj);
                }
            }
            return result;
        }

        #endregion

    }
}
