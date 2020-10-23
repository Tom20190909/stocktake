using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stocktake.Entity
{
  public partial  class BarCodeInfo
    {
        
        public int ID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }

        public string CodeBard { get; set; }
        public string BrandCode { get; set; }

        public string BrandName { get; set; }
       
    }
}
