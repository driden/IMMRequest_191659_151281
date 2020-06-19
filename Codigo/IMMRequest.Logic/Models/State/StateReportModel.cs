namespace IMMRequest.Logic.Models.State
{
    using System.Collections.Generic;

    public class StateReportModel
    {
        public string StateName { get; set; }
        
        public int Quantity { get; set; }
        
        public IEnumerable<int> Ids { get; set; }
    }
}