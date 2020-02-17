using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TechhuddleWarehouse.Exceptions;
using TechhuddleWarehouse.Models;

namespace TechhuddleWarehouse.Repository
{
    public class WarehouseRepository
    {
        private AppDbContext context;

        public WarehouseRepository(AppDbContext ctx)
        {
            this.context = ctx;
        }

        public void SetCapacityRecord(int productId, int capacity)
        {
            if(capacity <= 0)
                throw new NotPositiveQuantityException("");

            ProductRecord product = this.GetProductRecords((ProductRecord p) => p.Id == productId).FirstOrDefault();
            if(product == null)
                throw new ProductNotFoundException("");

            if (product.Quantity > capacity)
                throw new QuantityTooLowException("");

            CapacityRecord record = this.GetCapacityRecords((CapacityRecord r) => r.ProductId == productId).FirstOrDefault();

            if (record != null)
            {
                record.Quantity = capacity;
            }
            else
            {
                CapacityRecord capacityRecord = new CapacityRecord()
                {
                    ProductId = productId,
                    Quantity = capacity
                };

                this.context.CapacityRecords.Add(capacityRecord);
            }

            this.context.SaveChanges();
        }

        public IEnumerable<CapacityRecord> GetCapacityRecords()
        {
            return this.context.CapacityRecords;
        }

        public IEnumerable<CapacityRecord> GetCapacityRecords(Func<CapacityRecord, bool> filter)
        {
            return this.context.CapacityRecords.Where(filter);
        }

        public void SetProductRecord(int productId, int quantity)
        {
            ProductRecord product = this.context.Products.Include(p => p.CapacityRecords).Where(p => p.Id == productId).FirstOrDefault();
            if (product == null)
                throw new ProductNotFoundException("");

            CapacityRecord capacity = product.CapacityRecords.FirstOrDefault();
            int fullCapacity = capacity != null ? capacity.Quantity : 0;

            if (product.Quantity + quantity < 0 || product.Quantity + quantity > fullCapacity)
            {
                throw new QuantityTooHighException("");
            }

            product.Quantity = product.Quantity + quantity;

            this.context.WarehouseEntries.Add(new WarehouseEntry()
            {
                ProductId = productId,
                Quantity = quantity
            });

            this.context.SaveChanges();
        }

        public IEnumerable<ProductRecord> GetProductRecords()
        {
            return this.context.Products;
        }
        public IEnumerable<ProductRecord> GetProductRecords(Func<ProductRecord, bool> filter)
        {
            return this.context.Products.Where(filter);
        }
    }
}
