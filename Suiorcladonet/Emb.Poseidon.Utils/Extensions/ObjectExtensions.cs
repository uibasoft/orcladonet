using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emb.Poseidon.Utils.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToStringEmpty(this object obj)
        {
            return obj?.ToString() ?? string.Empty;
        }
    }

    public static class StringExtensions
    {
        public static string ToStringEmpty(this string obj)
        {
            return obj ?? string.Empty;
        }
    }
}
