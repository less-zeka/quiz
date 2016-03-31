using System.Collections.Generic;

namespace Quiz.Models
{
    public class User
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public HashSet<string> ConnectionIds { get; set; }

        public bool IsReadyForNextQuestion { get; set; }
    }
}