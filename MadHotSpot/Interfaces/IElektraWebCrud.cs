using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadHotSpot.Models;

namespace MadHotSpot.Interfaces
{
    public interface IElektraWebCrud
    {
        public Task<string> GetDepartmansContent();
        public Task<string> GetRevenuesContent();
        public Task<string> GetCurrenciesContent();
        public Task<string> SetCashFolio(CashFolioParameter cashFolioParameters,Guid FirmaId);
        public Task<string> GetCashPostingContent();
    }
}
