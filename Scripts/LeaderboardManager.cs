using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

using Dan.Main;
using Dan.Models;
using Dan.Demo;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Leaderboard Essentials:")]
    [SerializeField] private TMP_InputField _playerUsernameInput;
    [SerializeField] private Transform _entryDisplayParent;
    [SerializeField] private EntrySetting _entryDisplayPrefab;
    [SerializeField] private CanvasGroup _leaderboardLoadingPanel;

    [Header("Search Query Essentials:")]
    [SerializeField] private TMP_InputField _pageInput, _entriesToTakeInput;
    [SerializeField] private int _defaultPageNumber = 1, _defaultEntriesToTake = 100;

    public int _playerScore;
    [SerializeField]
    private string _mainMenu;

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        var pageNumber = int.TryParse(_pageInput.text, out var pageValue) ? pageValue : _defaultPageNumber;
        pageNumber = Mathf.Max(1, pageNumber);
        _pageInput.text = pageNumber.ToString();

        var take = int.TryParse(_entriesToTakeInput.text, out var takeValue) ? takeValue : _defaultEntriesToTake;
        take = Mathf.Clamp(take, 1, 100);
        _entriesToTakeInput.text = take.ToString();

        var searchQuery = new LeaderboardSearchQuery
        {
            Skip = (pageNumber - 1) * take,
            Take = take
        };

        _pageInput.image.color = Color.white;
        _entriesToTakeInput.image.color = Color.white;

        Leaderboards.IceScaleLeaderboard.GetEntries(searchQuery, OnLeaderboardLoaded, ErrorCallback);
        ToggleLoadingPanel(true);
    }

    private void OnLeaderboardLoaded(Entry[] entries)
    {
        foreach (Transform t in _entryDisplayParent)
            Destroy(t.gameObject);

        foreach (var t in entries)
            CreateEntryDisplay(t);

        ToggleLoadingPanel(false);
    }

    private void CreateEntryDisplay(Entry entry)
    {
        var entryDisplay = Instantiate(_entryDisplayPrefab.gameObject, _entryDisplayParent);
        entryDisplay.GetComponent<EntrySetting>().SetEntry(entry);
    }

    private void ToggleLoadingPanel(bool isOn)
    {
        _leaderboardLoadingPanel.alpha = isOn ? 1f : 0f;
        _leaderboardLoadingPanel.interactable = isOn;
        _leaderboardLoadingPanel.blocksRaycasts = isOn;
    }

    private void Callback(bool success)
    {
        if (success)
            Load();
    }

    private void ErrorCallback(string error)
    {
        Debug.LogError(error);
    }

    public void Submit()
    {
        Leaderboards.IceScaleLeaderboard.UploadNewEntry(_playerUsernameInput.text, _playerScore, Callback, ErrorCallback);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(_mainMenu);
    }
}
