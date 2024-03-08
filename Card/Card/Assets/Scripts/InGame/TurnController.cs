
using System.Collections;
using System.Collections.Generic;
using EnumTypes;
using EnumTypes.InGame;
using UnityEngine;



public delegate void HP_Delegate();
public delegate void FinishTurn_Delegate();

interface IPlayMachine
{
    void Play();
}
interface ISkill
{
    IEnumerator UseCard(int cardIndex);
}

public abstract class CharBase : IPlayMachine, ISkill
{

    public CharBase Target = null;
    public ESlotCard ESlotCard;
    public List<CardBase> OwnCards = new();
    public SymbolskillSystem OwnSymbolskills;

    #region Stat
        
    private float _maxHP;
    private float _curHP;
    public int Attack;
    public int Defence;
    public int 명중;
    public int 회피;
    public float MaxHP { get { return _maxHP; } }
    public float CurHP
    {
        set
        {
            _curHP = value;
            if (_curHP > _maxHP)
            {
                _curHP = _maxHP;
            }
            else if (_curHP <= 0)
            {
                _curHP = 0;
            }
        }
        get
        {
            return _curHP;
        }
    }
    public float NormalizedHP
    {
        get { return _curHP / _maxHP; }
    }

    #endregion

    
    #region Delegate

    public HP_Delegate HPDelegate;
    public FinishTurn_Delegate TurnEndDelegate;

    #endregion
    
    public virtual void Play()
    {
        ESlotCard = (ESlotCard)DrawManager.Instance.GetRandomCard();
    }
    public IEnumerator OpenCard()
    {
        yield return PopupManager.Instance.Inst_InGameHUD.SlotCardSpawner.OpenSlotCard(ESlotCard);
        ESlotCard = ESlotCard.None;
    }

    protected ESlotCard GetCard(int cardIndex)
    {
        if(OwnCards[cardIndex])
        {
            return OwnCards[cardIndex].ESlotCard;
        }
        return ESlotCard.None;
    }

    public virtual IEnumerator UseCard(int cardIndex)
	{
		ESlotCard card = GetCard(cardIndex);
		PopupManager.Instance.Inst_InGameHUD.HistorySpawner.AddCard(card);
		yield return OwnCards[cardIndex].Use();
	}


	public virtual int ApplyDamage(int damage)
    {
        damage -= Defence;
        CurHP = _curHP - damage;
        HPDelegate();

        return damage;
    }


    public CharBase(float hp, int attack, int defence)
    {
        _maxHP = hp;
        _curHP = _maxHP;

        Attack = attack;
        Defence = defence;
        ESlotCard = ESlotCard.None;
    }

}

public class Player : CharBase
{
    public int Coin;
    public override void Play()
    {
        base.Play();
        Handle.Instance.bInput = true;

        Debug.Log("<color=#00F0FF>Player</color>");
    }


    public override IEnumerator UseCard(int cardIndex)
    {
        ESlotCard card = GetCard(cardIndex);
		yield return base.UseCard(cardIndex);

        OwnSymbolskills.AddSkill(card);
		yield return OwnSymbolskills.Skills[0].Use();
        InGameManager.Instance.Mon_HitFxSpawner.SpawnFX();
    }

	public override int ApplyDamage(int damage)
    {
        int Damage = base.ApplyDamage(damage);

        PopupManager.Instance.Inst_InGameHUD.Player_HitFxSpawner.SpawnFX();
        PopupManager.Instance.Inst_InGameHUD.Player_HPTextSpawner.SpawnText(Damage, ETextType.Damage);

        return Damage;
    }


    public Player(float hp, int attack, int defence) : base(hp, attack, defence)
    {
        Coin = 0;
        OwnSymbolskills = new SymbolskillSystem(this);

        Debug.Log("Player : HP= " + MaxHP + ", Attack= " + Attack + ", Defence= " + defence + ", Coin= " + Coin);
    }
}

public class Monster : CharBase
{
    public GameObject Obj = null;
    public bool bIsEndCreate;

