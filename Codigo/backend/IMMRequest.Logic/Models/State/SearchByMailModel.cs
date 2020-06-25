namespace IMMRequest.Logic.Models.State
{
    using System;
    using System.Collections.Generic;

    public class SearchByMailModel
    {
        public string Mail { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}