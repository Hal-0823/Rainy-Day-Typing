using UnityEngine;
using DG.Tweening;

public class BikeAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip bikeSE;

    private void OnEnable()
    {
        //GameManager.OnBike += PlayBikeSE;
    }

    private void OnDisable()
    {
        //GameManager.OnBike -= PlayBikeSE;
    }

    private void PlayBikeSE()
    {
        if (audioSource != null && bikeSE != null)
        {
            // 開始位置にセット
            transform.position = new Vector3(30, transform.position.y, transform.position.z);

            audioSource.clip = bikeSE;
            audioSource.Play();

            // x = 30 -> x = -30
            // SEの長さに合わせて移動
            transform.DOMoveX(-30, bikeSE.length)
                .SetEase(Ease.InOutSine);
        }
    }
}