using MadHotSpot.Models;
using MadHotSpot.Models.Enum;
using MadHotSpot.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Interfaces
{
    public interface ICustomerInfo
    {
        public void SendCustomerInfoAsync(CustomerInfoViewModel customer);
    }
}
