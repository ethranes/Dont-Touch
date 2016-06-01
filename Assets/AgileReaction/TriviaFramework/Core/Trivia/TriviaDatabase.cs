using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AgileReaction.TriviaFramework.Core
{
    /// <summary>
    /// Database for Trivia Questions
    /// </summary>
    public class TriviaDatabase : ScriptableObject
    {
        #region Private Fields

        [SerializeField]
        private List<Trivia> database;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Returns the number of Trivia Questions in database
        /// </summary>
        public int COUNT
        {
            get { return database.Count; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Adds Trivia to database
        /// </summary>
        /// <param name="trivia"></param>
        public void Add(Trivia trivia)
        {
            database.Add(trivia);
        }

        /// <summary>
        /// Returns a new list of Trivia
        /// </summary>
        /// <returns></returns>
        public List<Trivia> GetTrivia()
        {
            return new List<Trivia>(database);
        }

        /// <summary>
        /// Removes that specific Trivia
        /// </summary>
        /// <param name="trivia"></param>
        public void Remove(Trivia trivia)
        {
            database.Remove(trivia);
        }

        public void RemoveAt(int index)
        {
            database.RemoveAt(index);
        }

        /// <summary>
        /// Removes the Trivia at the index
        /// </summary>
        /// <param name="index"></param>
        /// <summary>
        /// Returns Trivia at the index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Trivia Trivia(int index)
        {
            return database.ElementAt(index);
        }

        /// <summary>
        /// Returns new List Trivia of that CategoryID
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public List<Trivia> TriviaByCategory(int Category)
        {
            List<Trivia> T = new List<Trivia>();
            for (int i = 0; i < database.Count; i++)
            {
                if (database.ElementAt(i).category == Category)
                {
                    T.Add(database.ElementAt(i));
                }
            }
            return T;
        }

        #endregion Public Methods

        #region Private Methods

        private void OnEnable()
        {
            if (database == null)
            {
                database = new List<Trivia>();
            }
        }

        #endregion Private Methods
    }
}