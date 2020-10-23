using stocktake.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace stocktake.DAL
{
    class BarCodeInfoDal : BaseDal<BarCodeInfo>
    {
        public override Expression<Func<BarCodeInfo, bool>> GetByIdKey(int id)
        {
            return u => u.ID == id;
        }


        public override Expression<Func<BarCodeInfo, int>> GetKey()
        {
            return u => u.ID;
        }
    }
}
