/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public Text nameText;
    public SpriteRenderer artworkSprite;

    private int selectedOption = 0;

    void Start()
    {
        UpdateCharacter(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if(selectedOption >= characterDB.CharacterCount)
        {
            selectedOption = 0;
        }
        
        UpdateCharacter(selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;
        
        if(selectedOption <0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }

        UpdateCharacter(selectedOption);
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = characterSprite;
        nameText.text = character.characterName;
    }
}
*/