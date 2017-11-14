using System;
using System.ComponentModel.DataAnnotations;

namespace TuanVinhShop.Model.Abstract
{
    public interface IAuditable
    {
        DateTime? CreatedDate { set; get; }
        [StringLength(256)]
        string CreatedBy { set; get; }

        DateTime? UpdateDate { set; get; }

        [StringLength(256)]
        string UpdateBy { set; get; }

        string MetaKeywork { set; get; }

        string Descryption { set; get; }

        int? DisplayOrder { set; get; }

        [Required]
        bool Status { set; get; }
    }
}
