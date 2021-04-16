using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using UnityEngine;
using Zenject;

namespace Project
{
    [Category("Project/MainController")]
    public class PlayState : FSMState
    {
        MainController _mainController;

        protected override void OnInit()
        {
            _mainController = ((DiContainer)graphBlackboard.variables["DiContainer"].value).Resolve<MainController>();
        }

        protected override void OnEnter()
        {
            System.GC.Collect();
            _mainController.LoadScene("_Project/Scenes/GameplayModes/Normal");
        }

        protected override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                _mainController.HandlePlay_BackButtonClick();
        }

        protected override void OnExit()
        {
            _mainController.UnloadScene("_Project/Scenes/GameplayModes/Normal");
        }
    }
}