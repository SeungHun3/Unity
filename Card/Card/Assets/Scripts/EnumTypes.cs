using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EnumTypes
{

    public enum SceneType
    {
        None,
        Splash,
        InGame,

        End,
    }
    public enum ETextType { Damage, Heal }

    namespace InGame
    {
        public enum ESlotCard
        {
            None = 1,
            Sword,
            Coin,
            Boom,
            Heart,
            Shield,
            Potion,
            Gloves,

            End,
        }
        public enum ESkillType
        {
            None,
            Attack,
            Buff,
        }

        public enum EMotion
        {
            None,
            Arrival,
            Hit,
            Attack,
            Dead,


            End,
        }
        public enum Suit
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        }

        public enum Rank
        {
            Two = 2,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }
    }

    namespace Lobby
    {
    }

}
