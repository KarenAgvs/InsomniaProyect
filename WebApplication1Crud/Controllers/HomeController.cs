using CRUDweb.Models.Entities;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1Crud.Models;
using WebApplication1Crud.Models.Entities;

namespace WebApplication1Crud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        database database = new database();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        public string Index()
        {
            C_invoice c_Invoice = new C_invoice();
            C_client client = new C_client();
            C_invoice_detail c_Invoice_Detail = new C_invoice_detail();

            return "nobody";
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string InsertUser([FromBody] C_client clients)
        {
            string output = $"insert into clients(name, last_name,document_id) values('{clients.name}', '{clients.last_name}', '{clients.document_id}')";
            string dbresponse = database.MySqlOperations(output);

            return dbresponse;

        }

        public string insertInvoice([FromBody] C_invoice invoice)
        {
            string sql = "";

            sql += $"Insert into invoice (id_client, cod) values ({invoice.id_client}, {invoice.cod});";
            sql += "SELECT @@IDENTITY as id;";

            foreach (C_invoice_detail item in invoice.listInvoicedetails)
            {
                sql += $"Insert into invoice_detail(id_invoice, description, value) values (@@IDENTITY,'{item.description}', {item.value});";

            }

            string output = database.MySqlOperations(sql);
            return output;
        }
        public IEnumerable<C_client> showClients()
        {
            string sql = "SELECT * FROM clients";
            DataTable dt = database.getData(sql);

            List<C_client> clientsList = new List<C_client>();
            clientsList = (from DataRow dr in dt.Rows
                           select new C_client()
                           {
                               ID = Convert.ToInt32(dr["ID"]),
                               name = dr["name"].ToString(),
                               last_name = dr["last_name"].ToString(),
                               document_id = Convert.ToInt32(dr["document_id"]),

                           }).ToList();
            return clientsList;
        }

        public IEnumerable<C_client> showClient(int id)
        {
            string sql = $"SELECT * FROM clients where id={id}";
            DataTable dt = database.getData(sql);

            List<C_client> clientsList = new List<C_client>();
            clientsList = (from DataRow dr in dt.Rows
                           select new C_client()
                           {
                               ID = Convert.ToInt32(dr["ID"]),
                               name = dr["name"].ToString(),
                               last_name = dr["last_name"].ToString(),
                               document_id = Convert.ToInt32(dr["document_id"]),

                           }).ToList();
            return clientsList;
        }

        public IEnumerable<C_invoice> showdetailsandinvoice(int id)
        {
            string sql = $"select i.*, id.* from invoice i, invoice_detail id where i.ID=id.id_invoice and id_invoice={id}";
            DataTable dt = database.getData(sql);

            List<C_invoice> invoicesList = new List<C_invoice>();
            List<C_invoice_detail> _Invoice_Details = new List<C_invoice_detail>();

            _Invoice_Details = (from DataRow dr in dt.Rows
                                select new C_invoice_detail()
                                {
                                    ID = Convert.ToInt32(dr["ID"]),
                                    id_invoice = Convert.ToInt32(dr["id_invoice"]),
                                    description = dr["description"].ToString(),
                                    value = Convert.ToInt32(dr["value"]),

                                }).ToList();
            invoicesList = (from DataRow dr in dt.Rows
                            select new C_invoice()
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                id_client = Convert.ToInt32(dr["id_client"]),
                                cod = Convert.ToInt32(dr["cod"]),
                                listInvoicedetails = _Invoice_Details,

                            }).ToList();
            return invoicesList;
        }


    }
}
