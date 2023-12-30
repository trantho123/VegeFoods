namespace VEGEFOODS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            order_details = new HashSet<order_details>();
        }

        public int id { get; set; }

        public int? user_id { get; set; }

        public double? total_prices { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_date { get; set; }
        public string receiver { get; set; }
        public string receiverPhone { get; set; }
        public string address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_details> order_details { get; set; }

        public virtual user user { get; set; }
    }
}
