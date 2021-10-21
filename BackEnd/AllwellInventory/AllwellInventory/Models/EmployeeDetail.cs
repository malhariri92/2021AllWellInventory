using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllwellInventory.Models
{
    public class EmployeeDetail
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LNname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}