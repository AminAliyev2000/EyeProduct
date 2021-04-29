using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeProduct
{
    public static class ProdDetails
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>
                {
                    new Product
                    {
                        Name = "Mercedes",
                        ProductImage = @"merc.png"
                    },

                    new Product
                    {
                        Name = "BMW",
                        ProductImage = @"bmw.png"
                    },

                    new Product
                    {
                        Name = "Subaru",
                        ProductImage = @"subaru.png"
                    }

                };
        }
    }
}
