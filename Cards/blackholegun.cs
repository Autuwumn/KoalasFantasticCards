using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using Photon.Pun;
using System.Collections.Generic;

namespace KFC.Cards
{
    public class blackholegun : CustomEffectCard<blackholegun_Mono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Blackhole Gun",
            Description = "Bro stole it from stickfight\n:skull:",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Blackhole"),
            Rarity = RarityUtils.GetRarity("Mythical"),
            Theme = CardThemeColor.CardThemeColorType.TechWhite
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            gun.gravity = 0;
            gun.ignoreWalls = true;
            gun.projectileSpeed = 0.3f;
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class blackholegun_Mono : CardEffect
    {
        protected override void Start()
        {
            base.Start();
        }
        public override void OnShoot(GameObject projectile)
        {
            var bhole = PhotonNetwork.Instantiate("KFC_BlackHole", data.hand.transform.position, Quaternion.identity);
            bhole.AddComponent<blackHole_Mono>();
            bhole.GetComponent<Rigidbody2D>().velocity = projectile.GetComponent<Rigidbody2D>().velocity*5;
        }
    }
    public class blackHole_Mono : MonoBehaviour
    {
        private float size;
        public void Start()
        {
            size = 0.5f;

        }
        public void Update()
        {
            size += TimeHandler.deltaTime / 10f;
            gameObject.transform.GetChild(0).transform.localScale = new Vector3(size, size, size);
            List<Player> players = PlayerManager.instance.players;
            foreach(var ployer in players)
            {
                var dist = Vector2.Distance(ployer.transform.position, gameObject.transform.position);
                if (dist < size * 2f)
                {
                    Vector2 velo = (Vector2)ployer.data.playerVel.GetFieldValue("velocity");
                    Vector2 vecto = (gameObject.transform.position.normalized-ployer.transform.position.normalized)*size;
                    velo -= vecto;
                    UnityEngine.Debug.Log(ployer.data.playerVel.GetFieldValue("velocity") + "," + velo + "," + vecto);
                }
            }
        }
    }

}