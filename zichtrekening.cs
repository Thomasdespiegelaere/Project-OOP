using Project_OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vives
{
    internal class Zichtrekening:Rekening
    {
        public override double Saldo { get; set; }

        public override string VisualSaldo()
        {
            return $"{Math.Round(Saldo, 2)} \u20AC";
        }
    }
}
