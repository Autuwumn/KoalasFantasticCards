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
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            gun.gravity = 0;
            gun.ignoreWalls = true;
            gun.projectileSpeed = 0.1f;
            gun.destroyBulletAfter = 30f;
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class blackholegun_Mono : CardEffect
    {
        private GameObject bhole;
        protected override void Start()
        {
            base.Start();
        }
        public override void OnShoot(GameObject projectile)
        {
            if (player.data.view.IsMine)
            {
                if (bhole)
                {
                    Destroy(bhole.GetComponent<blackHole_Mono>().projec);
                    PhotonNetwork.Destroy(bhole);
                }
                bhole = PhotonNetwork.Instantiate("KFC_BlackHole", data.hand.transform.position, Quaternion.identity);
            }
            KFC.instance.ExecuteAfterFrames(5, () =>
            {
                if(GameObject.Find("BlackHole(Clone)").GetComponent<blackHole_Mono>() == null)
                {
                    GameObject.Find("BlackHole(Clone)").AddComponent<blackHole_Mono>().projec = projectile;
                }
            });
        }
        public override IEnumerator OnPointEnd(IGameModeHandler gameModeHandler)
        {
            if (player.data.view.IsMine)
            {
                if (bhole)
                {
                    Destroy(bhole.GetComponent<blackHole_Mono>().projec);
                    PhotonNetwork.Destroy(bhole);
                }
            }
            return base.OnPointEnd(gameModeHandler);
        }
    }
    public class blackHole_Mono : MonoBehaviour
    {
        private float size;
        public GameObject projec;
        public void Start()
        {
            size = 0.5f;
            gameObject.transform.GetChild(0).transform.localScale = new Vector3(size / 3, size / 3, size / 3);
            gameObject.transform.GetChild(1).transform.localScale = new Vector3(size, size, size);
        }
        public void Update()
        {
            size += TimeHandler.deltaTime;
            gameObject.transform.GetChild(0).transform.localScale = new Vector3(size / 3, size / 3, size / 3);
            gameObject.transform.GetChild(1).transform.localScale = new Vector3(size, size, size);
            gameObject.transform.GetComponentInChildren<DamageBox>().damage = size * 25f;
            if (size < 1f)
            {
                gameObject.transform.GetComponentInChildren<DamageBox>().damage = 0f;
            }
            List<Player> players = PlayerManager.instance.players;
            if (projec != null)
            {
                gameObject.transform.position = projec.transform.position;
            }
            if (players.Count == 0) return;
            foreach (var ployer in players)
            {
                var dist = Vector2.Distance((Vector2)ployer.transform.position, (Vector2)this.transform.position);
                if (dist < size * 1.5f && size > 1f)
                {
                    ployer.data.stats.gravity = -System.Math.Abs(ployer.data.stats.gravity);
                    Vector3 vecto = Vector3.MoveTowards(ployer.transform.position, gameObject.transform.position,TimeHandler.deltaTime*size*2f);
                    ployer.transform.position = vecto;
                } else
                {
                    ployer.data.stats.gravity = System.Math.Abs(ployer.data.stats.gravity);
                }
            }
        }
    }

}