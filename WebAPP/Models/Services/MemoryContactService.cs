namespace WebApp.Models.Services;

public class MemoryContactService: IContactService
{
    private Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1,
            new ContactModel()
            {
                Id = 1,
                Category = Category.Business,
                FirstName = "Adamo",
                LastName = "Kus",
                Email = "adamkus@wsei.edu.pl",
                PhoneNumber = "333 134 003",
                BirthDate = new DateOnly(2003, 10, 10)
            }
        },
        {
            2,
            new ContactModel()
            {
                Id = 2,
                Category = Category.Family,
                FirstName = "Michal",
                LastName = "Glus",
                Email = "michalglus@wsei.edu.pl",
                PhoneNumber = "885 267 388",
                BirthDate = new DateOnly(2005, 3, 22)
            }
        },
        {
            3,
            new ContactModel()
            {
                Id = 3,
                Category = Category.Friend,
                FirstName = "Kamil",
                LastName = "Zdun",
                Email = "kamilzdun@wsei.edu.pl",
                PhoneNumber = "578 399 100",
                BirthDate = new DateOnly(2000, 1, 5)
            }
        }
    };

    private int _index = 3;
    
    public void Add(ContactModel model)
    {
        model.Id = ++_index;
        _contacts.Add(model.Id, model);
    }

    public void Update(ContactModel model)
    {
        if (_contacts.ContainsKey(model.Id))
        {
            _contacts[model.Id] = model;
        }
    }

    public void Delete(int id)
    {
        _contacts.Remove(id);
    }

    public List<ContactModel> GetAll()
    {
        return _contacts.Values.ToList();
    }

    public ContactModel? GetById(int id)
    {
        return _contacts[id];
    }
}