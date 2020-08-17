using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeadLockTestJulisyAmador.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<Person> Person { get; set; }
    }
}
