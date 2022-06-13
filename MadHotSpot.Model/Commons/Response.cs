using System;
using System.ComponentModel;
using MadHotSpot.Extention;

namespace MadHotSpot.Model.Commons
{
    public class Response
    {
        private bool success { get; set; } = true;

        public bool Success
        {
            get => success;
            set => success = value;
        }

        private string message { get; set; }

        public string Message
        {
            get => string.IsNullOrWhiteSpace(message) ? ResultCode.Success.GetDescription() : message;
            set => message = value;
        }

        public object Result { get; set; }

        private ResultCode resultCode { get; set; } = ResultCode.Success;

        public ResultCode ResultCode
        {
            get => resultCode;
            set
            {
                resultCode = value;
                success = resultCode == ResultCode.Success;
                message = string.IsNullOrWhiteSpace(message) ? resultCode.GetDescription() : message;
            }
        }

        public string RedirectUrl { get; set; }

        public Guid RestaurantId { get; set; }

        public double IadeTutar { get; set; }
        public double IadeGun { get; set; }
        public string Doviz { get; set; }
        public int KullaniciSayisi { get; set; }
    }



    public enum ResultCode
    {
        //General
        [Description("İşlem başarılı.")]
        Success = 700,
        //Exception
        [Description("Bir hata ile karşılaşıldı.")]
        ExceptionError = 800,
    }

}
