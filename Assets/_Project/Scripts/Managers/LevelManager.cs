using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int levelNo;

    public List<Level> levels;

    public Level _curLevel;
    internal void RestartLevelManager()
    {
        DeleteCurrentLevel();
        CreateNewLevel();
    }

    private void CreateNewLevel()
    {
        var newLevel = Instantiate(levels[levelNo - 1]);
        newLevel.transform.position = Vector3.zero;
        _curLevel = newLevel;
    }

    private void DeleteCurrentLevel()
    {
        if (_curLevel != null)
        {
            Destroy(_curLevel.gameObject);
        }
    }
}
