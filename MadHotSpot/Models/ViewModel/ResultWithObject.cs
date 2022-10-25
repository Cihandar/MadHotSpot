using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models.ViewModel
{
    public class ResultWithObject<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<T> ListData { get; set; }
    }
}
