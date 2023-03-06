using UnityEngine;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using System;
using ClassesManagerReborn.Util;
using System.Collections;
using UnboundLib.GameModes;

namespace KFC.Cards 
{
    public class ViruzCard : CustomEffectCard<FuckMe>
    {
        internal static CardInfo card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = ViruzClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title = "Viruz",
            Description = "p = (∫a∫b(t^1.15-2t^3/4)dydy(sub)2)/(100/c)+1\nYou only get 15 increments, so choose wisely",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Viruz"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.MagicPink,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "<#FFFF00>p*",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "stats every point"
                },
                new CardInfoStat
                {
                    amount = "+1",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "a"
                },
                new CardInfoStat
                {
                    amount = "+1",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "b"
                },
                new CardInfoStat
                {
                    amount = "+1",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "c"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
        }
    }
}
namespace KFC.MonoBehaviors
{
    public class FuckMe : CardEffect
    {
        public float t = 0;
        public float a = 1;
        public float b = 1;
        public float c = 1;
        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            t++;
            a = 1;
            b = 1;
            c = 1;
            foreach(var card in player.data.currentCards)
            {
                if(card.cardName.ToLower().Equals("increase a"))
                {
                    a++;
                }
                if(card.cardName.ToLower().Equals("increase b"))
                {
                    b++;
                }
                if(card.cardName.ToLower().Equals("increase c"))
                {
                    c++;
                }
            }
            //var mult = (((a * Math.Pow(t, 1.15 / 1.1) - (3 * Math.Pow(t, 1.2) / 6 * b) + c * t) / (100 / c)/6))+1;
            var mult = (float)(((Math.Pow(a * t, 1.2)+(3*b*t)+(c*t)/4)/(100/(c/8))-b/3)/6)+1;
            if(mult < 0.2f)
            {
                mult = 0.2f;
            }
            gun.damage *= mult;
            gun.attackSpeed /= mult;
            gunAmmo.reloadTimeMultiplier /= mult;
            gun.projectileSpeed *= mult;
            player.data.maxHealth *= mult;
            characterStats.movementSpeed *= mult;
            block.cdMultiplier /= mult;
            yield break;
        }
    }
}