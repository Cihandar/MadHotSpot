using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Dtos
{
    public class HotspotUserProfileDto
    { 
        public string SharedUsers { get; set; }
 
        public string SessionTimeout { get; set; }
 
        public string RateLimit { get; set; }
 
        public string OutgoingPacketMark { get; set; }
 
        public string OutgoingFilter { get; set; }
 
        public string OpenStatusPage { get; set; }
 
        public string OnLogout { get; set; }
 
        public string OnLogin { get; set; }
 
        public string Name { get; set; }
 
        public string MacCookieTimeout { get; set; }
 
        public string StatusAutorefresh { get; set; }
 
        public string KeepaliveTimeout { get; set; }
 
        public string IncomingFilter { get; set; }
 
        public string IdleTimeout { get; set; }
 
        public string AdvertiseUrl { get; set; }
 
        public string AdvertiseTimeout { get; set; }
 
        public string AdvertiseInterval { get; set; }
 
        public bool Advertise { get; set; }
 
        public string AddressPool { get; set; }
 
        public string AddressList { get; set; }
 
        public bool AddMacCookie { get; set; }
 
        public string Id { get; set; }
 
        public string IncomingPacketMark { get; set; }
 
        public bool TransparentProxy { get; set; }
    }
}
