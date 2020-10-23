using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stocktake;
using stocktake.Entity;
using stocktake.BLL;
using stocktake.DAL;

namespace stocktake.BLL
{
   partial class BarCodeInfoBll:BaseBll<BarCodeInfo>
    {
        public override BaseDal<BarCodeInfo> GetDal()
        {
            return new BarCodeInfoDal();
        }
       
    }
   
}
