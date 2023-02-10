using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using Photon.Pun;

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
            Theme = CardThemeColor.CardThemeColorType.FirepowerYellow
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
        }
    }
}

namespace KFC.MonoBehaviors
{
    [DisallowMultipleComponent]
    public class legos_Mono : CardEffect
    {
        public override void OnBulletHit(GameObject projectile, HitInfo hit)
        {
            base.OnBulletHit(projectile, hit);
            var other = hit.rigidbody.gameObject;
            if (other.name == "Bullet_Base(Clone)") return;
            var lego = PhotonNetwork.Instantiate("KFC_LegoBrick", projectile.transform.position, Quaternion.identity);
            lego.GetComponent<DamageBox>().damage = gun.damage*27.5f;
        }
    }
}