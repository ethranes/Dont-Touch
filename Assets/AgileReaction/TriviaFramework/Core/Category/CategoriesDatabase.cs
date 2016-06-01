using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AgileReaction.TriviaFramework.Core
{
    /// <summary>
    /// Database for Categories
    /// </summary>
    public class CategoriesDatabase : ScriptableObject
    {
        #region Private Fields

        [SerializeField]
        private List<Category> database;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Returns the number of Categories in the database
        /// </summary>
        public int COUNT
        {
            get { return database.Count; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Adds a Category to the database
        /// </summary>
        /// <param name="category"></param>
        public void Add(Category category)
        {
            database.Add(category);
        }

        /// <summary>
        /// Returns the category at the index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Category Category(int index)
        {
            return database.ElementAt(index);
        }

        /// <summary>
        /// Returns a Category with that ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Category CategoryByID(int ID)
        {
            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].categoryID == ID)
                {
                    return database[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Returns a list of Categories
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            return new List<Category>(database);
        }

        /// <summary>
        /// Removes that Category from the database
        /// </summary>
        /// <param name="category"></param>
        public void Remove(Category category)
        {
            database.Remove(category);
        }

        /// <summary>
        /// Removes Category at that index
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            database.RemoveAt(index);
        }

        /// <summary>
        /// Returns the first CategoryID with that name
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        public int ReturnFirstIDByName(string CategoryName)
        {
            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].categoryName == CategoryName)
                {
                    return database[i].categoryID;
                }
            }
            return -1;
        }

        #endregion Public Methods

        #region Private Methods

        private void OnEnable()
        {
            if (database == null)
            {
                database = new List<Category>();
            }
        }

        #endregion Private Methods
    }
}