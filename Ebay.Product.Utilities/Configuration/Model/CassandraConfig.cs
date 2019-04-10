using System;
using System.Collections.Generic;
using System.Text;

namespace Ebay.Product.Utilities.Configuration.Model
{
    public class CassandraConfig
    {
        public string ServerName { get; set; }

        public string KeySpaceName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
