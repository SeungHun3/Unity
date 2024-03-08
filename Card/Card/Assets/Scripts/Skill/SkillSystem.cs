
using System.Collections;
using System.Collections.Generic;
using EnumTypes;
using EnumTypes.InGame;
using UnityEngine;

public class SkillSystem
{
    public CharBase Owner;
    public bool IsPlayer { get { return Owner == TurnController.Instance.Player; } }

    public SkillSystem(CharBase owner)
    {
        Owner = owner;
    }
}
public class DeckSkillSystem : SkillSystem
{

    public DeckSkillSystem(CharBase owner)
    : base(owner) { }
}

public class SymbolskillSystem : SkillSystem
{
    public List<Symbolskill> Skills = new();
    public void AddSkill(ESlotCard card)
	{
		switch (card)
		{
			case ESlotCard.None:
				Skills.Add(new Symbolskill(Owner));
				break;
			case ESlotCard.Sword:
				Skills.Add(new Sword(Owner));
				break;
			case ESlotCard.Coin:
				Skills.Add(new Coin(Owner));
				break;
			case ESlotCard.Boom:
				Skills.Add(new Boom(Owner));
				break;
			case ESlotCard.Heart:
				Skills.Add(new Heart(Owner));
				break;
			case ESlotCard.Shield:
				Skills.Add(new Shield(Owner));
				break;
			case ESlotCard.Potion:
				Skills.Add(new Potion(Owner));
				break;
			case ESlotCard.Gloves:
				Skills.Add(new Gloves(Owner));
				break;
			case ESlotCard.End:
				break;
		}
    }
    public SymbolskillSystem(CharBase owner)
    : base(owner) { }
}
