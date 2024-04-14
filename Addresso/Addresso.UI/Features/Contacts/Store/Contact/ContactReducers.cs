using Addresso.UI.Domain.Entities;
using Fluxor;

namespace Addresso.UI.Features.Contacts.Store;

public static class ContactReducers
{
    #region FetchCurrentContactData

    [ReducerMethod]
    public static ContactState ReduceFetchCurrentContactData(ContactState state, ContactActions.FetchCurrentContactData action)
    {
        return new ContactState(
            isLoading: true,
            currentContact: new Contact()
        );
    }
    
    [ReducerMethod]
    public static ContactState ReduceFetchCurrentContactDataSuccess(ContactState state, ContactActions.FetchCurrentContactDataSuccess action)
    {
        return new ContactState(
            isLoading: false,
            currentContact: action.Result
        );
    }

    #endregion
    
    #region DeleteCurrentContactData

    [ReducerMethod]
    public static ContactState ReduceDeleteCurrentContactDataSuccess(ContactState state,
        ContactActions.DeleteCurrentContactDataSuccess action)
    {
        return new ContactState(isLoading: false, currentContact: null);
    }
    
    #endregion
}