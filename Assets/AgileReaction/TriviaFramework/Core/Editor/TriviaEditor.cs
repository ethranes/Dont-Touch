using AgileReaction.TriviaFramework.Core;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AgileReaction.TriviaFramework.Editor
{
    /// <summary>
    /// Trivia Editor Window for adding/Editing/Removing Trivia Questions
    /// </summary>
    public class TriviaEditor : EditorWindow
    {
        private enum State
        {
            BLANK,
            EDIT,
            ADD
        }

        private State state;
        private TriviaDatabase triviaDatabase;
        private int seletedTrivia;
        private Vector2 scrollPos;

        private string newQuestion;
        private int newCategory;
        private Texture2D triviaTexture;
        private Sprite newImage;
        private float imageHeight = 250;
        private float imageWidth = 250;
        private List<TriviaAnswer> newAnswers = new List<TriviaAnswer>();
        private CategoriesDatabase categoryDatabase;
        private List<string> categoryList = new List<string>();

        private bool filterTrivia = false;
        private int catFiltered = 0;

        private int listID = 0;

        private List<string> catListToBeFiltered = new List<string>();
        //private const string trivia_loc = @"Assets/TriviaDatabase/TriviaDatabase.asset";

        [MenuItem("AG/TriviaEditor/Trivia")]
        public static void Init()
        {
            TriviaEditor window = TriviaEditor.GetWindow<TriviaEditor>();
            window.minSize = new Vector2(800, 400);
#if UNITY_5_3_OR_NEWER
            window.titleContent = new GUIContent("Trivia Editor");
#else
            window.title = "Trivia Editor";
#endif
            window.Show();
        }

        private void OnEnable()
        {
            if (triviaDatabase == null)
            {
                triviaDatabase = CommonFunctions.LoadTriviaDatabase();
            }

            if (categoryDatabase == null)
            {
                categoryDatabase = CommonFunctions.LoadCategoriesDatabase();
            }
            state = State.BLANK;
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

            UpdateCategories();
            DisplayTrivia();
            DisplayMainArea();
            EditorGUILayout.EndHorizontal();
        }

        private void UpdateCategories()
        {
            if (categoryDatabase != null)
            {
                categoryList.Clear();
                for (int i = 0; i < categoryDatabase.COUNT; i++)
                {
                    categoryList.Add(categoryDatabase.Category(i).categoryName);
                }
                catListToBeFiltered = new List<string>(categoryList);
            }
        }

        private void DisplayTrivia()
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(250));
            EditorGUILayout.Space();

            catListToBeFiltered.Insert(0, "All");
            catFiltered = EditorGUILayout.Popup("Filter By Category: ", catFiltered, catListToBeFiltered.ToArray());
            int catFilterID = 0;
            if (catFiltered != 0)
            {
                filterTrivia = true;
                catFilterID = categoryDatabase.Category(catFiltered - 1).categoryID;
            }
            else
            {
                filterTrivia = false;
            }
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, "box", GUILayout.ExpandHeight(true));

            for (int cnt = 0; cnt < triviaDatabase.COUNT; cnt++)
            {
                if (Filter(cnt, catFilterID))
                {
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("-", GUILayout.Width(25)))
                    {
                        triviaDatabase.RemoveAt(cnt);
                        EditorUtility.SetDirty(triviaDatabase);
                        state = State.BLANK;
                        return;
                    }

                    GUI.SetNextControlName("Clear");
                    if (GUILayout.Button(triviaDatabase.Trivia(cnt).question, "box", GUILayout.ExpandWidth(true)))
                    {
                        GUI.FocusControl("Clear");
                        seletedTrivia = cnt;
                        state = State.EDIT;
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField("Questions: " + triviaDatabase.COUNT, GUILayout.Width(100));

            if (GUILayout.Button("New Trivia"))
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
                "Click Add Trivia to add new trivia\n" +
                "You can add Question and Answers here\n",
                GUILayout.ExpandHeight(true));
        }

        private void DisplayEditMainArea()
        {
            GetListID(triviaDatabase.Trivia(seletedTrivia).category);
            listID = EditorGUILayout.Popup("Category: ", listID, categoryList.ToArray());

            triviaDatabase.Trivia(seletedTrivia).category = GetCategoryID();
            if (triviaDatabase.Trivia(seletedTrivia).image)
            {
                triviaTexture = triviaDatabase.Trivia(seletedTrivia).image.texture;
            } else
            {
                triviaTexture = null;
            }
            ImagePicker(triviaTexture, false);
            
            triviaDatabase.Trivia(seletedTrivia).question = EditorGUILayout.TextField(new GUIContent("Question: "), triviaDatabase.Trivia(seletedTrivia).question);

            if (triviaDatabase.Trivia(seletedTrivia).answers.Count > 0)
            {
                for (int i = 0; i < triviaDatabase.Trivia(seletedTrivia).answers.Count; i++)
                {
                    GUILayout.BeginHorizontal();

                    if (GUILayout.Button("-", GUILayout.Width(25)))
                    {
                        triviaDatabase.Trivia(seletedTrivia).answers.RemoveAt(i);
                        return;
                    }
                    triviaDatabase.Trivia(seletedTrivia).answers[i].answer = EditorGUILayout.TextField(new GUIContent("Answer " + i + ": "), triviaDatabase.Trivia(seletedTrivia).answers[i].answer);
                    EditorGUILayout.LabelField("is True?", GUILayout.Width(50));
                    triviaDatabase.Trivia(seletedTrivia).answers[i].isTrue = EditorGUILayout.Toggle(triviaDatabase.Trivia(seletedTrivia).answers[i].isTrue, GUILayout.Width(50));

                    GUILayout.EndHorizontal();
                }
            }

            if (GUILayout.Button("Add Answer", GUILayout.Width(150)))
            {
                triviaDatabase.Trivia(seletedTrivia).answers.Add(new TriviaAnswer("", false));
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Done", GUILayout.Width(100)))
            {
                EditorUtility.SetDirty(triviaDatabase);
                state = State.BLANK;
            }
        }

        private void ImagePicker(Texture2D tex, bool isNew)
        {
            if (GUILayout.Button(tex, GUILayout.Width(imageWidth), GUILayout.Height(imageHeight)))
            {
                int controlerID = EditorGUIUtility.GetControlID(FocusType.Passive);
                EditorGUIUtility.ShowObjectPicker<Sprite>(null, true, null, controlerID);
            }

            string commandName = Event.current.commandName;
            if (commandName == "ObjectSelectorUpdated")
            {
                if (!isNew)
                {
                    triviaDatabase.Trivia(seletedTrivia).image = (Sprite)EditorGUIUtility.GetObjectPickerObject();
                } else
                {
                    newImage = (Sprite)EditorGUIUtility.GetObjectPickerObject();
                }
                Repaint();
            }
        }

        private int GetCategoryID()
        {
            return categoryDatabase.ReturnFirstIDByName(categoryList[listID]);
        }

        private void GetListID(int CategoryID)
        {
            Category cat = categoryDatabase.CategoryByID(CategoryID);
            string catName = cat.categoryName;
            for (int i = 0; i < categoryList.Count; i++)
            {
                if (categoryList[i] == catName)
                {
                    listID = i;
                    return;
                }
            }
            listID = 0; // if nothing else is found
        }

        private void DisplayAddMainArea()
        {
            listID = EditorGUILayout.Popup("Category: ", listID, categoryList.ToArray());
            newCategory = GetCategoryID();
            Texture2D tex = null;
            if (newImage)
            {
                tex = newImage.texture;
            }
            ImagePicker(tex, true);
            newQuestion = EditorGUILayout.TextField(new GUIContent("Question: "), newQuestion);

            if (newAnswers.Count == 0)
            {
                newAnswers.Add(new TriviaAnswer("", false));
            }

            if (newAnswers.Count > 0)
            {
                for (int i = 0; i < newAnswers.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("-", GUILayout.Width(25)))
                    {
                        newAnswers.RemoveAt(i);
                        return;
                    }
                    newAnswers[i].answer = EditorGUILayout.TextField(new GUIContent("Answer " + i + ": "), newAnswers[i].answer);
                    EditorGUILayout.LabelField("is True?", GUILayout.Width(50));
                    newAnswers[i].isTrue = EditorGUILayout.Toggle(newAnswers[i].isTrue, GUILayout.Width(50));
                    GUILayout.EndHorizontal();
                }
            }
            if (GUILayout.Button("Add Answer", GUILayout.Width(150)))
            {
                newAnswers.Add(new TriviaAnswer("", false));
            }
            EditorGUILayout.Space();

            if (GUILayout.Button("Done", GUILayout.Width(100)))
            {
                triviaDatabase.Add(new Trivia(newCategory, newQuestion, newAnswers, newImage));

                newImage = null;
                newQuestion = string.Empty;
                newAnswers = new List<TriviaAnswer>();
                newCategory = 0;

                EditorUtility.SetDirty(triviaDatabase);
                state = State.BLANK;
            }
        }

        private bool Filter(int cnt, int category)
        {
            if (filterTrivia)
            {
                return triviaDatabase.Trivia(cnt).category == category;
            }
            else
            {
                return true;
            }
        }
    }
}