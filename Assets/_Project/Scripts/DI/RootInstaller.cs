using Doozy.Engine.SceneManagement;
using NodeCanvas.Framework;
using NodeCanvas.StateMachines;
using UnityEngine;
using Zenject;

namespace Project
{
    public class RootInstaller : MonoInstaller
    {
        [SerializeField] FSM _mainControllerFSM;
        [SerializeField] MainController _mainController;

        public override void InstallBindings()
        {
            Container.Bind<SceneDirector>().FromMethod(createSceneDirector).AsCached().NonLazy();
            Container.BindInstance(_mainController).AsCached().NonLazy();
            Container.Bind<FSM>().WithId("MainControllerFSM").FromMethod(injectMainControllerFSM).AsCached();
        }

        SceneDirector createSceneDirector(InjectContext context)
        {
            return SceneDirector.Instance;
        }

        FSM injectMainControllerFSM(InjectContext context)
        {
            var fsmOwner = gameObject.AddComponent<FSMOwner>();
            var blackboard = gameObject.AddComponent<Blackboard>();
            blackboard.AddVariable("DiContainer", context.Container);
            fsmOwner.blackboard = blackboard;
            fsmOwner.StartBehaviour(_mainControllerFSM);
            return fsmOwner.GetCurrentState().FSM;
        }
    }
}