using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using EnumTypes.InGame;


public class Symbolskill
{
    private int _turn;
    public CharBase Owner;
    public bool IsPlayer { get { return Owner == TurnController.Instance.Player; } }
    public virtual IEnumerator Use()
    {
        Owner.OwnSymbolskills.Skills.Remove(this);
        yield break;
    }
    protected void RemoveSkill()
	{
        Owner.OwnSymbolskills.Skills.Remove(this);
    }
    public Symbolskill(CharBase owner)
	{
        Owner = owner;
    }
}

public class Sword : Symbolskill
{
	public override IEnumerator Use()
    {
		int damage = Owner.Attack * 2;
        Owner.Target.ApplyDamage(damage);
        Owner.OwnSymbolskills.Skills.Remove(this);
        yield break;
    }
    public Sword(CharBase owner) : base(owner)	{	}
}
public class Coin : Symbolskill
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
public class Boom : Symbolskill
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

public class Heart : Symbolskill
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


public class Shield : Symbolskill
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

public class Potion : Symbolskill
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


public class Gloves : Symbolskill
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