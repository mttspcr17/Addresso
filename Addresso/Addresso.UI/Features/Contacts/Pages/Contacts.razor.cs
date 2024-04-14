using Addresso.UI.Api.Services;
using Addresso.UI.Domain.Entities;
using Addresso.UI.Features.Contacts.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace Addresso.UI.Features.Contacts.Pages;

public partial class Contacts : FluxorComponent
{
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Dispatcher!.Dispatch(new ContactsActions.FetchContactsData());
    }

    [Inject]
    private IContactService? ContactService { get; set; }
    [Inject]
    private IState<ContactsState>? ContactsState { get; set; }
    [Inject]
    private IDispatcher? Dispatcher { get; set; }

    private List<Contact>? _contacts;

    public string? SearchText { get; set; }
    public bool IsLoading { get; set; } = true;
    public List<Contact>? AvailableContacts =>
        !string.IsNullOrEmpty(SearchText)
            ? ContactsState?.Value.Contacts?
                .Where(x =>
                    x.FirstName!.Contains(SearchText) ||
                    x.LastName!.Contains(SearchText) ||
                    x.Email!.Contains(SearchText) ||
                    x.Phone!.Contains(SearchText)
                ).ToList()
            : ContactsState?.Value.Contacts;
}