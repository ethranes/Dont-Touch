using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AgileReaction.TriviaFramework.Core;

namespace AgileReaction.TriviaFramework.Example
{
    /// <summary>
    /// Simple example script of a implementation of TriviaFramework
    /// </summary>
    public class TriviaExample : MonoBehaviour
    {
        /// <summary>
        /// This example requires the Answers Text List order
        /// to match up with button press events, because it
        /// doesn't actually read the answers on the buttons
        /// that are pressed.
        /// </summary>

        #region Private Fields

        [SerializeField]
        private Color ColorCorrect = new Color(0, 255, 0);

        [SerializeField]
        private Color ColorNormal = new Color(197, 197, 197);

        [SerializeField]
        private Color ColorWrong = new Color(255, 0, 0);
        [SerializeField]
        private Image image;

        [SerializeField]
        private List<Image> imgAnswers;

        private bool isAnswered = false;

        private TriviaDatabaseAccess TriviaData;

        [SerializeField]
        private List<Text> txtAnswers;

        [SerializeField]
        private Text txtQuestion;

        [SerializeField]
        private Text txtTitle;

        private List<TriviaAnswer> answers;

        #endregion Private Fields

        #region Public Methods

        public void BtnAnswer0()
        {
            SubmitAnswer(0);
        }

        public void BtnAnswer1()
        {
            SubmitAnswer(1);
        }

        public void BtnAnswer2()
        {
            SubmitAnswer(2);
        }

        public void BtnAnswer3()
        {
            SubmitAnswer(3);
        }

        #endregion Public Methods

        #region Private Methods

        private void init()
        {
            TriviaData = TriviaDatabaseAccess.instance;
            if (TriviaData)
            {
                TriviaData.LoadTrivia(true); // Loads Trivia and makes it random, you can optionally load only one particular category too.
                NewQuestion();
            }
        }

        private void NewQuestion()
        {
            isAnswered = false;
            txtTitle.text = "";
            SetInitBtnColor(ColorNormal); //change buttons colors back to normal
            txtQuestion.text = TriviaData.GetQuestion(); // get and set the question to the label

            answers = TriviaData.GetRandomizeAnswers(); // gets a mix of answers

            if (TriviaData.GetImage())
            {
                //set image and make visible
                image.sprite = TriviaData.GetImage();
                image.color = new Color(255,255,255,1);
            } else
            {
                //make invisible
                image.color = new Color(255, 255, 255, 0);
            }
            //Set each button text to a answer
            for (int i = 0; i < answers.Count; i++)
            {
                txtAnswers[i].text = answers[i].answer;
            }
        }

        private void SetButtonColor(int btn, Color color)
        {
            imgAnswers[btn].color = color;
        }

        private void SetInitBtnColor(Color color)
        {
            for (int i = 0; i < imgAnswers.Count; i++)
            {
                imgAnswers[i].color = color;
            }
        }

        // text label for the question
        // Text labels for the buttons
        private void Start()
        {
            init(); // Needs to start after TriviaDatabaseAccess otherwise it can't find the instance which makes its
            // instance in Awake Function
        }

        private void SubmitAnswer(int btnPressed)
        {
            if (isAnswered)
            {
                return; //prevents from answering again till new question
            }

            isAnswered = true;

            if (answers[btnPressed].isTrue) // Test if button pressed number is the correct button number
            {
                //You got the Question Right
                txtTitle.text = "You got it right";
                SetButtonColor(btnPressed, ColorCorrect);
            }
            else
            {
                //You got the Question Wrong
                txtTitle.text = "Wrong";
                SetButtonColor(btnPressed, ColorWrong);
            }

            if (TriviaData.trivia.Count > 1) // checks to see if that was the last one
            {
                TriviaData.PopTrivia(); // Removes Trivia from the list so its not repeated and
                Invoke("NewQuestion", 3); //Changes question in 5 seconds
                //NewQuestion();
            }
            else
            {
                Debug.Log("No more trivia");
            }
        }

        #endregion Private Methods
    }
}