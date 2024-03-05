using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes.InGame;

public class CardBase : MonoBehaviour
{
	public ESlotCard ESlotCard;
	public virtual IEnumerator Use()
	{
		yield break;
	}
}
