﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuanVinhShop.Web.Models
{
    public class TagViewModel
    {

        public string TagID { set; get; }

 
        public string Name { set; get; }

        public string Type { set; get; }

        public virtual IEnumerable<PostTagViewModel> PostTags { set; get; }
        public virtual IEnumerable<ProductTagViewModel> ProductTags { set; get; }
    }
}