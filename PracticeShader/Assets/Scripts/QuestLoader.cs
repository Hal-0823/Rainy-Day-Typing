using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

public class QuestLoader
{
    public readonly struct QuestData
    {
        public readonly string Furigana;
        public readonly string Question;

        public QuestData(string furigana, string question)
        {
            Furigana = furigana;
            Question = question;
        }
    }

    public async UniTask<List<QuestData>> LoadQuestDataAsync(string address)
    {
        var textAsset = await Addressables.LoadAssetAsync<TextAsset>(address).ToUniTask();

        var list = new List<QuestData>();

        using (var reader = new StringReader(textAsset.text))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(',');
                if (parts.Length >= 2)
                {
                    var question = parts[0];
                    var furigana = parts[1];
                    list.Add(new QuestData(furigana, question));
                }
            }
        }

        Addressables.Release(textAsset);
        return list;
    }
}