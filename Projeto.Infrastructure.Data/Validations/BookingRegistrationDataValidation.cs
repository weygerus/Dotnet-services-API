using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Infrastructure.Data;
using Projeto.Domain.GoogleSheets;

namespace Projeto.Infrastructure.Data.Validations
{
    public class BookingRegistrationDataValidation
    {
        public bool ValidateBookingRegistration(BookingRegistration bookingRegistrationRegister, string formattedEventDatetime)
        {
            var isAvailableRegister = 
                this.ValidateEventDatetimeFormat(bookingRegistrationRegister.EventType.ToString(),
                                                 formattedEventDatetime);

            if (isAvailableRegister)
            {
                return true;
            }

            return false;
        }
        public bool ValidateEventDatetimeFormat(string eventDatetime, string format)
        {
            DateTime datetimeReturn;

            if (DateTime.TryParseExact(eventDatetime, format, null, System.Globalization.DateTimeStyles.None, out datetimeReturn));
            {
                return true;
            }
        }

        public struct BookingRegistrationResponseDTO
        {
            public bool IsAvaliable { get; set; }
            
            public string Message { get; set; }
        }
    }
}