using System;
using System.Collections.Generic;
using System.Linq;
using Cassandra;
using Ebay.Product.Utilities.Configuration.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
//using Ebay.Product.Entities;

namespace Ebay.Product.DAL
{
    public class ProductRepository : IProductRepository
    {
        private CassandraConfig cassandraConfiguration;
        private ISession session;

        public ProductRepository(IOptions<CassandraConfig> configuration)
        {
            this.cassandraConfiguration = configuration.Value;
        }


        public ISession Session
        {
            get
            {
                if (session == null)
                {
                    Cluster cluster = Cluster.Builder().WithCredentials(cassandraConfiguration.UserName, cassandraConfiguration.Password).AddContactPoint(cassandraConfiguration.ServerName).Build();
                    session = cluster.Connect(cassandraConfiguration.KeySpaceName);
                }
                return session;
            }
        }

        public void Add(Entities.Product prod)
        {
            Session.Execute(String.Format("insert into product (productid, productname, productdesc, price) values ({0}, '{1}', '{2}', {3})", prod.ProductId, prod.ProductName, prod.ProductDesc, prod.Price));
        }

        public IEnumerable<Entities.Product> GetAll()
        {
            RowSet result = Session.Execute("select * from product");
            List<Entities.Product> productList = new List<Entities.Product>();
            foreach (var val in result)
            {
                productList.Add(new Entities.Product() { ProductId = new Guid(Convert.ToString(val["productid"])), ProductName = Convert.ToString(val["productname"]), ProductDesc = Convert.ToString(val["productdesc"]), Price = Convert.ToDecimal(val["price"]) });
            }
            return productList;
        }

        public Entities.Product GetByID(Guid id)
        {
            Row result = Session.Execute(String.Format("select * from product where productid={0}", id)).First();
            if (result != null)
            {
                return new Entities.Product() { ProductId = new Guid(Convert.ToString(result["productid"])), ProductName = Convert.ToString(result["productname"]), ProductDesc = Convert.ToString(result["productdesc"]), Price = Convert.ToDecimal(result["price"]) };
            }
            return null;
        }

        public void Delete(Guid id)
        {
            Session.Execute(String.Format("delete from product where productid = {0}", id));
        }

        public void Update(Entities.Product prod)
        {
            Session.Execute(String.Format("update product set productname = '{0}',productdesc = '{1}', price = {2} where productid = {3}", prod.ProductName, prod.ProductDesc, prod.Price, prod.ProductId));
        }
    }
}
