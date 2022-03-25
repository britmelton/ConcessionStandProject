using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ConcessionStandProject
{
    public class LiveProductRepository : IProductRepository
    {       
        private readonly IConnectionStringProvider connectionStringProvider;

        public LiveProductRepository(IConnectionStringProvider connectionStringProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
        }

        public Product Find(int sku)
        {
            using var conn = new MySqlConnection(connectionStringProvider.GetConnectionString());
            return (Product)conn.QuerySingle<Product>("SELECT name, price, sku, image FROM PRODUCT WHERE Sku = @sku",
                new {sku = sku});
        }

        public IEnumerable<Product> GetAllProducts()
        {
            using var conn = new MySqlConnection(connectionStringProvider.GetConnectionString());
            return conn.Query<Product>("SELECT name, price, sku, image FROM PRODUCT;");
        }



    }
}
