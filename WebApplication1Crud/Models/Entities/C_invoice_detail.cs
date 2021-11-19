using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDweb.Models.Entities
{
    public class C_invoice_detail
    {
        public int ID { get; set; }
        public int id_invoice { get; set; }
        public string description { get; set; }
        public int value { get; set; }

    }
}
