using EventManagerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly EventSetDbContext _context;

        public SupplierController(EventSetDbContext context)
        {
            _context = context;            
        }

        [HttpGet("Suppliers")]
        public async Task<ActionResult> GetSuppliers()
        {
            var suppliers = await _context.Suppliers
                .Include(s => s.SupplierCategory).Select(x => new
                {
                    x.SupplierName,
                    x.SupplierCategory.SupplierCategoryName,
                    x.Link,
                    x.Location
                })
                .ToListAsync();

            if (suppliers == null || suppliers.Count == 0)
            {
                return NotFound("No suppliers found.");
            }

            return Ok(suppliers);
        }

        // API: Get all Supplier Categories
        [HttpGet("SpCategories")]
        public async Task<ActionResult> GetSupplierCategories()
        {
            var supplierCategories = await _context.SupplierCategories.Select(x => new
            {
                x.SupplierCategoryId,
                x.SupplierCategoryName
            }).ToListAsync();

            if (supplierCategories == null || supplierCategories.Count == 0)
            {
                return NotFound("No supplier categories found.");
            }

            return Ok(supplierCategories);
        }
    }
}
