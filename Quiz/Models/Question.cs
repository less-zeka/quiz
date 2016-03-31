using System.Collections.Generic;
using Quiz.Gameplay;

namespace Quiz.Models
{
    public class Question
    {
        public string QuestionText { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}