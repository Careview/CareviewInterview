using CareviewTest.Data;
using CareviewTest.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareviewTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            using (var context = new CareviewDbContext())
            {
                var clients = context.Clients;
                foreach (var client in clients)
                {
                    Console.WriteLine(client.ToModel().Name);
                    Console.WriteLine(client.ToModel().TotalInvoices);
                    Console.WriteLine(client.ToModel().InvoiceQuantity);
                }
            }
            Console.ReadLine();
        }
    }
}
