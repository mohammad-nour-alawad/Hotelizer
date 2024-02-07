using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DBConnection.Repos
{
    public interface IBaseRepo<DbModel> where DbModel : class
    {
        DbModel GetById(object id);
        bool IsExist(object id);
        public IEnumerable<DbModel> GetAll(Expression<Func<DbModel, bool>> filter = null, Func<IQueryable<DbModel>, IOrderedQueryable<DbModel>> orderBy = null, string includeProperties = "");
        public IEnumerable<DbModel> GetPage(int skip,int take,Expression<Func<DbModel, bool>> filter = null, Func<IQueryable<DbModel>, IOrderedQueryable<DbModel>> orderBy = null, string includeProperties = "");
        DbModel Insert(DbModel newObj);
        bool Any(Expression<Func<DbModel, bool>> query);
        bool Any();
        DbModel Update(DbModel newObj);
        void DeleteAll(Expression<Func<DbModel, bool>> condition = null);
        void Delete(object id);
        void Delete(DbModel obj);
        int GetTotalCounts(Expression<Func<DbModel, bool>> condition = null);
    }
}
