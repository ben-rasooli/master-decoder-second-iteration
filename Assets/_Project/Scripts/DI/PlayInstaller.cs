using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

#pragma warning disable 0649, 0414
namespace Project
{
    public class PlayInstaller : MonoInstaller
    {
        [SerializeField] RectTransform _playCanvas;

        public override void InstallBindings()
        {
            MainController mainController = Container.Resolve<MainController>();

            var attemptResultUI = mainController.LevelData.AttemptResultPrefab.GetComponent<AttemptResultUI>();
            Container.Bind<RectTransform>().WithId("attemptResultUI").FromComponentInNewPrefab(attemptResultUI).AsCached();
            Container.BindInstance(attemptResultUI).AsCached();
            Container.Bind<AttemptResultsUI>().FromComponentInHierarchy().AsCached();
            Container.Bind<ScrollRect>().FromComponentInHierarchy().AsCached();

            Container.BindInstance(_playCanvas).WithId("playCanvas").AsCached();
            Container.Bind<CodeGenerator>().AsCached()
                     .WithArguments(
                         attemptResultUI.CodeCount,
                         attemptResultUI.SymbolCount,
                         attemptResultUI.ColorCount);

            Container.Bind<PiecePickingUI>().FromComponentInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<ResultsController>().AsCached();

            var guessingPanelPrefab = mainController.LevelData.GuessingPanelPrefab;
            Container.Bind<RectTransform>().WithId("guessingPanel").FromComponentInNewPrefab(guessingPanelPrefab).AsCached();

            PlaySignalsInstaller.Install(Container);
        }
    }

    class PlaySignalsInstaller : Installer<PlaySignalsInstaller>
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<GameIsOverSignal>();
        }
    }
}