using System;

namespace IMMREQUEST.Domain
{

    public enum Status { Creada, EnRevision, Aceptada, Denegada, Finalizada }

    public class Request
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string Details { get; set; } /* less 2000 */ 
        public Citizen Citizen { get; set; }
        public RequestType Type { get; set; }

        public Request()
        {

        }
    }
}