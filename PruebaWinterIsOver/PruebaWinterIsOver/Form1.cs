using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinterIsOverNuget.Models;
using WinterIsOverNuget.Services;

namespace PruebaWinterIsOver
{
    public partial class Form1 : Form
    {
        List<string> ListCustomerIDs;
        ServiceNorthWind service;

        public Form1()
        {
            InitializeComponent();
            service = new ServiceNorthWind();
            this.ListCustomerIDs = new List<string>();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            CustomerList customerList = await this.service.GetCustomerListAsync();

            foreach (Customer cus in customerList.Customers) {

                this.ListCustomerIDs.Add(cus.IdCustomer);
                this.lstCustomers.Items.Add(cus.Contact);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            int seleccionado = this.lstCustomers.SelectedIndex;
            string idCustomer = this.ListCustomerIDs[seleccionado];

            Customer customer = await this.service.FindCustomer(idCustomer);

            this.txtCity.Text = customer.City;
            this.txtCompany.Text = customer.Company;
            this.txtContact.Text = customer.Contact;
            this.txtCustom.Text = customer.IdCustomer;

        }
    }
}
