using System;

namespace MadHotSpot.Dtos
{
    public class VisitorCrudDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string LastMac { get; set; }
        public string IdNumber { get; set; }
        public bool Active { get; set; }
        public Guid FirmaId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserProfile { get; set; }
        public string Comment { get; set; }
        public string MikrotikId { get; set; }
    }
}
