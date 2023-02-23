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
            Description = "p = (∫a∫b(t^1.15-2t^3/4)dydy2)/(100/c)+1",
            ModName = KFC.ModInitials,
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
                    stat = "Random Stat every Point"
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
        public int t = 0;
        public int a = 1;
        public int b = 1;
        public int c = 1;
        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            t++;
            t++;
            a = 1;
            b = 1;
            c = 1;
            foreach(var card in player.data.currentCards)
            {
                if(card.name.ToLower() == "a++;")
                {
                    a++;
                }
                if(card.name.ToLower() == "b++;")
                {
                    b++;
                }
                if(card.name.ToLower() == "c++;")
                {
                    c++;
                }
            }
            var mult = (((float)(a * Math.Pow(t, 1.15 / 1.1) - (3 * Math.Pow(t, 1.2) / 6 * b) + c * t) / (100 / c)/6))+1;
            if(mult < 0.2f)
            {
                mult = 0.2f;
            }
            gun.damage *= mult;
            gun.attackSpeed /= mult;
            gunAmmo.reloadTimeMultiplier /= mult;
            gun.projectileSpeed *= mult;
            characterStats.health *= mult;
            characterStats.movementSpeed *= mult;
            block.cdMultiplier /= mult;
            print(mult);
            yield break;
        }
    }
}