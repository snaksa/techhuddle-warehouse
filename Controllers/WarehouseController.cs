using System;
using Microsoft.AspNetCore.Mvc;
using TechhuddleWarehouse.Exceptions;
using TechhuddleWarehouse.Exceptions.Messages;
using TechhuddleWarehouse.Models;
using TechhuddleWarehouse.Repository;
using TechhuddleWarehouse.ViewModels;

namespace TechhuddleWarehouse.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WarehouseController : ControllerBase
    {
        private WarehouseRepository repository;
        public WarehouseController(AppDbContext dbContext)
        {
            this.repository = new WarehouseRepository(dbContext);
        }

        [HttpGet]
        public OkObjectResult GetProducts()
        {
            return new OkObjectResult(this.repository.GetProductRecords((ProductRecord r) => r.Quantity > 0));
        }

        [HttpPost]
        public ObjectResult SetProductCapacity(CapacityViewModel vm)
        {
            try
            {
                this.repository.SetCapacityRecord(vm.ProductId, vm.Capacity);
            }
            catch(ProductNotFoundException)
            {
                return new BadRequestObjectResult(new ProductNotFoundMessage());
            }
            catch (NotPositiveQuantityException)
            {
                return new BadRequestObjectResult(new NotPositiveQuantityMessage());
            }
            catch (QuantityTooLowException)
            {
                return new BadRequestObjectResult(new QuantityTooLowMessage());
            }

            return new OkObjectResult(this.repository.GetProductRecords((ProductRecord r) => r.Quantity > 0));
        }

        [HttpPost]
        public ObjectResult ReceiveProduct(ReceiveViewModel vm)
        {
            if(vm.Quantity < 0)
                return new BadRequestObjectResult(new NotPositiveQuantityMessage());

            try
            {
                this.repository.SetProductRecord(vm.ProductId, vm.Quantity);
            }
            catch (ProductNotFoundException)
            {
                return new BadRequestObjectResult(new ProductNotFoundMessage());
            }
            catch (QuantityTooHighException)
            {
                return new BadRequestObjectResult(new QuantityTooHighMessage());
            }

            return new OkObjectResult(this.repository.GetProductRecords((ProductRecord r) => r.Quantity > 0));
        }

        [HttpPost]
        public ObjectResult DispatchProduct(DispatchViewModel vm)
        {
            if (vm.Quantity < 0)
                return new BadRequestObjectResult(new NotPositiveQuantityMessage());

            try
            {
                this.repository.SetProductRecord(vm.ProductId, vm.Quantity * -1);
            }
            catch (ProductNotFoundException)
            {
                return new BadRequestObjectResult(new ProductNotFoundMessage());
            }
            catch (QuantityTooHighException)
            {
                return new BadRequestObjectResult(new QuantityTooHighMessage());
            }

            return new OkObjectResult(this.repository.GetProductRecords((ProductRecord r) => r.Quantity > 0));
        }
    }
}
