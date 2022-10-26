using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Dtos
{
    public class MeetCrudDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Kullanıcı bağlantı sayısı
        /// </summary>
        public string AccessLimit { get; set; }
        /// <summary>
        ///  Bağlantı hızı sayısı 
        /// </summary>
        public string RateLimit { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public DateTime PasswordExpire { get; set; }
        public bool Silindi { get; set; }
        public Guid FirmaId { get; set; }
        public string MikrotikId { get; set; }

    }
}
