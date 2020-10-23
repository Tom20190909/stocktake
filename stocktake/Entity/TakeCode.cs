using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stocktake.Entity
{
   public  class TakeCode
    {
        [Key]
        public int    ID { get; set; }
        public string CodeBard { get; set; }
        public string ItemCode { get; set; }

        public string ItemName { get; set; }
        public string BrandCode { get; set; }
        public int TV1 { get; set; }
        public int TV2 { get; set; }
        public int TV3 { get; set; }
        public string BrandName { get; set; }
        public string TakeArea { get; set; }

        public DateTime TakeTime { get; set; }
    }
}
