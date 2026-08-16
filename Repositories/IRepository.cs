namespace RestauranteAPI.Repositories
{
    public interface IRepository
    {
        public object Save(object obj);
        public object Get(long id);
        public bool Delete(long id);
        public List<object> Query();
    }
}
