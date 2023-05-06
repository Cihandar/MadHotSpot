using MadHotSpot.Interfaces;
using MadHotSpot.Models;
using MadHotSpot.Models.Enum;
using MadHotSpot.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Applications.CustomerInfos
{
    public class CustomerInfoCrud : ICustomerInfo
    {
        OtelAppDbContext _context;

        public CustomerInfoCrud(OtelAppDbContext context)
        {
            _context = context;
        }

        public void SendCustomerInfoAsync(CustomerInfoViewModel customer)
        {
            try
            {
                CustomerInfo model = _context.H_CustomerInfo.Where(x => x.Email.Equals(customer.Email)).FirstOrDefault();
                if (model == null && customer.LoginType != LoginType.Staff)
                {
                    _context.H_CustomerInfo.Add(new CustomerInfo
                    {
                        Email = customer.Email,
                        PhoneNumber = customer.PhoneNumber,
                        FirmaId = customer.FirmaId,
                        LoginType = customer.LoginType,
                        RoomNumber = customer.RoomNumber,
                        BirthDate = customer.BirthDate
                    });
                }
            }
            catch (Exception ex)
            {
                
            }
         
  

        }
    }
}
