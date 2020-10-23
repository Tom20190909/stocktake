using stocktake;
using stocktake.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using stocktake.BLL;

namespace stocktake.DAL
{
    partial class TakeCodeDal : BaseDal<TakeCode>
    {
        public override Expression<Func<TakeCode, bool>> GetByIdKey(int id)
        {
            return u => u.ID == id;
        }

       
        public override Expression<Func<TakeCode, int>> GetKey()
        {
            return u => u.ID;
        }
    }
    
}
