using AgileReaction.TriviaFramework.Core;
using UnityEditor;
using UnityEngine;

namespace AgileReaction.TriviaFramework.Editor
{
    /// <summary>
    /// Category Editor window for Adding/Editing/Removing Categories
    /// </summary>
    public class CategoryEditor : EditorWindow
    {
        private enum State
        {
            BLANK,
            EDIT,
            ADD
        }

        private State state;
        private Vector2 _scrollPos;
        private int selectedCategory;
        private string newCategory;

        private CategoriesDatabase categoriesDatabase;

        [MenuItem("AG/TriviaEditor/Categories")]
        public static void Init()
        {
            CategoryEditor window = CategoryEditor.GetWindow<CategoryEditor>();
            window.minSize = new Vector2(800, 400);
#if UNITY_5_3_OR_NEWER
            window.titleContent = new GUIContent("Categories Editor");
#else
            window.title = "Categories Editor";
#endif
            window.Show();
        }

        private void OnEnable()
        {
            if (categoriesDatabase == null)
            {
                categoriesDatabase = CommonFunctions.LoadCategoriesDatabase();
            }

            state = State.BLANK;
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            DisplayCategories();
            DisplayMainArea();
            EditorGUILayout.EndHorizontal();
        }

        private void DisplayCategories()
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(250));
            EditorGUILayout.Space();
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, "box", GUILayout.ExpandHeight(true));

            for (int cnt = 0; cnt < categoriesDatabase.COUNT; cnt++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("-", GUILayout.Width(25)))
                {
                    if (IsCatEmpty(categoriesDatabase.Category(cnt).categoryID))
                    {
                        categoriesDatabase.RemoveAt(cnt);
                        EditorUtility.SetDirty(categoriesDatabase);
                        state = State.BLANK;
                        return;
                    }
                    else
                    {
                        Debug.LogWarning("Unable to Delete, Category is not Empty");
                    }
                }
                GUI.SetNextControlName("Clear");
                if (GUILayout.Button(categoriesDatabase.Category(cnt).categoryName, "box", GUILayout.ExpandWidth(true)))
                {
                    GUI.FocusControl("Clear");
                    selectedCategory = cnt;
                    state = State.EDIT;
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField("Categories: " + categoriesDatabase.COUNT, GUILayout.Width(100));

            if (GUILayout.Button("New Category"))
                state = State.ADD;

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }

        private void DisplayMainArea()
        {
            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            EditorGUILayout.Space();

            switch (state)
            {
                case State.ADD:
                    DisplayAddMainArea();
                    break;

                case State.EDIT:
                    DisplayEditMainArea();
                    break;

                default:
                    DisplayBlankMainArea();
                    break;
            }

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }

        private void DisplayBlankMainArea()
        {
            EditorGUILayout.LabelField(
                "Click New Category to add a new category\n" +
                "or click on a category to change its name",
                GUILayout.ExpandHeight(true));
        }

        private void DisplayEditMainArea()
        {
            categoriesDatabase.Category(selectedCategory).categoryName = EditorGUILayout.TextField(new GUIContent("Category: "), categoriesDatabase.Category(selectedCategory).categoryName);
            EditorGUILayout.LabelField("CategoryID: " + categoriesDatabase.Category(selectedCategory).categoryID, GUILayout.Width(100));

            EditorGUILayout.Space();

            if (GUILayout.Button("Done", GUILayout.Width(100)))
            {
                EditorUtility.SetDirty(categoriesDatabase);
                state = State.BLANK;
            }
        }

        private void DisplayAddMainArea()
        {
            newCategory = EditorGUILayout.TextField(new GUIContent("Category: "), newCategory);

            int categoryID = GetUniqueID(categoriesDatabase.COUNT);

            EditorGUILayout.LabelField("CategoryID: " + categoryID, GUILayout.Width(100));

            EditorGUILayout.Space();

            if (GUILayout.Button("Done", GUILayout.Width(100)))
            {
                categoriesDatabase.Add(new Category(newCategory, categoryID));
                newCategory = string.Empty;

                EditorUtility.SetDirty(categoriesDatabase);
                state = State.BLANK;
            }
        }

        private int GetUniqueID(int initial)
        {
            while (categoriesDatabase.CategoryByID(initial) != null)
            {
                initial++;
            }
            return initial;
        }

        private bool IsCatEmpty(int CatID)
        {
            TriviaDatabase triviaData = CommonFunctions.LoadTriviaDatabase();

            for (int i = 0; i < triviaData.COUNT; i++)
            {
                if (triviaData.Trivia(i).category == CatID)
                {
                    return false;
                }
            }
            return true;
        }
    }
}