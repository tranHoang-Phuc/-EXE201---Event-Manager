using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagerAPI.Models
{
    public class SupplierCategory
    {
        [Key]
        public Guid SupplierCategoryId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(255)]
        public string SupplierCategoryName { get; set; } // Tên thể loại nhà cung cấp

        public ICollection<Supplier> Suppliers { get; set; } // Quan hệ 1-N với Supplier
    }
}
