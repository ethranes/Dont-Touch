using AgileReaction.TriviaFramework.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AgileReaction.TriviaFramework
{
    /// <summary>
    /// Handles runtime Access to database
    /// </summary>
    public class TriviaDatabaseAccess : MonoBehaviour
    {

        #region Public Fields

        public static TriviaDatabaseAccess instance;

        [HideInInspector]
        public List<Category> categories = new List<Category>();

        [HideInInspector]
        public List<Trivia> trivia = new List<Trivia>();

        #endregion Public Fields

        #region Private Fields

        private CategoriesDatabase CatDatabase;
        private TriviaDatabase Database;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Gets list of Trivia Answers
        /// </summary>
        /// <returns></returns>
        public List<TriviaAnswer> GetAnswers()
        {
            return trivia[0].answers;
        }

        /// <summary>
        /// Returns a list of Categories
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            return categories;
        }

        /// <summary>
        /// Gets First Trivia Object in the list.
        /// </summary>
        /// <returns></returns>
        public Trivia GetFirstTrivia()
        {
            return trivia[0];
        }

        public Sprite GetImage()
        {
            return trivia[0].image;
        }

        /// <summary>
        /// Gets first Trivia Question in the list
        /// </summary>
        /// <returns></returns>
        public string GetQuestion()
        {
            return trivia[0].question;
        }

        /// <summary>
        /// Randomizes the Answers.
        /// </summary>
        /// <returns></returns>
        public List<TriviaAnswer> GetRandomizeAnswers()
        {
            List<TriviaAnswer> a = new List<TriviaAnswer>(trivia[0].answers);
            var rnd = new System.Random();
            var result = a.OrderBy(item => rnd.Next());
            a = result.ToList<TriviaAnswer>();
            return a;
        }

        /// <summary>
        /// Returns the sprite of the first Trivia
        /// </summary>
        /// <returns></returns>
        public void LoadCategories()
        {
            categories = CatDatabase.GetCategories();
        }

        /// <summary>
        /// Loads Trivia of that category, then randomizes it
        /// </summary>
        /// <param name="category"></param>
        /// <param name="makeRandom"></param>
        public void LoadTrivia(int category, bool makeRandom)
        {
            trivia = Database.TriviaByCategory(category);
            if (makeRandom)
            {
                RandomizeTrivia();
            }
        }

        /// <summary>
        /// Loads all Trivia, then randomizes it
        /// </summary>
        /// <param name="makeRandom"></param>
        public void LoadTrivia(bool makeRandom)
        {
            trivia = Database.GetTrivia();
            if (makeRandom)
            {
                RandomizeTrivia();
            }
        }

        /// <summary>
        /// Load Categories
        /// </summary>
        /// <summary>
        /// Removes the first Category
        /// </summary>
        public void PopCategory()
        {
            categories.RemoveAt(0);
        }

        /// <summary>
        /// Pop category at index
        /// </summary>
        /// <param name="index"></param>
        public void PopCategory(int index)
        {
            categories.RemoveAt(index);
        }

        /// <summary>
        /// Finds category by ID and remove it
        /// </summary>
        /// <param name="ID"></param>
        public void PopCategoryById(int ID)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].categoryID == ID)
                {
                    categories.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Removes the first Trivia
        /// </summary>
        public void PopTrivia()
        {
            trivia.RemoveAt(0);
        }

        /// <summary>
        /// Returns a list of RandomizeCategories
        /// </summary>
        /// <returns></returns>
        public void RandomizeCategories()
        {
            var rnd = new System.Random();
            var result = categories.OrderBy(item => rnd.Next());
            categories = result.ToList<Category>();
        }

        /// <summary>
        /// Randomizes Trivia
        /// </summary>
        public void RandomizeTrivia()
        {
            var rnd = new System.Random();
            var result = trivia.OrderBy(item => rnd.Next());
            trivia = result.ToList<Trivia>();
        }

        #endregion Public Methods

        #region Private Methods

        private void Awake()
        {
            MakeInstance();
            Database = Resources.Load("TriviaDatabase") as TriviaDatabase;
            CatDatabase = Resources.Load("CategoriesDatabase") as CategoriesDatabase;
            LoadCategories();
        }

        private void MakeInstance()
        {
            if (instance)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this);
        }

        #endregion Private Methods

    }
}