using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Oracle.ManagedDataAccess.Client;

namespace Emb.Poseidon.Oracle.DataAccess
{
    public abstract class OracleDbConexion : IDisposable
    {
        public OracleConnection OracleConnection { get; set; }

        protected ILog Log;

        public ILog Logger => Log ?? (Log = LogManager.GetLogger("LogEmbPoseidonFrm"));

        #region Constructor

        protected OracleDbConexion(string connectionString)
        {
            try
            {
                if (OracleConnection == null)
                {
                    OracleConnection = new OracleConnection(connectionString);
                }
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

        public bool OpenConnection()
        {
            var result = true;
            try
            {
                if (OracleConnection != null && OracleConnection.State == ConnectionState.Closed)
                {
                    OracleConnection.Open();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result = false;
                if (Logger != null)
                {
                    var mensaje = $"Error en {GetType().Name}.{System.Reflection.MethodBase.GetCurrentMethod().Name}: {ex.Message}.";
                    Logger.Error(mensaje);
                }
                return result;
            }

        }

        public bool CloseConnection()
        {
            var result = false;
            try
            {
                if (OracleConnection != null && OracleConnection.State == ConnectionState.Open)
                {
                    OracleConnection.Close();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result = false;
                if (Logger != null)
                {
                    var mensaje = $"Error en {GetType().Name}.{System.Reflection.MethodBase.GetCurrentMethod().Name}: {ex.Message}.";
                    Logger.Error(mensaje);
                }
                return result;
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            CloseConnection();
            OracleConnection = null;
        }

        #endregion

    }
}
