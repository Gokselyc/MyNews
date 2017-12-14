using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace MyNews.Core.Models
{
    public class News
    {


        public int Id { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        public string Description{ get; set; }

        [StringLength(200)]
        public string Photo { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsPublished { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(200)]
        public string  CreatedBy { get; set; }

        public DateTime UpdateDate { get; set; }

        [StringLength(200)]
        public string UpdatedBy { get; set; }


        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
