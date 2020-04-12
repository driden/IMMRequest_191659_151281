using System;
using System.Collections.Generic;
using System.Text;

namespace IMMRequest.Domain
{
    public interface IState
    {
        void Created();
        void InReview();
        void Accepted();
        void Denied();
        void Done();
    }
}
