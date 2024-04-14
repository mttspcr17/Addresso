using System.Net;
using System.Text.Json;
using Addresso.UI.Domain.Common;
using Addresso.UI.Domain.Entities;

namespace Addresso.UI.Api.Services;

public class ContactService(HttpClient _HttpClient) : IContactService
{
    public List<Contact>? Contacts { get; set; }

    public async Task<ServiceActionResult> Add(Contact contact)
    {
        if (Contacts == null)
        {
            ServiceQueryResult<List<Contact>?> fetchContacts = await FetchAll();

            if (fetchContacts.Success)
                Contacts = fetchContacts.Result;
            else
                return new ServiceActionResult(false, fetchContacts.StatusCode, "Failed to fetch contacts");
        }

        if(Contacts == null) Contacts = new List<Contact>();

        if(Contacts.Count > 0 && !Contacts.Contains(contact)){
            Contacts.Add(contact);
        }

        return new ServiceActionResult(true, HttpStatusCode.OK, "");
    }

    public async Task<ServiceQueryResult<List<Contact>?>> FetchAll()
    {
        if (Contacts != null) 
            return new ServiceQueryResult<List<Contact>?>(true, HttpStatusCode.OK, null, Contacts);

        using HttpResponseMessage response = await _HttpClient.GetAsync(SAMPLE_DATA_PATH);
        List<Contact>? contacts = null;

        if(response.IsSuccessStatusCode)
            contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content.ReadAsStream());

        return new ServiceQueryResult<List<Contact>?>(response.IsSuccessStatusCode, response.StatusCode, null, contacts);
    }

    public async Task<ServiceQueryResult<Contact?>> FetchSingle(int Id)
    {
        if (Contacts == null)
        {
            using HttpResponseMessage response = await _HttpClient.GetAsync(SAMPLE_DATA_PATH);
        
            if(response.IsSuccessStatusCode)
                Contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content.ReadAsStream());
            else
                return new ServiceQueryResult<Contact?>(response.IsSuccessStatusCode, response.StatusCode, null, null);
        }

        return new ServiceQueryResult<Contact?>(true, HttpStatusCode.OK, null, Contacts![Id - 1]);
    }

    public async Task<ServiceActionResult> Update(Contact contact)
    {
        ServiceQueryResult<List<Contact>?> fetchContacts = await FetchAll();
        List<Contact>? contacts = null;

        if(fetchContacts.Success)
            contacts = fetchContacts.Result;
        else
            return new ServiceActionResult(false, fetchContacts.StatusCode, "Failed to fetch contacts");

        if(contacts == null) 
            return new ServiceActionResult(false, fetchContacts.StatusCode, "Failed to fetch contacts");

        if(contacts.Count > 0 && contacts.Contains(contact)){
            Contact contactToUpdate = contacts
                .FirstOrDefault(x=>x.Email == contact.Email);

            contactToUpdate.FirstName = contact.FirstName;
            contactToUpdate.LastName = contact.LastName;
            contactToUpdate.Email = contact.Email;
            contactToUpdate.Phone = contact.Phone;
        }

        return new ServiceActionResult(true, HttpStatusCode.OK, "");
    }

    public async Task<ServiceActionResult> Delete(int Id)
    {
        Contacts.Remove(Contacts[Id-1]);

        return new ServiceActionResult(true, HttpStatusCode.OK, "");
    }

    internal const string SAMPLE_DATA_PATH = "sample-data/contacts.json";
}