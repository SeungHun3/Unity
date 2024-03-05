using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnumTypes;

public class TextSpawner : Spawner<TextBase>
{
    public void SpawnText(int value, ETextType eTextType)
    {
        TextBase textObj = Spawn();
        switch(eTextType)
        {
            case ETextType.Damage:
            textObj.SetColor = Color.red;
            break;

            case ETextType.Heal:
            textObj.SetColor = Color.green;
            break;

        }

        textObj.SetText = value.ToString();
    }


}
