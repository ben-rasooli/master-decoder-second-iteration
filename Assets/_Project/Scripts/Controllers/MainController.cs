using UnityEngine;
using UnityEngine.SceneManagement;
using Doozy.Engine.SceneManagement;
using Doozy.Engine.Progress;
using NodeCanvas.StateMachines;
using Zenject;
using ParadoxNotion;

#pragma warning disable 0649, 0414
namespace Project
{
    public class MainController : MonoBehaviour
    {
        #region ----------------------------------------dependencies
        [Inject] SceneDirector _sceneDirector;
        [Inject(Id = "MainControllerFSM")] FSM _FSM;
        [Inject(Id = "sceneLoadingProgressor")] Progressor _progressor;
        #endregion

        #region ----------------------------------------API
        public LevelData LevelData;

        public void LoadScene(string sceneName)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            var sceneLoader = SceneDirector.LoadSceneAsync(sceneName, LoadSceneMode.Additive, _progressor);
            sceneLoader.SceneActivationDelay = 0.4f;
            sceneLoader.SetSelfDestructAfterSceneLoaded(true);
        }

        public void UnloadScene(string sceneName)
        {
            SceneDirector.UnloadSceneAsync(sceneName);
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

        #region ----------------------------------------Unity Messages
        void Start()
        {
            DontDestroyOnLoad(this);
        }
        #endregion

        #region ----------------------------------------details
        void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            print(scene.name);
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.SetActiveScene(scene);
        }
        #endregion
    }
}
