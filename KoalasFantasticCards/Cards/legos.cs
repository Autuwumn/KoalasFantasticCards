using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using Photon.Pun;
using System.Linq;

namespace KFC.Cards
{
    public class legos : CustomEffectCard<legos_Mono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "A bunch of legos",
            Description = "Everybody hates you",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Legos"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.FirepowerYellow,
            OwnerOnly = true
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class legos_Mono : CardEffect
    {
        private int leegos = 0;
        public override void OnBulletHit(GameObject projectile, HitInfo hit)
        {
            base.OnBulletHit(projectile, hit);
            if (leegos < 10)
            {
                leegos++;
                var theLego = new[] { "KFC_LegoBrickR", "KFC_LegoBrickY", "KFC_LegoBrickB", "KFC_LegoBrickG" };
                var brick = theLego[UnityEngine.Random.Range(0, theLego.Length)];
                var lego = PhotonNetwork.Instantiate(brick, projectile.transform.position, Quaternion.identity);
                lego.gameObject.GetComponent<DamageBox>().damage = gun.damage * 5;
                KFC.instance.ExecuteAfterSeconds(10f, () =>
                {
                    leegos--;
                    PhotonNetwork.Destroy(lego);
                });
            }
        }
    }
}