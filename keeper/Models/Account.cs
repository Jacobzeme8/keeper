namespace keeper.Models;

public class Account : Profile
{ 
  public string Email { get; set; }
  
}

public class Profile {
  public string Picture { get; set; }
  public string coverImg { get; set; }
  public string Name { get; set; }
  public string Id { get; set; }
}
