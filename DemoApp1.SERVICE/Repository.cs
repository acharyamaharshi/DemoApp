using DemoApp1.ENTITY;
using DemoApp1.INTERFACE;
using Microsoft.EntityFrameworkCore;

namespace DemoApp1.SERVICE
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        // This Class is  Providing Common Db Methods those who can be used by all Entity Claasses
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // Common Methods Are Defined Here..

        public bool Add(TEntity entity)
        {
            var Res = false;
            try
            {
                _dbContext.Set<TEntity>().Add(entity);
                Res = true;
            }
            catch (Exception ex)
            {

            }
            return Res;

        }

        public bool Update(TEntity entity)
        {
            var Res = false;
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                Res = true;
            }
            catch (Exception ex)
            {

            }

            return Res;
        }

        public bool Delete(TEntity entity)
        {
            var Res = false;
            try
            {
                _dbContext.Set<TEntity>().Remove(entity);
                Res = true;
            }
            catch (Exception ex)
            {

            }
            return Res;

        }

        public TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }
    }
}
