using Addresso.UI.Api.Services;
using Addresso.UI.Domain.Common;
using Addresso.UI.Domain.Entities;
using Fluxor;

namespace Addresso.UI.Features.Contacts.Store;

public class ContactsEffects(IContactService contactService)
{
    #region FetchContactsData
    
    [EffectMethod]
    public async Task HandleFetchContactDataAction(ContactsActions.FetchContactsData action, IDispatcher dispatcher)
    {
        ServiceQueryResult<List<Contact>?> contactServiceCall = await contactService.FetchAll();

        if (contactServiceCall.Success)
        {
            dispatcher.Dispatch(new ContactsActions.FetchContactsDataSuccess(contactServiceCall.Result));
        }
    }
    
    #endregion
}