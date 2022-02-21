using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarNameInvalid = "Araba ismi en az 3 harf olmalıdır.";
        public static string CarPriceInvalid = "Arabanın fiyatı sıfır(0)'dan büyük olmalıdır.";
        public static string CarMaintenanceTime = "Sistem bakımda!";
        public static string RentalReturnDate = "Araba teslim edilmedi.";
        internal static string CountOfImageError = "Bir arabanın en fazla 5 adet resmi olabilir.";
        internal static string NoCarImageError = "Arabaya ait resim bulunmamaktadır.";
    }
}
