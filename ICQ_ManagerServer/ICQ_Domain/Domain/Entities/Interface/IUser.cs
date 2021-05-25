namespace ICQ_AppDomain.Entities
{
    public interface IUser
    {
        string UserIdentifier { get; set; }
        object ConnectionSocket { get; set; }
       
    }
}