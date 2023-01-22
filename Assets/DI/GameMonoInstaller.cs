using System.Collections;
using System.Collections.Generic;
using Sokoban;
using UnityEngine;
using Zenject;

namespace Sokoban.Di
{
   
    public class GameMonoInstaller : MonoInstaller<GameMonoInstaller>
    {
        // [SerializeField] private FieldManager _fieldManager;

        public override void InstallBindings()
        {
            Container.Bind<FieldManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
