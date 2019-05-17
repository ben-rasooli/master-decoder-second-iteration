using Doozy.Engine.UI;
using TMPro;
using Zenject;

namespace Project
{
    public class GameOverUIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TextMeshProUGUI>().WithId("titleText").FromMethod(getTitleText).AsCached();
            Container.Bind<TextMeshProUGUI>().WithId("descriptionText").FromMethod(getDescriptionText).AsCached();
            Container.Bind<UIButton>().WithId("okButton").FromComponentInChildren().AsCached();
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