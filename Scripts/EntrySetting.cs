using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dan.Models;

public class EntrySetting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rankText, _usernameText, _scoreText;

    public void SetEntry(Entry entry)
    {
        _rankText.text = entry.RankSuffix();
        _usernameText.text = entry.Username;
        _scoreText.text = entry.Score.ToString();

        GetComponent<Image>().color = entry.IsMine() ? Color.yellow : Color.white;
    }
}
