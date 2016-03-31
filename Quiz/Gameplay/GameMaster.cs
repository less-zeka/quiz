using System;
using System.Collections.Generic;
using System.Linq;
using Quiz.Models;

namespace Quiz.Gameplay
{
    public class GameMaster
    {
        private int CurrentRound { get; set; }
        public static Question CurrentQuestion { get; set; }
        public bool NewQuestionNeeded { get; set; }

        public GameMaster()
        {
            CurrentRound = 0;
            NewQuestionNeeded = true;
            LoadQuestions();
            LoadNextQuestion();
        }

        public void LoadNextQuestion()
        {
            if (NewQuestionNeeded)
            {
                var randomNr = new Random().Next(0, _questions.Count);
                CurrentQuestion = _questions.ElementAt(randomNr);
            }
        }


        public void ProceedToNextQuestion()
        {
            CurrentRound ++;
            NewQuestionNeeded = true;
        }

        private static void LoadQuestions()
        {
            _questions = new List<Question>
            {
                new Question
                {
                    QuestionText = "Wieviel gibt 2+2?",
                    Answers = new List<Answer>
                    {
                        new Answer {Id = 1, Text = "2", IsCorrect = false},
                        new Answer {Id = 2, Text = "1", IsCorrect = false},
                        new Answer {Id = 3, Text = "4", IsCorrect = true},
                        new Answer {Id = 4, Text = "3", IsCorrect = false}
                    }
                },
                new Question
                {
                    QuestionText = "Wie ist das Wetter?",
                    Answers = new List<Answer>
                    {
                        new Answer {Id = 1, Text = "Das Wetter ist nicht. Das Wetter wird.", IsCorrect = false},
                        new Answer {Id = 2, Text = "Schön", IsCorrect = true},
                        new Answer {Id = 3, Text = "Regnerisch", IsCorrect = false},
                        new Answer {Id = 4, Text = "Kalt!", IsCorrect = false}
                    }
                },
                new Question
                {
                    QuestionText = "C# ist ...",
                    Answers = new List<Answer>
                    {
                        new Answer {Id = 1, Text = "Ein preprocessing Compiler", IsCorrect = false},
                        new Answer {Id = 2, Text = "Eine Programmiersprache", IsCorrect = true},
                        new Answer {Id = 3, Text = "Ein Framework", IsCorrect = false},
                        new Answer {Id = 4, Text = "Kalt!", IsCorrect = false}
                    }
                }
            };
        }

        private static ICollection<Question> _questions;
    }
}