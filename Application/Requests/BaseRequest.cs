using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests
{
    public class BaseRequest
    {
        public CancellationToken CancellationToken { get; set; }
    }
}
