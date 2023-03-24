using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus; 
using UnboundLib;
using KFC.MonoBehaviors;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace KFC.Cards
{
    class splinter : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title       = "Splinter",
            Description = "Shoot unstoppable iron rods with massive amounts of strength",
            ModName     = KFC.ModInitials,
            Art         = KFC.ArtAssets.LoadAsset<GameObject>("C_Splinter"),
            Rarity      = RarityUtils.GetRarity("Legendary"),
            Theme       = CardThemeColor.CardThemeColorType.TechWhite,
            Stats = new []
            {
                new CardInfoStat
                {
                    amount = "+900%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Damage"
                },
                new CardInfoStat
                {
                    amount = "+900%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Bullet Speed"
                },
                new CardInfoStat
                {
                    amount = "+5s",
                    positive = false,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Reload Time"
                },
                new CardInfoStat
                {
                    amount = "-999",
                    positive = false,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Ammo"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            gun.damage = 10f;
            gun.projectileSpeed = 10f;
            gun.unblockable = true;
            gun.ammo = -999;
            gun.reloadTimeAdd = 5f;
        }
    }
}
namespace KFC.MonoBehaviors
{
    public class byebyeWall : MonoBehaviour
    {
        
    }
}