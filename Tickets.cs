using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPE312_PROJECT_TICKETSYSTEM
{
    public class Tickets
    {
        public int TId { get; set; } = 0;
        public int CId { get; set; }= 0;

        public string Hall { get; set; } = null;    
        public DateTime? TicketDate { get; set; } = null;
        public string TicketTime { get; set; } = null;
        public float Price { get; set; } = 0;
        public string MovieName { get; set; } = null;
        public Tickets(int TId, int CId, string MovieName, DateTime TicketDate, string TicketTime, string Hall, float Price)
        {
            this.TId = TId;
            this.CId = CId;
            this.MovieName = MovieName;
            this.TicketDate = TicketDate;
            this.TicketTime = TicketTime;
            this.Hall = Hall;
            this.Price = Price;
        }
    }

}
