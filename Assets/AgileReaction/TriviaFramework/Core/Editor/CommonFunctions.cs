using AgileReaction.TriviaFramework.Core;
using UnityEditor;
using UnityEngine;

namespace AgileReaction.TriviaFramework.Editor
{
    /// <summary>
    /// Common functions shared between Editor Windows
    /// </summary>
    public class CommonFunctions
    {
        #region Private Fields

        private const string category_loc = @"Assets/TriviaDatabase/Resources/CategoriesDatabase.asset";

        /*!< Location to create database */

        private const string trivia_loc = @"Assets/TriviaDatabase/Resources/TriviaDatabase.asset";

        #endregion Private Fields

        /*!< Location to create database */

        #region Public Methods

        /// <summary>
        /// Creates CategoriesDatabase and returns it
        /// </summary>
        /// <returns></returns>
        public static CategoriesDatabase CreateCategoryDatabase()
        {
            CategoriesDatabase categoryDatabase = (CategoriesDatabase)ScriptableObject.CreateInstance(typeof(CategoriesDatabase));
            if (categoryDatabase != null)
            {
                CreateFolders();
                AssetDatabase.CreateAsset(categoryDatabase, category_loc);
                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
                categoryDatabase.Add(new Category("Default", 0));
                EditorUtility.SetDirty(categoryDatabase);
            }

            return categoryDatabase;
        }

        /// <summary>
        /// Creates the TriviaDatabase and returns it
        /// </summary>
        /// <returns></returns>
        public static TriviaDatabase CreateTriviaDatabase()
        {
            TriviaDatabase triviaDatabase = (TriviaDatabase)ScriptableObject.CreateInstance(typeof(TriviaDatabase));
            if (triviaDatabase != null)
            {
                CreateFolders();
                AssetDatabase.CreateAsset(triviaDatabase, trivia_loc);
                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
            }
            return triviaDatabase;
        }

        /// <summary>
        /// Loads the Categories Database, if it doesn't exist calls the Created Categories Database and returns Database
        /// </summary>
        /// <returns></returns>
        public static CategoriesDatabase LoadCategoriesDatabase()
        {
            CategoriesDatabase categoriesDatabase = Resources.Load("CategoriesDatabase") as CategoriesDatabase;
            if (categoriesDatabase == null)
            {
                categoriesDatabase = CreateCategoryDatabase();
            }
            return categoriesDatabase;
        }

        /// <summary>
        /// Loads the TriviaDatabase, if it doesn't exist creates the database and returns it.
        /// </summary>
        /// <returns></returns>
        public static TriviaDatabase LoadTriviaDatabase()
        {
            TriviaDatabase triviaDatabase = Resources.Load("TriviaDatabase") as TriviaDatabase;
            if (triviaDatabase == null)
            {
                triviaDatabase = CreateTriviaDatabase();
            }
            return triviaDatabase;
        }

        #endregion Public Methods

        #region Private Methods

        private static void CreateFolders()
        {
            if (!AssetDatabase.IsValidFolder("Assets/TriviaDatabase"))
            {
                AssetDatabase.CreateFolder("Assets", "TriviaDatabase");
            }
            if (!AssetDatabase.IsValidFolder("Assets/TriviaDatabase/Resources"))
            {
                AssetDatabase.CreateFolder("Assets/TriviaDatabase", "Resources");
            }
        }

        #endregion Private Methods
    }
}