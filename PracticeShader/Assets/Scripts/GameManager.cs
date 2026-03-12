using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public TypingData typingData;

    [SerializeField]
    private TypingManager typingManager;

    private int typeCount = 0;
    public static event Action<int> OnTypeCountUpdated;

    [SerializeField]
    private int count_knock = 521;
    public static event Action OnKnock;

    [SerializeField]
    private int count_bike = 100;
    public static event Action OnBike;


    private void OnEnable()
    {
        TypingManager.OnTypeCorrect += AddTypeCount;
    }

    IEnumerator Start()
    {
        typingManager.SetTextAsset(typingData);
        yield return new WaitForSeconds(0.2f);
        typingManager.ActivateTyping();
    }


    private void AddTypeCount()
    {
        typeCount++;
        OnTypeCountUpdated?.Invoke(typeCount);

        if (typeCount == count_knock)
        {
            OnKnock?.Invoke();
        }

        if (typeCount == count_bike)
        {
            OnBike?.Invoke();
        }
    }
}