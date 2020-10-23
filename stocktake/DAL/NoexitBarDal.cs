using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stocktake.Entity;
using System.Linq.Expressions;

namespace stocktake.DAL
{
    partial class NoexitBarDal : BaseDal<NoexitsBar>
    {
        public override Expression<Func<NoexitsBar, bool>> GetByIdKey(int id)
        {
            return u => u.ID == id;
        }


        public override Expression<Func<NoexitsBar, int>> GetKey()
        {
            return u => u.ID;
        }
    }
}
