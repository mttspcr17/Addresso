using System.Net;
using System.Text;
using System.Text.Json;
using Addresso.Shared.Domain.Common;
using Addresso.UI.Domain.Entities;

namespace Addresso.UI.Api.Services;

public class ContactService(HttpClient _HttpClient) : IContactService
{
    public List<Contact>? Contacts { get; set; }

    public async Task<ServiceActionResult> Add(Contact contact)
    {
        var content = new StringContent(JsonSerializer.Serialize(contact), Encoding.UTF8, "application/json");
        var response = await _HttpClient.PostAsync("contacts", content);

        if (response.IsSuccessStatusCode)
        {
            return new ServiceActionResult(true, HttpStatusCode.OK, "");
        }
        else
        {
            return new ServiceActionResult(false, response.StatusCode, "");
        }
    }

    public async Task<ServiceQueryResult<List<Contact>?>> FetchAll()
    {
        using HttpResponseMessage response = await _HttpClient.GetAsync("contacts");
        List<Contact>? contacts = null;

        if(response.IsSuccessStatusCode)
            contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content.ReadAsStream());

        return new ServiceQueryResult<List<Contact>?>(response.IsSuccessStatusCode, response.StatusCode, null, contacts);
    }

    public async Task<ServiceQueryResult<Contact?>> FetchSingle(int id)
    {
        using HttpResponseMessage response = await _HttpClient.GetAsync($"contacts/{id}");
        Contact contact = null;

        if(response.IsSuccessStatusCode)
            contact = JsonSerializer.Deserialize<Contact>(response.Content.ReadAsStream());

        return new ServiceQueryResult<Contact>(response.IsSuccessStatusCode, response.StatusCode, null, contact);
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

    public async Task<ServiceActionResult> Delete(int id)
    {
        using HttpResponseMessage response = await _HttpClient.DeleteAsync($"contacts/{id}");
        Contact contact = null;

        if(response.IsSuccessStatusCode)
            contact = JsonSerializer.Deserialize<Contact>(response.Content.ReadAsStream());

        return new ServiceActionResult(true, HttpStatusCode.OK, "");
    }

    internal const string SAMPLE_DATA_PATH = "sample-data/contacts.json";
}