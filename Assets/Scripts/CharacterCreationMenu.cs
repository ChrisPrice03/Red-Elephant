using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCreationMenu : MonoBehaviour
{
    public List<OutfitChanger> outfitChangers = new List<OutfitChanger>();
    public TMP_InputField characterNameInput;


    public void RandomizeCharacter()
    {
        foreach (OutfitChanger changer in outfitChangers)
        {
            changer.Randomize();
        }
    }

    public int[] Indices()
{
    List<int> fits = new List<int>(); // Initialize a list to store the indices
    
    foreach (var fit in outfitChangers)
    {
        fits.Add(fit.GetCurrentOption()); // Add each option to the list
    }
    
    return fits.ToArray(); // Convert the list to an array and return
}

}
