using System;

namespace GymBro.Models
{
    public class Person : EntityBase
    {
        public String FullName { get; set; }
        public String DisplayName { get; set; }
        public String Initials { get; set; }
        public String ColorCode { get; set; }
    }
}