using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1Crud.Models.Entities;

namespace CRUDweb.Models.Entities
{
    public class C_invoice
    {
        public int ID { get; set; }
        public int id_client { get; set; }
        public int cod { get; set; }

        public List <C_invoice_detail> listInvoicedetails { get; set; }

    }
}
