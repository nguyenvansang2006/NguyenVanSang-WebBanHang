using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebBanHang.Models;

namespace WebBanHang.Context
{
    [MetadataType(typeof(UserMasterData))]
    public partial class User
    {
        //public object UserName { get; set; }
        //public string Name { get; internal set; }
        //public bool Status { get; internal set; }
        //public DateTime CreateDate { get; internal set; }
    }


    [MetadataType(typeof(ProductMasterData))]
    public partial class Product
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
        public int Status { get;  set; }
        //public int Status { get; internal set; }
    }
    public partial class Brand
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
    public partial class Category
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
        public int Status { get;  set; }
    }
    
}