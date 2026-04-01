using System.Collections.Generic;
using System.Reflection;
using R3;
using Utils;

public class TypingModel
{
    /// <summary>
    /// タイピングの状態を表す構造体
    /// </summary>
    public readonly struct State
    {
        public readonly string FString;
        public readonly string QString;
        public readonly string RString;
        public readonly int RNum;
        public readonly bool IsCorrect;

        public State(string fString, string qString, string rString, int rNum, bool isCorrect)
        {
            FString = fString;
            QString = qString;
            RString = rString;
            RNum = rNum;
            IsCorrect = isCorrect;
        }
    }

    // 現在のタイピング状態
    public readonly ReactiveProperty<State> CurrentState = new();
    // 単語を正しく打ち終えたときのイベント
    private readonly Subject<Unit> _onWordCompletedSubject = new();
    public Observable<Unit> OnWordCompleted => _onWordCompletedSubject;
    // 入力受付中かどうか
    public bool IsAcceptingInput { get; set; } = true;

    // 問題データのリスト
    private List<QuestLoader.QuestData> QuestDataList;
    private int currentQuestIndex = -99;

    private readonly ChangeDictionary _cd = new ChangeDictionary();

    private List<string> _romSliceList = new List<string>();
    private List<int> _romNumList = new List<int>();
    private List<int> _furiCountList = new List<int>();

    private string _fString;    // フリガナ文字列
    private string _qString;    // 問題文文字列
    private string _rString;    // ローマ字文字列
    private bool _isCorrect;    // 今回の入力が正解かどうか
    private int _rNum;  // 何文字目を入力しているか

    public TypingModel(List<QuestLoader.QuestData> questDataList)
    {
        QuestDataList = questDataList;
        currentQuestIndex = -99;
    }

    public void NextWord()
    {
        currentQuestIndex++;
        if (currentQuestIndex >= QuestDataList.Count || currentQuestIndex < 0)
        {
            currentQuestIndex = 0;
            QuestDataList.Shuffle();
        }

        var questData = QuestDataList[currentQuestIndex];
        SetWord(questData.Furigana, questData.Question);
    }

    public void HandleInput(char inputChar)
    {
        // 入力受付中でなければ無視
        if (!IsAcceptingInput) return;
        // 入力すべき文字数を超えていたら無視
        if (_rNum >= _rString.Length) return;

        int furiCount = _furiCountList[_rNum];
        _isCorrect = false;

        if (inputChar == _rString[_rNum])
        {
            _isCorrect = true;
            Correct();
        }
        else if (inputChar == 'n' && furiCount > 0 && _romSliceList[furiCount - 1] == "n")
        {
            _romSliceList[furiCount - 1] = "nn";
            _rString = string.Join("", GetRomSliceListWithoutSkip());
            ReCreateRomSliceList(_romSliceList);

            _isCorrect = true;
            Correct();
        }
        else
        {
            if (furiCount < _fString.Length - 1)
            {
                string addNextMoji = _fString[furiCount].ToString() + _fString[furiCount + 1].ToString();
                CheckIrregularType(inputChar, addNextMoji, furiCount, false);
            }

            if (!_isCorrect)
            {
                string moji = _fString[furiCount].ToString();
                CheckIrregularType(inputChar, moji, furiCount, true);
                Miss();
            }
        }

        // 状態更新
        CurrentState.Value = new State(_fString, _qString, _rString, _rNum, _isCorrect);
    }

    private void SetWord(string fString, string qString)
    {
        // 0番目の文字に戻す
        _rNum = 0;

        _fString = fString;
        _qString = qString;

        CreateRomSliceList(_fString);

        _rString = string.Join("", GetRomSliceListWithoutSkip());

        // 状態更新
        CurrentState.Value = new State(_fString, _qString, _rString, _rNum, false);
    }

    private void Correct()
    {
        _rNum++;

        if (_rNum >= _rString.Length)
        {
            _onWordCompletedSubject.OnNext(Unit.Default);
        }
    }

