using Addresso.UI.Domain.Common;
using Addresso.UI.Domain.Entities;

namespace Addresso.UI.Features.Contacts.Store;

public class ContactsActions
{
    #region FetchContactsData
    
    public record FetchContactsData();

    public record FetchContactsDataSuccess(List<Contact> Result);
    
    #endregion
}