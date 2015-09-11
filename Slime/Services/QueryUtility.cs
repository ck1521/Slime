using Slime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slime.Services
{
    public static class QueryUtility
    {
        public static int GetStart(QueryOptions qo)
        {
            return (qo.CurrentPage - 1) * qo.PageSize;
        }

        public static int GetTotal(int count, int pageSize)
        {
            return (int)Math.Ceiling((double)count / pageSize);
        }
    }
}