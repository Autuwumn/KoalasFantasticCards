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
            Rarity = RarityUtils.GetRarity("Rare"),
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
        public override void OnBulletHit(GameObject projectile, HitInfo hit)
        {
            base.OnBulletHit(projectile, hit);
            var lego = PhotonNetwork.Instantiate("KFC_LegoBrick", projectile.transform.position, Quaternion.identity);
            //lego.RPC("RPCA_ChangeSprite", lego.gameObject);
            lego.gameObject.GetComponent<DamageBox>().damage = gun.damage * 5;
            KFC.instance.ExecuteAfterSeconds(10f, () =>
            {
                PhotonNetwork.Destroy(lego);
            });
        }
        [PunRPC]
        private void RPCA_ChangeSprite()
        {
            Sprite[] colors = new[] { KFC.ArtAssets.LoadAsset<Sprite>("legoBrickRed"), KFC.ArtAssets.LoadAsset<Sprite>("legoBrickYellow"), KFC.ArtAssets.LoadAsset<Sprite>("legoBrickBlue"), KFC.ArtAssets.LoadAsset<Sprite>("legoBrickGreen") };
            Sprite rainbowLego = colors[UnityEngine.Random.Range(0, colors.Length)];
            GetComponent<SpriteRenderer>().sprite = rainbowLego;
        }
    }
}