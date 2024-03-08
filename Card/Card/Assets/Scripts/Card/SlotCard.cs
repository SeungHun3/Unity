using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using EnumTypes.InGame;

public class SlotCard : CardBase
{
	public Image MainImage;
	public TextMeshProUGUI Title_Text;
	public TextMeshProUGUI Content_Text;

	bool b_animEnd;

	private void OnEnable()
	{
		//transform.DOPunchRotation(new Vector3(0,360,0),animDuration);

	}

	public IEnumerator Open(Transform spawn, 
		Transform arrived1, float v_arrived_1, float d_arrived_1,
		Transform arrived2, float v_arrived_2, float d_arrived_2, 
		float v_scale, float d_scale)
	{
		TurnController.Instance.Target.OwnCards.Add(this);
		transform.position = spawn.position;
		transform.DOScale(0.5f, 0.2f);

		transform.DOMoveY(arrived1.position.y, v_arrived_1); // 0.5
		yield return new WaitForSecondsRealtime(d_arrived_1); // 0.5

		transform.DOMoveY(arrived2.position.y, v_arrived_2); // 0.2
		yield return new WaitForSecondsRealtime(d_arrived_2); // 0.4

		transform.DOScale(1f, v_scale); // 0.5
		yield return new WaitForSecondsRealtime(d_scale); // 0.5


	}
	public override IEnumerator Use()
	{
		transform.SetParent(PopupManager.Instance.Canvas.transform);
		b_animEnd = false;
		GetComponent<Animation>().Play("CardUse");
		while(!b_animEnd)
		{
			yield return null;
		}
		TurnController.Instance.Target.OwnCards.Remove(this);
		gameObject.SetActive(false);
	}

	public void SetCard(ESlotCard eSlotCard)
	{
		ESlotCard = eSlotCard;
		GetComponent<Animation>().Play("CardIdle");
		transform.localScale = new Vector3(0.5f, 0.4f, 1f);

		AtlasManager AtlasMgr = AtlasManager.Instance;
		Dictionary<ulong, CardInfo> cardInfo = InGameTable.Instance.CardInfo;
		MainImage.sprite = AtlasMgr.GetSymbolSkillSprite(cardInfo[(ulong)eSlotCard].IconName);
		Title_Text.text = cardInfo[(ulong)eSlotCard].SkillName;
		Content_Text.text = cardInfo[(ulong)eSlotCard].Content;
	}
	public void AnimEnd()
	{
		b_animEnd = true;
	}

}
