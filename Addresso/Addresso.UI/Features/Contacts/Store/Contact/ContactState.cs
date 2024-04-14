using Addresso.UI.Domain.Entities;
using Fluxor;

namespace Addresso.UI.Features.Contacts.Store;

[FeatureState]
public class ContactState
{
    public bool IsLoading { get; set; }
    public Contact? CurrentContact { get; set; }
    
    public ContactState(){}

    public ContactState(bool isLoading, Contact? currentContact)
    {
        IsLoading = isLoading;
        CurrentContact = currentContact;
    }
}