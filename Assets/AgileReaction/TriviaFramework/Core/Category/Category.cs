using System;
using UnityEngine;

namespace AgileReaction.TriviaFramework.Core
{
    /// <summary>
    /// Object Category contains Name and ID
    /// </summary>
    [Serializable]
    public class Category
    {
        #region Public Fields

        [SerializeField]
        public int categoryID;

        [SerializeField]
        public string categoryName;

        #endregion Public Fields

        #region Public Constructors

        public Category(string CategoryName, int ID)
        {
            categoryName = CategoryName;
            categoryID = ID;
        }

        #endregion Public Constructors
    }
}