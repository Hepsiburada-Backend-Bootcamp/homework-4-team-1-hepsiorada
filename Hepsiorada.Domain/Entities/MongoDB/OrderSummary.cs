using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Entities.MongoDB
{
    public class OrderSummary
    {
        public OrderSummary()
        {
            this.OrderLines = new List<OrderLines>();//TODO check
        }

        [JsonIgnore]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        #region UserInfo
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        #endregion

        #region OrderHeaderInfo

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public decimal TotalPrice { get; set; } = 0;
        public string OrderId { get; set; }

        #endregion

        public List<OrderLines> OrderLines { get; set; }

    }
}