    public override void Play()
    {
        base.Play();
        Handle.Instance.Click();
        SlotMachine.Instance.Play();
        Debug.Log("<color=#FF5326>Monster</color>");
    }

	public override IEnumerator UseCard(int cardIndex)
	{
		ESlotCard card = GetCard(cardIndex);
		yield return base.UseCard(cardIndex);

		OwnSymbolskills.AddSkill(card);

		TriggerMotion(EMotion.Attack);
		yield return OwnSymbolskills.Skills[0].Use();
    }

	public override int ApplyDamage(int damage)
    {

        // motion
        TriggerMotion(EMotion.Hit);
        int Damage = base.ApplyDamage(damage);

        InGameManager.Instance.Mon_HitTextSpawner.SpawnText(Damage, ETextType.Damage);

        if (CurHP == 0)
        {
            TriggerMotion(EMotion.Dead);
        }
        return Damage;
    }

    public void TriggerMotion(EMotion eMotion)
    {
        string motion = string.Empty;
        switch (eMotion)
        {
            case EMotion.Arrival:
                motion = "Arrival";
                break;
            case EMotion.Hit:
                motion = "Hit";
                break;
            case EMotion.Attack:
                motion = "Attack";
                break;
            case EMotion.Dead:
                motion = "Dead";
                break;
        }
        InGameManager.Instance.Monster_Obj.GetComponent<Animator>().SetTrigger(motion);
    }
    

    public Monster(int hp, int attack, int defence) : base(hp, attack, defence)
    {
        Obj = InGameManager.Instance.Monster_Obj;
        OwnSymbolskills = new SymbolskillSystem(this);

        Debug.Log("Monster : HP= " + MaxHP + ", Attack= " + Attack + ", Defence= " + defence);
    }

}

public class TurnController : SingleDestroy<TurnController>
{
    private CharBase _target = null;
    private Player _player = null;
    private Monster _monster = null;

    public CharBase Target => _target;
    public Player Player => _player;
    public Monster Monster => _monster;
    public bool IsPlayerTurn { get { return _target == _player; } }

    public void Init()
    {
        InGameManager inGameManager = InGameManager.Instance;
        _player = new Player(inGameManager.Player_HP, inGameManager.Player_Attack, inGameManager.Player_Defence);
        _monster = new Monster(inGameManager.Monster_HP, inGameManager.Monster_Attack, inGameManager.Monster_Defence);
        _player.Target = _monster;
        _monster.Target = _player;
        int checkTurn = Random.Range(0, 2); // 0 ~ 1
        bool isPlayer = checkTurn == 0;
        _target = isPlayer ? Player : Monster;
    }


    public IEnumerator StartGame()
    {
        SlotMachine.Instance.TurnChange();
        SlotMachine.Instance.ShutterAnim();
        yield return new WaitForSecondsRealtime(1f);
        yield return PopupManager.Instance.Inst_Turn.Open();
        if (!IsPlayerTurn)
        {
            yield return new WaitForSecondsRealtime(InGameManager.Instance.MonsterPlayDelay);
        }
        _target.Play();
    }

    public IEnumerator StartTurn()
    {
        yield return new WaitForSecondsRealtime(InGameManager.Instance.TurnDelay);
        SlotMachine.Instance.TurnChange();
        SlotMachine.Instance.ShutterAnim();
        yield return PopupManager.Instance.Inst_Turn.Open();
        if (!IsPlayerTurn)
        {
            yield return new WaitForSecondsRealtime(InGameManager.Instance.MonsterPlayDelay);
        }

        _target.Play();
    }

    public void EndTurnCoroutine()
    {
        StartCoroutine(EndTurn());
    }
    public IEnumerator EndTurn()
    {
        yield return _target.OpenCard();
        if(_target.OwnCards.Count != 0)
		{
            yield return _target.UseCard(0);
        }

        _target = IsPlayerTurn ? Monster : Player;
        if (_target.CurHP <= 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            InGameManager.Instance.GameEnd();
            yield break;
        }

        StartCoroutine(StartTurn());

    }

}