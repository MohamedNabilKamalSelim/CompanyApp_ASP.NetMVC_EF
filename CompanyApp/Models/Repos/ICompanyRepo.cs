namespace CompanyApp.Models.Repos
{
    public interface ICompanyRepo<TEntity>
    {
        void Add(TEntity entity);
        IList<TEntity> GetAll();

        TEntity GetById(int id);

        void Delete(int id);

        void Update(TEntity entity);

        TEntity? CheckForExistingEntity(TEntity entity);

        IList<TEntity> Search(string term);
    }
}

