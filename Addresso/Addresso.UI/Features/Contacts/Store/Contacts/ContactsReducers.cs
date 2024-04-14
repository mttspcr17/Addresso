using Fluxor;

namespace Addresso.UI.Features.Contacts.Store;

public static class ContactsReducers
{
    #region FetchContactsData
    
    [ReducerMethod]
    public static ContactsState ReduceFetchContactDataAction(ContactsState state, ContactsActions.FetchContactsData action)
    {
        return new ContactsState(
            isLoading: true,
            contacts: null
        );
    }

    [ReducerMethod]
    public static ContactsState ReduceFetchContactDataSuccessAction(ContactsState state, ContactsActions.FetchContactsDataSuccess action)
    {
        return new ContactsState(
            isLoading: false,
            contacts: action.Result
        );
    }
    
    #endregion
}