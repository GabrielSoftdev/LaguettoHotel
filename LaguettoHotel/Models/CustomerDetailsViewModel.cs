using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaguettoHotel.Models
{
    public class CustomerDetailsViewModel
    {
        //Student Detail Model   
        public  Cliente _cliente { get; set; }

        //Student Exam Result List Model   
        public IEnumerable<Reserva> _reserva { get; set; }
    }
}