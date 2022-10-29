using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class ResultJson
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Value { get; set; }
        public Guid Id { get; set; }
        public string MikrotikId { get; set; }
        public string UserProfileName { get; set; }
    }
}
