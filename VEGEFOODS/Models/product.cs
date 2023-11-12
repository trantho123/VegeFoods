namespace VEGEFOODS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            order_details = new HashSet<order_details>();
        }

        public int id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        public int? quantity { get; set; }

        [StringLength(100)]
        public string image { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public double? price { get; set; }

        public int? category_id { get; set; }

        public virtual category category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_details> order_details { get; set; }
    }
}
