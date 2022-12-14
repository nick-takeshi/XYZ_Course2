using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Defs/LocalDef", fileName = "LocalDef")]
public class LocalDef : ScriptableObject
{

    [SerializeField] private string _url;
    [SerializeField] private List<LocalItem> _localItems;

    private UnityWebRequest _request;

    public Dictionary<string, string> GetData()
    {
        var dictionary = new Dictionary<string, string>();

        foreach (var localItem in _localItems)
        {
            dictionary.Add(localItem.Key, localItem.Value);
        }

        return dictionary;
    }

    [ContextMenu("Update locale")]
    public void UpdateLocale()
    {
        if (_request != null) return;

        _request = UnityWebRequest.Get(_url);

        _request.SendWebRequest().completed += OnDataLoaded;
    }
    private void OnDataLoaded(AsyncOperation operation)
    {
        if (operation.isDone)
        {
            var rows = _request.downloadHandler.text.Split('\n');
            _localItems.Clear();
            foreach (var row in rows)
            {
                AddLocaleItem(row);
            }
        }
    }

    [ContextMenu("Update Locale From File")]
    public void UpdateLocaleFromFile()
    {
        var path = UnityEditor.EditorUtility.OpenFilePanel("Choose Locace File", "", "tsv");

        if (path.Length != 0)
        {
            var data = File.ReadAllText(path);
            ParseData(data);
        }
    }

    private void ParseData(string data)
    {
        var rows = data.Split('\n');
        _localItems.Clear();
        foreach (var row in rows)
        {
            AddLocaleItem(row);
        }
    }

    private void AddLocaleItem(string row)
    {
        try
        {
            var parts = row.Split('\t');
            _localItems.Add(new LocalItem { Key = parts[0], Value = parts[1] });
        }
        catch (Exception e)
        {
            Debug.LogError($"Can't parse row: {row}. \n {e}");
        }
    }

    [Serializable]
    private class LocalItem
    {
        public string Key;
        public string Value;
    }
}
