using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vives
{
    abstract class Rekening
    { 
        public abstract double Saldo { get; set; }

        public abstract string VisualSaldo();
    }
}
