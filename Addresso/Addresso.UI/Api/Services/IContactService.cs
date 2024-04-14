using Addresso.UI.Domain.Common;
using Addresso.UI.Domain.Entities;

namespace Addresso.UI.Api.Services;

public interface IContactService
{
    // Queries
    public Task<ServiceQueryResult<Contact?>> FetchSingle(int Id);
    public Task<ServiceQueryResult<List<Contact>?>> FetchAll();

    // Commands
    public Task<ServiceActionResult> Add(Contact contact);
    public Task<ServiceActionResult> Update(Contact contact);
    public Task<ServiceActionResult> Delete(int Id);
}