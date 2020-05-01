using System;

namespace IMMRequest.Domain
{
  public class Admin : User
  {
    public string Password { get; set; }
    public Guid Token { get; set; }
  }
}
