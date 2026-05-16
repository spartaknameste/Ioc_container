using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ioc
{
    internal class Mark
    {
        public Type ImplementationType { get; set; }
        public Lifetime Lifetime { get; set; }
        public object SingletonInstance { get; set; }

    }
}
