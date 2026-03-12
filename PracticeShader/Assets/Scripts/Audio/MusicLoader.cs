using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;

public class MusicLoader
{
    private AsyncOperationHandle<AudioClip> _currentHandle;

    public async UniTask<AudioClip> LoadMusicAsync(string address)
    {
        if (_currentHandle.IsValid()) Addressables.Release(_currentHandle);

        _currentHandle = Addressables.LoadAssetAsync<AudioClip>(address);
        return await _currentHandle.ToUniTask();
    }
}