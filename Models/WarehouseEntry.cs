using System;

namespace TechhuddleWarehouse.Models
{
    public class WarehouseEntry
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual ProductRecord Product{ get; set; }

    }
}
