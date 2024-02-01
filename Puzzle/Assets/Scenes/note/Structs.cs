using System;

/*
커스텀 Struct 데이터 타입들을 모아놓을 클래스 파일을 생성한다.
*/

namespace Structs
{
	[Serializable]
	public struct AttackData
	{
		//public AttackTypes attackTypes;
		//public int attackAnimationIndex;

		//public ScriptableObjects.MeleeTrace meleeTrace;
		//public ScriptableObjects.Projectile projectile;
	}

	[Serializable]
	public struct StatModifierData
	{
		//public StatTypes statType;
		//public ModifierTypes modifierType;
		public float value;
	}

	//..
}