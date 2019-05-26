using UnityEngine.SceneManagement;
using NodeCanvas.StateMachines;
using Zenject;
using ParadoxNotion;

#pragma warning disable 0649, 0414
namespace Project
{
    public class MainController : IInitializable
    {
        #region ----------------------------------------dependencies
        [Inject(Id = "MainControllerFSM")] FSM _FSM;

        #endregion

        #region ----------------------------------------API
        public LevelData LevelData;

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        public void HandleMainMenu_LevelButtonClick()
        {
            _FSM.SendEvent(gotoPlayState, null);
        }
        EventData gotoPlayState = new EventData("gotoPlayState");

        public void Handle_GameOverPopupButtonClick()
        {
            _FSM.SendEvent(gotoMainMenuState, null);
        }
        EventData gotoMainMenuState = new EventData("gotoMainMenuState");
        #endregion

        #region ----------------------------------------
        public void Initialize()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        #endregion

        #region ----------------------------------------details
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.SetActiveScene(scene);
        }
        #endregion
    }
}
