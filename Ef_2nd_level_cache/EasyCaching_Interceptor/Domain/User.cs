namespace EasyCaching_Interceptor.Domain;

public class User : IEntity
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }    
}