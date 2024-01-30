using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class InternalCustomer
    {
        public int Id { get; set; }

        [StringLength(20,MinimumLength=10) ]
        public string? Name { get; set; }

        [PasswordPropertyText]
        [StringLength(16,MinimumLength =8)]
        public string? Password { get; set; }

        public int Age { get; set; }
        public string? Address { get; set; }
    }
}
