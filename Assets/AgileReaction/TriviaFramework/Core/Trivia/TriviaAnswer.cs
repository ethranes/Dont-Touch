using UnityEngine;
using System;
using System.Collections;

namespace AgileReaction.TriviaFramework.Core
{
    /// <summary>
    /// Contains the answers for the trivia questions
    /// </summary>
    [Serializable]
    public class TriviaAnswer
    {
        [SerializeField]
        public string answer;

        [SerializeField]
        public bool isTrue;

        public TriviaAnswer(string Answer, bool IsTrue)
        {
            answer = Answer;
            isTrue = IsTrue;
        }
    }
}