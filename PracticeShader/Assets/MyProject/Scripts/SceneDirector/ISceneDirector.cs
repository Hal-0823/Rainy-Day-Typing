using UnityEngine;
using Cysharp.Threading.Tasks;

public interface ISceneDirector
{
    UniTask OnLoadScene();
    UniTask OnUnloadScene();
}