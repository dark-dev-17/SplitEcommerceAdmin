using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Catalogos
{
    public class OpenPayResponse
    {
        public string Type { get; set; }
        public DateTime Event_date { get; set; }
        public OpenPayTransac Transaction { get; set; }
    }
    public class OpenPayTransac
    {
        public string Id { get; set; }
        public string Autorization { get; set; }
        public string Operation_type { get; set; }
        public string Transactin_type { get; set; }
        public Card Card { get; set; }
        public string Estatus { get; set; }
        public bool Conciliated { get; set; }
        public DateTime Creation_date { get; set; }
        public DateTime Operation_date { get; set; }
        public string Descripcion { get; set; }
        public string Error_message { get; set; }
        public string Order_id { get; set; }
        public int Error_code { get; set; }
        public float amount { get; set; }
        public Customer Customer { get; set; }
        public string Currency { get; set; }
        public string Method { get; set; }
    }

    public class Card
    {
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Address { get; set; }
        public string Card_number { get; set; }
        public string Holder_name { get; set; }
        public string Expiration_year { get; set; }
        public string Expiration_month { get; set; }
        public bool Allows_charges { get; set; }
        public bool Allows_payouts { get; set; }
        public string bank_name { get; set; }
        public string bank_code { get; set; }
    }

    public class Customer
    {
        public string Name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Phone_number { get; set; }
        public string Address { get; set; }
        public DateTime Creation_date { get; set; }
        public string External_id { get; set; }
        public string Clabe { get; set; }
    }
}
