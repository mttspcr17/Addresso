using Addresso.UI.Domain.Entities;
using Fluxor;

namespace Addresso.UI.Features.Contacts.Store;

[FeatureState]
public class ContactsState
{
    public bool IsLoading { get; set; }
    public List<Contact>? Contacts { get; set; }
    
    public ContactsState() { }
    public ContactsState(bool isLoading, List<Contact>? contacts)
    {
        IsLoading = isLoading;
        Contacts = contacts;
    }
}