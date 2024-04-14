using Addresso.UI.Domain.Entities;

namespace Addresso.UI.Features.Contacts.Store;

public class ContactActions
{
    #region FetchCurrentContactData
    
    public record FetchCurrentContactData(int Id);
    public record FetchCurrentContactDataSuccess(Contact Result);
    
    #endregion

    #region SaveNewContactData 
    
    public record SaveNewContactData(Contact Contact);
    public record SaveNewContactDataSuccess();
    
    #endregion
    
    #region SaveExistingContactData

    public record SaveExistingContactData(Contact Contact);
    public record SaveExistingContactDataSuccess();
    
    #endregion
    
    #region DeleteCurrentContactData

    public record DeleteCurrentContactData(int Id);
    public record DeleteCurrentContactDataSuccess();
    
    #endregion
}