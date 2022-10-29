using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class Meet : BaseModel
    {
        public string Name { get; set; }
        /// <summary>
        /// Kullanıcı bağlantı sayısı
        /// </summary>
        public string AccessLimit { get; set; }
        /// <summary>
        ///  Bağlantı hızı   
        /// </summary>
        public string RateLimit { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public DateTime PasswordExpire { get; set; }
        public string MikrotikId { get; set; }
        public string UserProfileName { get; set; }
    }
}
