using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WinterIsOverNuget.Models;

namespace WinterIsOverNuget.Services
{
    public class ServiceNorthWind
    {
        public async Task<CustomerList> GetCustomerListAsync() {

            WebClient client = new WebClient();

            client.Headers["content-type"] = "application/json";

            string url = "https://services.odata.org/V4/Northwind/Northwind.svc/Customers";
            string datajson = await client.DownloadStringTaskAsync(url);

            CustomerList customers = JsonConvert.DeserializeObject<CustomerList>(datajson);

            return customers;
        }

        public async Task<Customer> FindCustomer(string idcustomer) {

            CustomerList customerList = await this.GetCustomerListAsync();

            Customer customer = customerList.Customers.FirstOrDefault(x => x.IdCustomer == idcustomer);

            return customer;
        }
    }
}
