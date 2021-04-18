using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Backend.Extensions
{
    public static class DbDateHelper
    {
        public static DateTime? ToNullIfTooEarlyForDb(this DateTime date)
        {
            return (date >= (DateTime)SqlDateTime.MinValue) ? date : (DateTime?)null;
        }
    }
}