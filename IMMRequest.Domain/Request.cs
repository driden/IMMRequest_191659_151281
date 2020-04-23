using IMMRequest.Domain.States;
using System;
using System.Collections.Generic;

namespace IMMRequest.Domain
{
  public class Request
  {
    public int Id { get; set; }
    public virtual State Status { get; set; }
    public string Details { get; set; } /* less 2000 */
    public virtual Citizen Citizen { get; set; }
    public virtual Type Type { get; set; }
        public virtual List<RequestField> FieldValues { get; set; } = new List<RequestField>();

    public Request()
    {
        Status = new CreatedState(this);
    }
  }
}
