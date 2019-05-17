using Doozy.Engine.UI;
using UnityEngine.UI;
using TMPro;
using Zenject;

namespace Project
{
    public class LevelInfoUIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Image>().WithId("backgroundImage").FromComponentOnRoot().AsCached();
            Container.Bind<TextMeshProUGUI>().WithId("titleText").FromMethod(getTitleText).AsCached();
            Container.Bind<TextMeshProUGUI>().WithId("descriptionText").FromMethod(getDescriptionText).AsCached();
            Container.Bind<UIButton>().WithId("playButton").FromComponentInChildren().AsCached();
        }

        TextMeshProUGUI getTitleText(InjectContext context)
        {
            return GetComponentInChildren<Title_tag>().GetComponent<TextMeshProUGUI>();
        }

        TextMeshProUGUI getDescriptionText(InjectContext context)
        {
            return GetComponentInChildren<Description_tag>().GetComponent<TextMeshProUGUI>();
        }
    }
}