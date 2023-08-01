using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{

    #region Login
    public class ElektraLoginResponse
    {
        public bool Success { get; set; }
        public string LoginToken { get; set; }

    }
    #endregion

    #region Global Request
    public class ElektraGlobalRequest
    {
        public string LoginToken { get; set; }
        public Parameters Parameters { get; set; }
        public string Action { get; set; } = "Function";
        public string Object { get; set; }
        public List<OrderBy> OrderBy { get; set; }
        public Paging Paging { get; set; }
        public List<object> Select { get; set; }
        public List<object> Where { get; set; }
        public string Tenant { get; set; }
        public string Usercode { get; set; }
        public string Password { get; set; }
    }

    public class OrderBy
    {
        public string Column { get; set; }
        public object Direction { get; set; }
    }

    public class Paging
    {
        public int ItemsPerPage { get; set; } = 100;
        public int Current { get; set; } = 1;
    }

    public class Parameters
    {
        public int HOTELID { get; set; }
    }

    public class CashFolioParameter : Parameters
    {
        public string RESID { get; set; }
        public int DEPID { get; set; }
        public int REVID { get; set; }
        public int DEPID_PAYMENT { get; set; }
        public string CTOTAL { get; set; }
        public int CURRENCYID { get; set; }
        public string Notes { get; set; }
    }
    #endregion

}
