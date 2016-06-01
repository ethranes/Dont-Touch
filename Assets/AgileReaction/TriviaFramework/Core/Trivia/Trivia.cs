using System;
using System.Collections.Generic;
using UnityEngine;

namespace AgileReaction.TriviaFramework.Core
{
    /// <summary>
    /// Object Trivia contains int CategoryID, String Question, string Answer, and a list of strings for Fake Answers
    /// </summary>
    [Serializable]
    public class Trivia
    {
        [SerializeField]
        public int category;

        [SerializeField]
        public List<TriviaAnswer> answers;

        [SerializeField]
        public string question;

        [SerializeField]
        public Sprite image;

        public Trivia(int CategoryID, string Question, List<TriviaAnswer> Answers, Sprite Image)
        {
            category = CategoryID;
            question = Question;
            image = Image;
            answers = Answers;
        }

    }
}