    private void Miss()
    {
        
    }

    private List<string> GetRomSliceListWithoutSkip()
    {
        List<string> returnList = new List<string>();
        foreach (string rom in _romSliceList)
        {
            if (rom == "SKIP") continue;

            returnList.Add(rom);
        }
        return returnList;
    }

    private void CreateRomSliceList(string fString)
    {
        _romSliceList.Clear();
        _romNumList.Clear();
        _furiCountList.Clear();

        for (int i=0; i<fString.Length; i++)
        {
            string r = _cd.dic[fString[i].ToString()][0];

            if (fString[i].ToString() == "ゃ" || fString[i].ToString() == "ゅ" || fString[i].ToString() == "ょ" ||
                fString[i].ToString() == "ぁ" || fString[i].ToString() == "ぃ" || fString[i].ToString() == "ぅ" || fString[i].ToString() == "ぇ" || fString[i].ToString() == "ぉ")
            {
                r = "SKIP";
            }
            else if (fString[i].ToString() == "っ" && i + 1 < fString.Length)
            {
                r = _cd.dic[fString[i + 1].ToString()][0][0].ToString();
            }
            else if (i + 1 < fString.Length)
            {
                string addNextMoji = fString[i].ToString() + fString[i + 1].ToString();
                if (_cd.dic.ContainsKey(addNextMoji))
                {
                    r = _cd.dic[addNextMoji][0];
                }
            }

            _romSliceList.Add(r);

            if (r == "SKIP") continue;

            for (int j=0; j<r.Length; j++)
            {
                _furiCountList.Add(i);
                _romNumList.Add(j);
            }
        }
    }

    private void ReCreateRomSliceList(IReadOnlyList<string> romList)
    {
        _furiCountList.Clear();
        _romNumList.Clear();

        for (int i = 0; i < romList.Count; i++)
        {
            string r = romList[i];

            if (r == "SKIP")
            {
                continue;
            }

            for (int j = 0; j < r.Length; j++)
            {
                _furiCountList.Add(i);
                _romNumList.Add(j);
            }
        }
    }

    private void CheckIrregularType(char inputChar, string currentFuri, int furiCount, bool addSmallMoji)
    {
        if (_cd.dic.ContainsKey(currentFuri))
        {
            List<string> candidateList = _cd.dic[currentFuri];

            for (int i = 0; i < candidateList.Count; i++)
            {
                string candidateRom = candidateList[i];
                int romNum = _romNumList[_rNum];

                bool preCheck = true;

                for (int j = 0; j < romNum; j++)
                {
                    if (candidateRom[j] != _romSliceList[furiCount][j])
                    {
                        preCheck = false;
                        break;
                    }
                }

                if (preCheck && inputChar == candidateRom[romNum])
                {
                    _romSliceList[furiCount] = candidateRom;
                    _rString = string.Join("", GetRomSliceListWithoutSkip());

                    ReCreateRomSliceList(_romSliceList);

                    _isCorrect = true;

                    if (addSmallMoji) AddSmallMoji();

                    Correct();

                    break;
                }
            }
        }
    }
    
    // 柔軟な入力をしたとき、次の文字が小文字なら小文字を挿入する
    private void AddSmallMoji()
    {
        int nextMojiNum = _furiCountList[_rNum] + 1;

        if (_fString.Length-1 <= nextMojiNum) return;

        string nextMoji = _fString[nextMojiNum].ToString();
        string a = _cd.dic[nextMoji][0];

        if (a[0] != 'x' && a[0] != 'l') return;

        // romSliceListに挿入と表示の反映
        _romSliceList.Insert(nextMojiNum, a);

        // SKIPを削除する
        _romSliceList.RemoveAt(nextMojiNum + 1);

        // 変更したリストを再度表示させる
        ReCreateRomSliceList(_romSliceList);
        _rString = string.Join("", GetRomSliceListWithoutSkip());
    }
}