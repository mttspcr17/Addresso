using Addresso.UI.Api.Services;
using Addresso.Shared.Domain.Common;
using Addresso.UI.Domain.Entities;
using Fluxor;
using Fluxor.Blazor.Web.Middlewares.Routing;

namespace Addresso.UI.Features.Contacts.Store;

public class ContactEffects(IContactService contactService)
{

    #region FetchContactDataAction

    [EffectMethod]
    public async Task HandleFetchContactDataAction(ContactActions.FetchCurrentContactData action, IDispatcher dispatcher)
    {
        if (action.Id < 1)
        {
            dispatcher.Dispatch(new ContactActions.FetchCurrentContactDataSuccess(new Contact()));
            return;
        }

        ServiceQueryResult<Contact> contactServiceCall = await contactService.FetchSingle(action.Id);

        if (contactServiceCall.Success)
        {
            dispatcher.Dispatch(new ContactActions.FetchCurrentContactDataSuccess(contactServiceCall.Result));
        }
    }

    #endregion

    #region SaveNewContactData

    [EffectMethod]
    public async Task HandleSaveNewContactData(ContactActions.SaveNewContactData action, IDispatcher dispatcher)
    {
        ServiceActionResult contactServiceCall = await contactService.Add(action.Contact);

        if (contactServiceCall.Success)
        {
            dispatcher.Dispatch(new ContactActions.SaveNewContactDataSuccess());
        }
    }

    [EffectMethod]
    public async Task HandleSaveNewContactDataSuccess(ContactActions.SaveNewContactDataSuccess action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new GoAction("/contacts"));
    }

    #endregion

    #region SaveExistingContactData

    [EffectMethod]
    public async Task HandleSaveExistingContactData(ContactActions.SaveExistingContactData action, IDispatcher dispatcher)
    {
        ServiceActionResult contactServiceCall = await contactService.Update(action.Contact);

        if (contactServiceCall.Success)
        {
            dispatcher.Dispatch(new ContactActions.SaveExistingContactDataSuccess());
        }
    }

    [EffectMethod]
    public async Task HandleSaveExistingContactDataSuccess(ContactActions.SaveExistingContactDataSuccess action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new GoAction("/contacts"));
    }

    #endregion
    
    #region DeleteCurrentContactData

    [EffectMethod]
    public async Task HandleDeleteCurrentContactData(ContactActions.DeleteCurrentContactData action,
        IDispatcher dispatcher)
    {
        ServiceActionResult contactServiceCall = await contactService.Delete(action.Id);

        if (contactServiceCall.Success)
        {
            dispatcher.Dispatch(new ContactActions.DeleteCurrentContactDataSuccess());
        }
    }

    [EffectMethod]
    public async Task HandleDeleteCurrentContactDataSuccess(ContactActions.DeleteCurrentContactDataSuccess action,
        IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new GoAction("/contacts"));
    }
    
    #endregion
}