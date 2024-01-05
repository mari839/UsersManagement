using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; } 
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
