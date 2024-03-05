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



	private void OnEnable()
	{
		//transform.DOPunchRotation(new Vector3(0,360,0),animDuration);

	}

	public IEnumerator Open(Transform spawn, Transform arrived1, Transform arrived2)
	{
		TurnController.Instance.Target.OwnCards.Add(this);
		transform.position = spawn.position;

		transform.DOScale(0.5f, 0.2f);
		transform.DOMoveY(arrived1.position.y, 0.5f);
		yield return new WaitForSecondsRealtime(0.5f);

		transform.DOMoveY(arrived2.position.y, 0.2f);
		yield return new WaitForSecondsRealtime(0.4f);

		transform.DOScale(1f, 0.5f);
		transform.DOMoveY(transform.position.y, 0.5f);
		yield return new WaitForSecondsRealtime(0.5f);

		//yield return Use();

	}
	public override IEnumerator Use()
	{
		transform.SetParent(PopupManager.Instance.Canvas.transform);
		GetComponent<Animation>().Play("CardUse");
		yield return new WaitForSecondsRealtime(1f);
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

		MainImage.sprite = AtlasMgr.GetSlotSkillSprite(cardInfo[(ulong)eSlotCard].IconName);
		Title_Text.text = cardInfo[(ulong)eSlotCard].SkillName;
		Content_Text.text = cardInfo[(ulong)eSlotCard].Content;
	}


}
