using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Exchange
    {
        public Exchange(string value, string displayName) { Value = value; DisplayName = displayName; }
        public int Id { get; set; }
        public string Value { get; set; }
        public string DisplayName { get; set; }
    }
}
