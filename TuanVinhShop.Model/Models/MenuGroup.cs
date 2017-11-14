﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuanVinhShop.Model.Models
{
    [Table("MenuGroups")]
    public class MenuGroup
    {
        [Key]
        public int ID { set; get; }

        [Required]
        public string Name { set; get; }

        public virtual IEnumerable<Menu> Menus { set; get; }
    }
}
