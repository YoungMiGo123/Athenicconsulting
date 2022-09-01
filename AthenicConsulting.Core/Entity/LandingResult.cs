using AthenicConsulting.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthenicConsulting.Core.Entity
{
    public class LandingResult : BaseEntity
    {
        public Brand Brand { get; set; }
        public Campaign Campaign { get; set; }

    }
}
