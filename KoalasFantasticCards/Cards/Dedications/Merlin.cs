using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using System.Linq;
using System.Collections.Generic;
using System;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnboundLib.Networking;
using System.Data;

namespace KFC.Cards
{
    public class Merlin : CustomEffectCard<MerlinChaos>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "<#ff0000><b>L<#ff8000><b>o<#ffff00><b>a<#80ff00><b>f<#00ff00><b>y<#00ff80><b>m<#00ffff><b>e<#0080ff><b>r<#0000ff><b>l<#8000ff><b>i<#ff00ff><b>n",
            Description = "What do you call a group of pandas in confusion and creating chaos?\nPanda-monium",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Merlin"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "Merlin Enhanced",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Bullets"
                },
                new CardInfoStat
                {
                    amount = "<#FF0000>R<#00FF00>G<#0000FF>B",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Bullets"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("cantEternity") };
            cardInfo.allowMultiple = false;
            gun.damageAfterDistanceMultiplier = 1.2f;
            gun.attackSpeed = 0.33f;
            gun.reloadTime = 0.33f;
            gun.numberOfProjectiles = 4;
            gun.ammo = 12;
            gun.spread = 0.15f;
            gun.gravity = 0.1f;
            gun.projectileSpeed = 1.5f;
            gun.damage = 0.0000001f;
            gun.percentageDamage = 0.01f;
            gun.reflects = 50;
            var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
            var mayhemCard = vanillaCards.Where((c) => c.cardName.ToLower() == "mayhem").ToArray()[0];
            var proj = mayhemCard.gameObject.GetComponent<Gun>().objectsToSpawn[0];
            gun.objectsToSpawn = new ObjectsToSpawn[] { proj };
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class MerlinChaos : CardEffect
    {
        public void Update()
        {
            player.data.weaponHandler.gun.damage = 0.11f;
            if (!player.data.view.IsMine) return;
            Vector2 borderSize = Camera.main.sensorSize*0.99f;
            var cursX = MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).x;
            var cursY = MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).y;
            int top = UnityEngine.Random.Range(0, 2);
            int negPos = UnityEngine.Random.Range(-1, 1);
            if (negPos == 0) negPos++;
            Vector2 pos;
            if (top == 0) pos = new Vector2(UnityEngine.Random.Range(-borderSize[0], borderSize[0]), negPos * borderSize[1]);
            else pos = new Vector2(negPos * borderSize[0], UnityEngine.Random.Range(-borderSize[1], borderSize[1]));
            NetworkingManager.RPC(typeof(MerlinChaos), nameof(RPC_PlayerForceShoot), player.playerID, pos);
        }
        [UnboundRPC]
        public static void RPC_PlayerForceShoot(int pid, Vector2 targ)
        {
            Player play = PlayerManager.instance.GetPlayerWithID(pid);
            play.gameObject.GetOrAddComponent<KoalasBonusStats>().shootPos = targ;
            var cursX = MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).x;
            var cursY = MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).y;
            play.data.weaponHandler.gun.SetFieldValue("forceShootDir", (Vector3)targ-new Vector3(cursX, cursY, 0));
        }
        public override void OnShoot(GameObject projectile)
        {
            projectile.gameObject.GetOrAddComponent<RBGullets>();
            projectile.transform.position = player.gameObject.GetComponent<KoalasBonusStats>().shootPos;
        }
        public override void OnBulletHit(GameObject projectile, HitInfo hit)
        {
            try
            {
                if (hit.collider.gameObject.GetComponent<Player>().playerID == player.playerID)
                {
                    player.data.healthHandler.Heal(player.data.weaponHandler.gun.damage*110f);
                    player.data.healthHandler.Heal(player.data.maxHealth * 0.02f);
                }
            } catch { }
        }
        public override void OnBlockProjectile(GameObject projectile, Vector3 forward, Vector3 hitPosition)
        {   
        }
    }
    public class RBGullets : MonoBehaviour
    {

        int phase = 1;
        float[] cols = new float[] { 1, 0, 0 };
        public void Update()
        {
            switch (phase)
            {
                case 1:
                    cols[1] += TimeHandler.deltaTime * 10;
                    if (cols[1] >= 1)
                    {
                        cols[1] = 1;
                        phase = 2;
                    }
                    break;
                case 2:
                    cols[0] -= TimeHandler.deltaTime * 10;
                    if (cols[0] <= 0)
                    {
                        cols[0] = 0;
                        phase = 3;
                    }
                    break;
                case 3:
                    cols[2] += TimeHandler.deltaTime * 10;
                    if (cols[2] >= 1)
                    {
                        cols[2] = 1;
                        phase = 4;
                    }
                    break;
                case 4:
                    cols[1] -= TimeHandler.deltaTime * 10;
                    if (cols[1] <= 0)
                    {
                        cols[1] = 0;
                        phase = 5;
                    }
                    break;
                case 5:
                    cols[0] += TimeHandler.deltaTime * 10;
                    if (cols[0] >= 1)
                    {
                        cols[0] = 1;
                        phase = 6;
                    }
                    break;
                case 6:
                    cols[2] -= TimeHandler.deltaTime * 10;
                    if (cols[2] <= 0)
                    {
                        cols[2] = 0;
                        phase = 1;
                    }
                    break;
            }
            foreach (var sr in gameObject.GetComponentsInChildren<TrailRenderer>())
            {
                /**
                sr.time = 0.5f;
                sr.endWidth = 1f;
                sr.startWidth = 0.1f;
                **/
                var col = new Color(1, 1, 1, 1);
                switch (phase)
                {
                    case 1: col = new Color(1, 0, 0); break;
                    case 2: col = new Color(1, 1, 0); break;
                    case 3: col = new Color(0, 1, 0); break;
                    case 4: col = new Color(0, 1, 1); break;
                    case 5: col = new Color(0, 0, 1); break;
                    case 6: col = new Color(1, 0, 1); break;
                }
                sr.startColor = col;
                sr.endColor = col;
            }
        }
    }
}