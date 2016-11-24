using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emb.Poseidon.Utils.Result
{
    public class ResultOperation
    {
        #region Constructor
        public ResultOperation()
        {
            Errors = new List<string>();
            Messages = new List<string>();
        }
        public ResultOperation(List<string> errors, List<string> messages)
        {
            Errors = errors ?? new List<string>();
            Messages = messages ?? new List<string>();
        }

        #endregion

        #region Propiedades

        public List<string> Errors { get; set; }
        public List<string> Messages { get; set; }

        public bool HasErrors => Errors.Any();
        public bool HasMessages => Messages.Any();

        #endregion
    }
}
