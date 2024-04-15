using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// クリックしたら別のシーンに移動するスクリプト
/// </summary>
public class ClickToStart : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string _sceneName;
    private bool isLoading = false;
    void Awake()
    {
        _button.onClick.AddListener(() =>
        { 
            if(isLoading) return;
            var asyncOperation = SceneManager.LoadSceneAsync(_sceneName);
            isLoading = true;
        });
    }
    
}
