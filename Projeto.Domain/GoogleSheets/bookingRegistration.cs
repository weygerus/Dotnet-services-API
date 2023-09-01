using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Domain.GoogleSheets
{
    public class BookingRegistration
    {
        public string Name { get; set; }

        public string Document { get; set; }

        public string EventType { get; set; }

        public string Description { get; set; }

        public DateTime EventDateTime { get; set; }
    }
}