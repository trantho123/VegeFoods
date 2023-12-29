namespace VEGEFOODS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order_details
    {
        public int id { get; set; }

        public int? product_id { get; set; }

        public int? quantity { get; set; }

        public double? price { get; set; }

        public int? order_id { get; set; }

        public virtual order order { get; set; }

        public virtual product product { get; set; }

    }
}
