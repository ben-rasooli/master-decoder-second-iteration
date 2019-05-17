using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

#pragma warning disable 0649, 0414
namespace Project
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] List<LevelData> LevelDatas;

        public override void InstallBindings()
        {
            Container.Bind<LevelData>().FromMethodMultiple(GetLevelDatas).AsCached();
            Container.Bind<LevelInfoUI>().FromComponentsInHierarchy().AsCached();
        }

        IEnumerable<LevelData> GetLevelDatas(InjectContext context)
        {
            return LevelDatas.OrderBy(levelData => levelData.Index);
        }
    }
}