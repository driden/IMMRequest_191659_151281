using IMMRequest.Domain.States;
using IMMRequest.Domain.Exceptions;
using System;

namespace IMMRequest.Domain
{
  public class Request
  {
    public int Id { get; set; }
    public State Status { get; set; }
    public string Details { get; set; } // set method} } /* less 2000 */
    public Citizen Citizen { get; set; }
    public Type Type { get; set; }

    // We can leave these 2 properties, so it's easier to handle
    public Area Area { get; set; }
    public Topic Topic { get; set; }

    public Request()
    {
        Status = new CreatedState(this);
    }
  }
}
