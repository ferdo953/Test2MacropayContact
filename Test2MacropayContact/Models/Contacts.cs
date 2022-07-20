using System.Collections.Generic;

namespace Test2MacropayContact.Models
{
    public class Contacts
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public List<string> addressLines { get; set; }
    }

    public class Adress
    {
        public int Id { get; set; }
    }
}
