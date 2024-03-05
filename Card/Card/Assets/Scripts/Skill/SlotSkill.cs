using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using EnumTypes.InGame;


public class SlotSkillPlayer : SlotSkillSystem
{
    public override IEnumerator Use(ESlotCard eSlotSkill)
    {
        int damage = Owner.Attack;

        switch (eSlotSkill)
        {
            case ESlotCard.None:
                {
                    yield break;
                }
            case ESlotCard.Sword:
                {
                    damage = Owner.Attack * 2;
                    break;
                }
            case ESlotCard.Coin:
                {
                    PopupManager.Instance.Inst_InGameHUD.CoinSpawner.SpawnFX();
                    if (IsPlayer)
                    {
                        Player player = Owner as Player;
                        player.Coin += 1000;
                        yield return PopupManager.Instance.Inst_InGameHUD.UpdateCoin();
                    }
                    else
                    {
                        yield return new WaitForSecondsRealtime(2f);
                    }
                    break;
                }
            case ESlotCard.Boom:
                {
                    damage = (int)(Owner.Target.CurHP * 0.3);
                    break;
                }
            case ESlotCard.Heart:
                {
                    float preHP = Owner.CurHP;
                    Owner.CurHP *= 1.1f;
                    Owner.HPDelegate();

                    TextSpawner Text = IsPlayer ? PopupManager.Instance.Inst_InGameHUD.Player_HPTextSpawner : InGameManager.Instance.Mon_HitTextSpawner;
                    Text.SpawnText((int)(Owner.CurHP - preHP), ETextType.Heal);
                    break;
                }
            case ESlotCard.Shield:
                {
                    Owner.Defence = (int)(Owner.Defence * 1.2);

                    InGameHUD inGameHUD = PopupManager.Instance.Inst_InGameHUD;
                    StatHUD HUD = IsPlayer ? inGameHUD.PlayerHUD : inGameHUD.EnemyHUD;
                    HUD.SetDefence(Owner.Defence);
                    break;
                }
            case ESlotCard.Potion:
                {
                    float preHP = Owner.CurHP;
                    Owner.CurHP *= 1.1f;
                    Owner.HPDelegate();

                    TextSpawner Text = IsPlayer ? PopupManager.Instance.Inst_InGameHUD.Player_HPTextSpawner : InGameManager.Instance.Mon_HitTextSpawner;
                    Text.SpawnText((int)(Owner.CurHP - preHP), ETextType.Heal);
                    break;
                }
            case ESlotCard.Gloves:
                {
                    Owner.Attack = (int)(Owner.Attack * 1.2);

                    InGameHUD inGameHUD = PopupManager.Instance.Inst_InGameHUD;
                    StatHUD HUD = IsPlayer ? inGameHUD.PlayerHUD : inGameHUD.EnemyHUD;
                    HUD.SetAttack(Owner.Attack);
                    break;
                }
        }
        Owner.Target.ApplyDamage(damage);
    }


    public SlotSkillPlayer(CharBase owner)
    : base(owner) { }
}

public class SlotSkill
{
    private int _turn;
    public CharBase Owner;
    public bool IsPlayer { get { return Owner == TurnController.Instance.Player; } }
    public virtual IEnumerator Use()
    {
        yield break;
    }
    protected void RemoveSkill()
	{
        Owner.OwnSlotSkills.Skills.Remove(this);
    }
    public SlotSkill(CharBase owner)
	{
        Owner = owner;
    }
}

public class Sword : SlotSkill
{
	public override IEnumerator Use()
    {
		int damage = Owner.Attack * 2;
        Owner.Target.ApplyDamage(damage);
        Owner.OwnSlotSkills.Skills.Remove(this);
        yield break;
    }
    public Sword(CharBase owner) : base(owner)	{	}
}
public class Coin : SlotSkill
{
    public override IEnumerator Use()
    {
        int damage = Owner.Attack;
        PopupManager.Instance.Inst_InGameHUD.CoinSpawner.SpawnFX();
        if (IsPlayer)
        {
            Player player = Owner as Player;
            player.Coin += 1000;
            yield return PopupManager.Instance.Inst_InGameHUD.UpdateCoin();
        }
        else
        {
            yield return new WaitForSecondsRealtime(2f);
        }

        Owner.Target.ApplyDamage(damage);
        RemoveSkill();
    }
    public Coin(CharBase owner) : base(owner) { }
}
public class Boom : SlotSkill
{
	public override IEnumerator Use()
	{
		int damage = Owner.Attack;
		damage = (int)(Owner.Target.CurHP * 0.3);

        Owner.Target.ApplyDamage(damage);
        RemoveSkill();
        yield break;
	}
	public Boom(CharBase owner) : base(owner) { }
}

public class Heart : SlotSkill
{
    public override IEnumerator Use()
    {
        float preHP = Owner.CurHP;
        Owner.CurHP *= 1.1f;
        Owner.HPDelegate();

        TextSpawner Text = IsPlayer ? PopupManager.Instance.Inst_InGameHUD.Player_HPTextSpawner : InGameManager.Instance.Mon_HitTextSpawner;
        Text.SpawnText((int)(Owner.CurHP - preHP), ETextType.Heal);

        Owner.Target.ApplyDamage(Owner.Attack);
        RemoveSkill();
        yield break;
    }
    public Heart(CharBase owner) : base(owner) { }
}


public class Shield : SlotSkill
{
    public override IEnumerator Use()
    {
        Owner.Defence = (int)(Owner.Defence * 1.2);
        InGameHUD inGameHUD = PopupManager.Instance.Inst_InGameHUD;
        StatHUD HUD = IsPlayer ? inGameHUD.PlayerHUD : inGameHUD.EnemyHUD;
        HUD.SetDefence(Owner.Defence);

        Owner.Target.ApplyDamage(Owner.Attack);
        RemoveSkill();
        yield break;
    }
    public Shield(CharBase owner) : base(owner) { }
}

public class Potion : SlotSkill
{
    public override IEnumerator Use()
    {
        float preHP = Owner.CurHP;
        Owner.CurHP *= 1.1f;
        Owner.HPDelegate();

        TextSpawner Text = IsPlayer ? PopupManager.Instance.Inst_InGameHUD.Player_HPTextSpawner : InGameManager.Instance.Mon_HitTextSpawner;
        Text.SpawnText((int)(Owner.CurHP - preHP), ETextType.Heal);

        Owner.Target.ApplyDamage(Owner.Attack);
        RemoveSkill();
        yield break;
	}
	public Potion(CharBase owner) : base(owner) { }
}


public class Gloves : SlotSkill
{
	public override IEnumerator Use()
	{
		Owner.Attack = (int)(Owner.Attack * 1.2);
		InGameHUD inGameHUD = PopupManager.Instance.Inst_InGameHUD;
		StatHUD HUD = IsPlayer ? inGameHUD.PlayerHUD : inGameHUD.EnemyHUD;
		HUD.SetAttack(Owner.Attack);

        Owner.Target.ApplyDamage(Owner.Attack);
        RemoveSkill();
        yield break;
    }
    public Gloves(CharBase owner) : base(owner) { }
}