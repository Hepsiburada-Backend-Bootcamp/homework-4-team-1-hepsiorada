using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Entities.MongoDB
{
    public class OrderSummary
    {
        #region UserInfo
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        #endregion

        #region OrderHeaderInfo

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public int ProductQuantity { get; set; } = 0; //TODO
        public decimal TotalPrice { get; set; } = 0; //TODO

        #endregion

        List<OrderLines> OrderLines { get; set; }

    }
}
