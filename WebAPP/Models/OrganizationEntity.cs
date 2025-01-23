using WebApp.Models.Services;

namespace WebAPP.Models;

public class OrganizationEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NIP { get; set; }
    public string Regon { get; set; }
    public Address Address { get; set; }
    public ISet<ContactEntity> Contacts { get; set; }
}

public class Address
{
    public string City { get; set; }
    public string Street { get; set; }
}