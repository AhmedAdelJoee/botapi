namespace TEST_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AxaProvider
    {
        [Key]
        [Column(Order = 0)]
        public string Provider { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Type { get; set; }

        [Key]
        [Column(Order = 2)]
        public string NetworkName { get; set; }

        public string Governorate { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string EmailAddress { get; set; }

        public string ServicesProvided { get; set; }

        public string Speciality { get; set; }

        public string التخصص { get; set; }

        public string الخدمات_المقدمة { get; set; }

        public string WebSite { get; set; }

        public string LocationOnMap { get; set; }

        public string Latitude { get; set; }

        public string Longtude { get; set; }

        public string فاكس { get; set; }

        public string تليفون { get; set; }

        public string العنوان { get; set; }

        public string المدينة { get; set; }

        public string المحافظة { get; set; }

        public string اسم_الشبكة { get; set; }

        public string النوع { get; set; }

        public string اسم_مقدم_الخدمة { get; set; }
    }
}
