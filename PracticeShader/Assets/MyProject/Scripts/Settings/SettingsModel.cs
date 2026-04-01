using System;
using UnityEngine;
using R3;

public class SettingsModel
{
    private const string SAVE_KEY = "_GAMECONFIG_";

    public ReactiveProperty<int> RainVolume { get; } = new();
    public ReactiveProperty<int> KeyboardVolume { get; } = new();
    public ReactiveProperty<int> MusicVolume { get; } = new();
    public ReactiveProperty<KeyboardSE.Type> SelectedKeyboardType { get; } = new();
    public ReactiveProperty<ThemeCatalog.Type> SelectedTheme { get; } = new();

    public ReactiveProperty<bool> IsMenuOpen { get; } = new();

    public SettingsModel()
    {
        Load();
    }

    public void Save()
    {
        var config = new GameConfig
        {
            RainVolume = RainVolume.Value,
            KeyboardVolume = KeyboardVolume.Value,
            MusicVolume = MusicVolume.Value,
            SelectedKeyboardType = SelectedKeyboardType.Value,
            SelectedTheme = SelectedTheme.Value
        };

        string json = JsonUtility.ToJson(config);

        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
        
        Debug.Log($"セーブ完了: {json}");
    }

    private void Load()
    {
        GameConfig config;
        // セーブデータが存在するかチェック
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            
            config = JsonUtility.FromJson<GameConfig>(json);
        }
        else
        {
            // デフォルト設定を作成
            config = new GameConfig();
        }

        RainVolume.Value = config.RainVolume;
        KeyboardVolume.Value = config.KeyboardVolume;
        MusicVolume.Value = config.MusicVolume;
        SelectedKeyboardType.Value = config.SelectedKeyboardType;
        SelectedTheme.Value = config.SelectedTheme;
    }

    /// <summary>
    /// メニューの開閉をトグルするリクエスト
    /// メニューが閉じられたときにはセーブを呼び出す
    /// </summary>
    public void RequestToggleMenu()
    {
        IsMenuOpen.Value = !IsMenuOpen.Value;

        if (!IsMenuOpen.Value) Save();
    }

    /// <summary>
    /// テーマを変更するリクエスト
    /// </summary>
    /// <param name="direction"></param>
    public void RequestShiftTheme(int direction)
    {
        int themeCount = Enum.GetValues(typeof(ThemeCatalog.Type)).Length;
        int newThemeIndex = ((int)SelectedTheme.Value + direction + themeCount) % themeCount;
        SelectedTheme.Value = (ThemeCatalog.Type)newThemeIndex;
    }
}