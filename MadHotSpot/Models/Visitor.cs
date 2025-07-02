namespace MadHotSpot.Models
{
    public class Visitor : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string RoomNumber { get; set; }
        public string LastMac { get; set; }
        public string IdNumber { get; set; }
        public bool Active { get; set; }
        public string UserProfile { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Kullanıcı bağlantı sayısı
        /// </summary>
        public string AccessLimit { get; set; }
        /// <summary>
        ///  Bağlantı hızı   
        /// </summary>
        public string RateLimit { get; set; }

        public string MikrotikId { get; set; }

    }
}
