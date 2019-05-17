using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Zenject;

namespace Project
{
    [Category("Project/MainController")]
    public class PlayState : FSMState
    {
        MainController _mainController;

        protected override void OnInit()
        {
            _mainController = graphBlackboard
                .GetVariable<DiContainer>("DiContainer").GetValue()
                .Resolve<MainController>();
        }

        protected override void OnEnter()
        {
            System.GC.Collect();
            _mainController.LoadScene("_Project/Scenes/GameplayModes/Normal");
        }

        protected override void OnExit()
        {
            _mainController.UnloadScene("_Project/Scenes/GameplayModes/Normal");
        }
    }
}