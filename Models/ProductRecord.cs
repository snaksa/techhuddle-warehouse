using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TechhuddleWarehouse.Models
{
    public class ProductRecord
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public virtual ICollection<WarehouseEntry> WarehouseEntries { get; set; }

        [JsonIgnore]
        public virtual ICollection<CapacityRecord> CapacityRecords { get; set; }
    }
}
