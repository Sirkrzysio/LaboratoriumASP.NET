using WebAPP.Models;

namespace WebApp.Models.Services;

public class EFContantService : IContactService
{
    private readonly AppDbContext _context;

    public EFContantService(AppDbContext context)
    {
        _context = context;
    }

    public void Add(ContactModel model)
    {
        _context.Contacts.Add(ContactMapper.ToEntity(model));
        _context.SaveChanges();
    }

    public void Update(ContactModel model)
    {
        _context.Contacts.Update(ContactMapper.ToEntity(model));
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        _context.Contacts.Remove(new ContactEntity(){Id = id});
        _context.SaveChanges();
    }

    public List<ContactModel> GetAll()
    {
        return _context.Contacts.Select(ContactMapper.FromEntity).ToList();

    }

    public ContactModel? GetById(int id)
    {
        return ContactMapper.FromEntity(_context.Contacts.Find(id));
    }
}