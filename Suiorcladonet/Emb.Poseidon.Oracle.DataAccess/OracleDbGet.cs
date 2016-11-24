using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emb.Poseidon.Oracle.DataAccess.Interfaces;
using Emb.Poseidon.Oracle.DataAccess.Resources;
using Emb.Poseidon.Utils.Result;
using Oracle.ManagedDataAccess.Client;

namespace Emb.Poseidon.Oracle.DataAccess
{
    public class OracleDbGet : OracleDbConexion, IOracleDbGet
    {
        #region Atributos

        public const string DatasetName = "dsDatos";
        public const string DatarowName = "drDatos";

        private readonly string _codigoErrorException = ResourceOracleMessages.CodigoErrorException;
        private readonly string _errorOpenConection = ResourceOracleMessages.ErrorOpenConection;

        #endregion

        #region Constructor
        public OracleDbGet(string connectionString) : base(connectionString)
        {
            try
            {
                if (!OpenConnection())
                    throw new Exception(ResourceOracleMessages.ErrorOpenConection);
            }
            catch (Exception ex)
            {
                if (Logger != null)
                {
                    var mensaje = $"Error en {GetType().Name}.{System.Reflection.MethodBase.GetCurrentMethod().Name}: {ex.Message}.";
                    Logger.Error(mensaje);
                }
            }
        }

        #endregion

        #region Core

        public ResultElement<DataRow> DataRowExecute(string sqlQuery)
        {
            var result = new ResultElement<DataRow>() { Element = null };
            try
            {
                var res = DataSetExecute(sqlQuery);
                if (res.Element == null)
                {
                    return result;
                }
                var dataSet = res.Element;
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    result.Element = dataSet.Tables[0].Rows[0];
                }
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

        public ResultElement<DataSet> DataSetExecute(string sqlQuery)
        {
            var result = new ResultElement<DataSet>() { Element = null };
            try
            {
                if (!OpenConnection())
                {
                    result.Errors.Add(_errorOpenConection);
                    return result;
                }
                var oracleCommand = base.OracleConnection.CreateCommand();
                oracleCommand.CommandType = CommandType.Text;
                oracleCommand.CommandText = sqlQuery;
                using (var oracleDataAdapter = new OracleDataAdapter(oracleCommand))
                {
                    var dataSet = new DataSet(DatasetName);
                    oracleDataAdapter.Fill(dataSet);
                    result.Element = dataSet;
                }
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
                CloseConnection();
            }
            return result;
        }

        public ResultElement<DataSet> DataSetExecuteStoreProcedure(string nameStoreProcedure, params OracleParameter[] parameters)
        {
            var result = new ResultElement<DataSet>() { Element = null };
            try
            {
                if (!OpenConnection())
                {
                    result.Errors.Add(_errorOpenConection);
                    return result;
                }
                var command = new OracleCommand
                {
                    Connection = OracleConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = nameStoreProcedure,
                    BindByName = true
                };
                if (parameters != null)
                {
                    foreach (var p in parameters)
                        command.Parameters.Add(p);
                }
                using (var oracleDataAdapter = new OracleDataAdapter(command))
                {
                    var dataSet = new DataSet(DatasetName);
                    oracleDataAdapter.Fill(dataSet);
                    result.Element = dataSet;
                }
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
                CloseConnection();
            }
            return result;
        }

        public ResultOperation IsExecuteStoreProcedure(string nameStoreProcedure, params OracleParameter[] parameters)
        {
            var result = new ResultOperation();
            try
            {
                if (!OpenConnection())
                {
                    result.Errors.Add(_errorOpenConection);
                    return result;
                }
                var command = base.OracleConnection.CreateCommand();
                command.Parameters.Clear();
                command.BindByName = true;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = nameStoreProcedure;
                if (parameters != null)
                {
                    foreach (var p in parameters)
                    {
                        command.Parameters.Add(p);
                    }

                }
                var res = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                result.Errors.Add(_codigoErrorException);
                result.Errors.Add(mensaje);
                if (Logger != null)
                {
                    var msj =
                        $"Error en {GetType().Name}.{System.Reflection.MethodBase.GetCurrentMethod().Name}: {mensaje}.";
                    Logger.Error(msj);
                }
            }
            finally
            {
                CloseConnection();
            }
            return result;
        }

        #endregion
    }
}
