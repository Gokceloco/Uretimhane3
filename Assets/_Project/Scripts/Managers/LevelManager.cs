using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels;

    public Level _curLevel;
    internal void RestartLevelManager()
    {
        DeleteCurrentLevel();

        var newLevel = Instantiate(levels[0]);
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
