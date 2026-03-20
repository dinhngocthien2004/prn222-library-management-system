using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Entities
{
    public class BookStatisticsViewModel
    {
        public int TotalBooks { get; set; }
        public int TotalCopies { get; set; }
        public int TotalBorrowedCopies { get; set; }
        public int TotalAvailableCopies { get; set; }
        public List<TopBookViewModel> TopBooks { get; set; }
    }
}
