using System;
using System.Collections.Generic;

namespace Ebay.Product.DAL
{
    public interface IProductRepository
    {
        void Add(Entities.Product prod);
        void Delete(Guid id);
        IEnumerable<Entities.Product> GetAll();
        Entities.Product GetByID(Guid id);
        void Update(Entities.Product prod);
    }
}