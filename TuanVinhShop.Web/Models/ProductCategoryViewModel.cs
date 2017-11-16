using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuanVinhShop.Web.Models
{
    public class ProductCategoryViewModel
    {

        public int ID { set; get; }

        public string Name { set; get; }

        public string Alias { set; get; }

        public int? ParentID { get; set; }

        public string Images { set; get; }

        public bool? HomeFlag { set; get; }

        public virtual IEnumerable<ProductViewModel> Products { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdateDate { set; get; }

        public string UpdateBy { set; get; }

        public string MetaKeywork { set; get; }

        public string Descryption { set; get; }

        public int? DisplayOrder { set; get; }


        public bool Status { set; get; }
    }
}