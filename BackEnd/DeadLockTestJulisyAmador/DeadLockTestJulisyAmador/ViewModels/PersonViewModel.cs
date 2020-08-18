using DeadLockTestJulisyAmador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeadLockTestJulisyAmador.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int PositionId { get; set; }
        public string PositionDesc { get; set; }
        public List<PositionViewModel> Positions { get; set; }
    }
}
