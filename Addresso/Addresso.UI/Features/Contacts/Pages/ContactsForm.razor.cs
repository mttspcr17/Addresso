
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Addresso.UI.Domain.Entities;
using Addresso.UI.Features.Contacts.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace Addresso.Features.Contacts.Pages;

public partial class ContactsForm : FluxorComponent {
    protected async override Task OnInitializedAsync()
    {
        
        dispatcher?.Dispatch(new ContactActions.FetchCurrentContactData(Id));
    }

    [Inject]
    private IDispatcher? dispatcher { get; set; }
    [Inject]
    private IState<ContactState>? ContactState { get; set; }
    [Inject]
    private NavigationManager? Navigation {get;set;}

    [Parameter]
    public int Id {get;set;}
}