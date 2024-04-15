using Addresso.UI.Domain.Entities;

namespace Addresso.Api.Repositories;

public interface IContactRepository
{
    public void Add(Contact contact);
    public Contact GetById(int id);
    public List<Contact> Get();
    public bool Exists(Contact contact);
    public bool Exists(int id);
    public void Update(Contact contact);
    public Contact Delete(int id);
}