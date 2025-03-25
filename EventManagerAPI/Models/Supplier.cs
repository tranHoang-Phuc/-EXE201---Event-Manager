using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagerAPI.Models
{
    public class Supplier
    {
        [Key]
        public Guid SupplierId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(255)]
        public string SupplierName { get; set; } // Tên nhà cung cấp

        [Required]
        [StringLength(5000)]
        public string Link { get; set; } // URL của nhà cung cấp

        public string Location { get; set; } // Địa chỉ của nhà cung cấp

        public Guid SupplierCategoryId { get; set; } // Khóa ngoại tham chiếu đến SupplierCategory
        public SupplierCategory SupplierCategory { get; set; } // Mối quan hệ với SupplierCategory
    }
}
