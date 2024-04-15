using System.Text.Json;
using Addresso.Api.Repositories;
using Addresso.UI.Domain.Entities;

namespace Addresso.Persistence.Repositories;

public class ContactRepository : IContactRepository
{
    public void Add(Contact contact)
    {
        if (Contacts == null)
        {
            LoadContactsFromFile();
        }
        
        contact.Id = GetNextId();
        
        Contacts!.Add(contact);

        UpdateContactsFile();
    }

    public Contact GetById(int id)
    {
        if (Contacts == null)
        {
            LoadContactsFromFile();
        }
        
        return Contacts.FirstOrDefault(x=>x.Id == id);
    }

    public List<Contact> Get()
    {
        if (Contacts == null)
        {
            LoadContactsFromFile();
        }
        
        return Contacts;  
    }

    public bool Exists(Contact contact)
    {
        if (Contacts == null)
        {
            LoadContactsFromFile();
        }

        return Contacts.Any(x =>
            x.FirstName == contact.FirstName &&
            x.LastName == contact.LastName &&
            x.Email == contact.Email &&
            x.Phone == contact.Phone);
    }
    
    public bool Exists(int id)
    {
        if (Contacts == null)
        {
            LoadContactsFromFile();
        }

        return Contacts.Any(x =>
            x.Id == id);
    }

    public void Update(Contact contact)
    {
        if (Contacts == null)
        {
            LoadContactsFromFile();
        }

        Contact contactToUpdate = Contacts.FirstOrDefault(x => x.Id == contact.Id);

        contactToUpdate.FirstName = contact.FirstName;
        contactToUpdate.LastName = contact.LastName;
        contactToUpdate.Email = contact.Email;
        contactToUpdate.Phone = contact.Phone;
        
        UpdateContactsFile();
    }

    public Contact Delete(int id)
    {
        if (Contacts == null)
        {
            LoadContactsFromFile();
        }

        Contact contactToRemove = Contacts.FirstOrDefault(x => x.Id == id);

        Contacts.Remove(contactToRemove);
        
        UpdateContactsFile();

        return contactToRemove;
    }

    private int GetNextId()
    {
        if (Contacts == null || Contacts.Count == 0)
        {
            return 1;
        }

        return (Contacts.OrderByDescending(i => i.Id).First().Id + 1);
    }

    private void LoadContactsFromFile()
    {
        var localDirectory = Environment.CurrentDirectory;

        string fileName = $"{localDirectory}{SAMPLE_DATA_PATH}contacts.json";

        if (!File.Exists(fileName))
        {
            Contacts = new List<Contact>();
        }

        var json = File.ReadAllText(fileName);  
        Contacts = JsonSerializer.Deserialize<List<Contact>>(json);  
    }

    private void UpdateContactsFile()
    {
        var localDirectory = Environment.CurrentDirectory;

        string fileName = $"{localDirectory}{SAMPLE_DATA_PATH}contacts.json";

        if (!File.Exists(fileName))
        {
            Contacts = new List<Contact>();
        }

        File.WriteAllText(fileName, JsonSerializer.Serialize(Contacts));  
    }
    
    public List<Contact>? Contacts { get; set; }

    private const string SAMPLE_DATA_PATH = "\\Data\\";
}