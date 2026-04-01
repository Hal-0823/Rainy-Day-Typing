using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

/// <summary>
/// Addressablesを使用して非同期で音楽をロードするクラス
/// </summary>
public class MusicLoader : IDisposable
{
    private AsyncOperationHandle<AudioClip> _currentHandle;

    public async UniTask<AudioClip> LoadMusicAsync(string address, CancellationToken token)
    {
        // 前回のリソースが残っている場合は解放する
        ReleaseHandle();

        try
        {
            _currentHandle = Addressables.LoadAssetAsync<AudioClip>(address);
            return await _currentHandle.ToUniTask(cancellationToken: token);
        }
        catch (OperationCanceledException)
        {
            ReleaseHandle();
            throw;
        }
        catch (Exception e)
        {
            ReleaseHandle();
            Debug.LogError($"音楽のロードに失敗しました: {address}\n{e}");
            throw;
        }
    }

    private void ReleaseHandle()
    {
        if (_currentHandle.IsValid())
        {
            Addressables.Release(_currentHandle);
            _currentHandle = default;
        }
    }

    public void Dispose()
    {
        ReleaseHandle();
    }
}