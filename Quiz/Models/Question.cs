using System.Collections.Generic;

namespace Quiz.Models
{
    public class Question
    {
        public string QuestionText { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}