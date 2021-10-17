using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Entities.MongoDB
{
    public class OrderLines
    {
        #region ProductInfo

        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        #endregion

        #region OrderDetailsInfo

        public int ProductQuantity { get; set; }
        public decimal ProductUnitPrice { get; set; }

        #endregion
    }
}
