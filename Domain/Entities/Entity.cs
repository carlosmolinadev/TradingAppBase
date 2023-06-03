using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Entity<Tid>
    {
        public Tid Id { get; set; }
    }
}
