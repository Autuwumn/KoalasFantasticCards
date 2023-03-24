using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnboundLib.GameModes;
using WillsWackyManagers.Utils;

namespace KFC.Cards
{
    public class LaggyBullets : CustomEffectCard<laggyMono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Laggy Bullets",
            Description = "Wait im lagging",
            ModName = KFC.CurseInt,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Laggy"),
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.EvilPurple
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            cardInfo.categories = new CardCategory[] { CurseManager.instance.curseCategory };
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class laggyMono : CardEffect
    {
        public override void OnShoot(GameObject projectile)
        {
            var lagTime = 0f;
            foreach(var c in player.data.currentCards)
            {
                if(c == LaggyBullets.card)
                {
                    lagTime++;
                }
            }   
            projectile.SetActive(false);
            KFC.instance.ExecuteAfterSeconds(lagTime, () =>
            {
                projectile.SetActive(true);
            });
        }
    }
}