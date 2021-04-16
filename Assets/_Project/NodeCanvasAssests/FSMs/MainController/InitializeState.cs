using NodeCanvas.StateMachines;
using ParadoxNotion;
using ParadoxNotion.Design;
using Zenject;

namespace Project
{
    [Category("Project/MainController")]
    public class InitializeState : FSMState
    {
        MainController _mainController;

        protected override void OnInit()
        {
            _mainController = ((DiContainer)graphBlackboard.parent.variables["DiContainer"].value).Resolve<MainController>();
        }

        protected override void OnEnter()
        {
            FSM.SendEvent("gotoMainMenuState", null, null);
        }
    }
}