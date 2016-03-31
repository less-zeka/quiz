using System.Collections.Generic;
using Quiz.Models;

namespace Quiz.Gameplay
{
    public class GameMaster
    {
        public static Question GetNextQuestion()
        {
            var question = new Question();
            var answers = new List<Answer>
            {
                new Answer {Id = 1, Text = "2", IsCorrect = false},
                new Answer {Id = 2, Text = "1", IsCorrect = false},
                new Answer {Id = 3, Text = "4", IsCorrect = true},
                new Answer {Id = 4, Text = "3", IsCorrect = false}
            };

            question.QuestionText = "Wieviel gibt 2+2?";
            question.Answers = answers;

            return question;
        }
    }
}