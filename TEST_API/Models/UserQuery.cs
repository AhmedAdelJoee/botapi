namespace TEST_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserQuery
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string Count { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string MessengerUserID { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string mapurl { get; set; }

        [StringLength(100)]
        public string longitude { get; set; }

        [StringLength(100)]
        public string latitude { get; set; }
    }
}
