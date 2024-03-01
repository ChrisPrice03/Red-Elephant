using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty", fileName = "New Difficulty")]
public class DifficultySO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string description = "Enter difficulty description here";
    [SerializeField] string[] difficulty = new string[3];
    [SerializeField] int selectedDifficulty;

    public string GetDescription() {
        return description;
    }
    public int GetDifficultyIndex() {
        return selectedDifficulty;
    }
    public string GetSelectedDiff(int index) {
        return difficulty[index];
    }

}
