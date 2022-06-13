using System;

namespace MadHotSpot.Model.Commons
{
    public class ResultJson
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Value { get; set; }
        public Guid Id { get; set; }
    }
}
