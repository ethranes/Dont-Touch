using UnityEngine;

namespace AgileReaction.TriviaFramework.Example
{
    public class exit : MonoBehaviour
    {
        #region Private Methods

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        #endregion Private Methods
    }
}