using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CutNFood.Data.Data;
using System.Web.Mvc;

namespace CutNFood.Web.Models
{
    public class ProductViewModel
    {
        
        public tbl_products product { get; set; }
        public List<SelectListItem> categories { get; set; }

        public HttpPostedFileBase postedFile { get; set; }

    }

}