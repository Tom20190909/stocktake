using stocktake.DAL;
using stocktake.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stocktake.BLL
{
    public partial class NoexitBarBll : BaseBll<NoexitsBar>
    {
        public override BaseDal<NoexitsBar> GetDal()
        {
            return new NoexitBarDal();
        }
    }
}
