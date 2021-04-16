using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Zenject;

namespace Project
{
    [Category("Project/MainController")]
    public class MainMenuState : FSMState
    {
        MainController _mainController;

        protected override void OnInit()
        {
            _mainController = ((DiContainer)graphBlackboard.parent.variables["DiContainer"].value).Resolve<MainController>();
        }

        protected override void OnEnter()
        {
            System.GC.Collect();
            _mainController.LoadScene("MainMenu");
        }

        protected override void OnExit()
        {
            _mainController.UnloadScene("MainMenu");
        }
    }
}
