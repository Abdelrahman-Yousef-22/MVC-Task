using Microsoft.EntityFrameworkCore;

namespace Library.DAL;


public class GenericRepo<T> : IGenericRepo<T> where T : class
{
    private readonly LibraryContext _context;

    public GenericRepo(LibraryContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public T? GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
    }
}
