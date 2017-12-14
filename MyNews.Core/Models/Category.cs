using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MyNews.Core.Models
{
    public class Category
    {


        public int Id { get; set; }

        [StringLength(200)]
        public  string Name { get; set; }

        public DateTime CreateDate { get; set; }
        [StringLength(200)]
        public string  CreatedBy { get; set; }

        public DateTime UpdateDate { get; set; }

        [StringLength(200)]
        public string UpdatedBy { get; set; }

        public virtual ICollection<News> News { get; set; }

    }
}
