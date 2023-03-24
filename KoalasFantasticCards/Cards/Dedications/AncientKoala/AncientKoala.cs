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
using UnboundLib.Networking;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ModdingUtils.Extensions;
using KFC.ArtSchenanigans;
using CardThemeLib;

namespace KFC.Cards
{
    public class AncientKoala : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Ancient Koala",
            Description = "<#ff00ff>Only the first gains a fragment of true power",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_KoalaMain"),
            Rarity = RarityUtils.GetRarity("Divine"),
            Theme =     CardThemeLib.CardThemeLib.instance.CreateOrGetType("Koality"),
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            try
            {
                for(int i = 0; i < this.transform.childCount; i++)
                {
                    var c = this.transform.GetChild(i);
                    if(c.name.Remove(9).ToLower() == "koalamain")
                    {
                        Destroy(this.transform.GetChild(i).gameObject);
                    }
                }
                var obj = KFC.ArtAssets.LoadAsset<GameObject>("KoalaMain");
                obj.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "MostFront";
                obj.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
                obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "MostFront";
                obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 12;
                obj.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "MostFront";
                obj.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 13;
                obj.transform.GetChild(2).GetComponent<SpriteRenderer>().sortingLayerName = "MostFront";
                obj.transform.GetChild(2).GetComponent<SpriteRenderer>().sortingOrder = 14;
                obj.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "MostFront";
                obj.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 15;
                obj.transform.GetChild(3).GetComponent<SpriteRenderer>().sortingLayerName = "MostFront";
                obj.transform.GetChild(3).GetComponent<SpriteRenderer>().sortingOrder = 13;
                obj.gameObject.layer = 21;
                for (var i = 0; i < obj.transform.childCount; i++)
                {
                    obj.transform.GetChild(i).gameObject.layer = 21;
                }
                Instantiate(obj, this.transform);
                obj.transform.position = Vector3.zero;
            } catch { }
            cardInfo.GetAdditionalData().canBeReassigned = false;
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("cantEternity"), CustomCardCategories.instance.CardCategory("CardManipulation") };
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (player.data.view.IsMine)
            {
                KFC.instance.ExecuteAfterFrames(10, () =>
                {
                    var angered = false;
                    foreach (var p in PlayerManager.instance.players.Where((pm) => pm.playerID != player.playerID).ToArray())
                    {
                        foreach (var c in p.data.currentCards)
                        {
                            if (c == card) angered = true;
                        }
                    }
                    if (angered)
                    {
                        ModdingUtils.Utils.Cards.instance.RemoveAllCardsFromPlayer(player);
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, KoalaWrath.card, false, ":(", 0, 0);
                    }
                    else
                    {
                        var gifts = new[] { KoalaGlory.card, KoalaThief.card, KoalaMight.card, KoalaStats.card};
                        var gifts2 = gifts.ToList();
                        foreach (var c in player.data.currentCards)
                        {
                            foreach (var g in gifts)
                            {
                                if (c == g)
                                {
                                    gifts2.Remove(g);
                                }
                            }
                        }
                        var gifts3 = gifts2.ToArray();
                        if (gifts3.Length != 0)
                        {
                            var rng = UnityEngine.Random.Range(0, gifts3.Length);
                            var index = gifts.ToList().IndexOf(gifts3[rng]);
                            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, gifts[index], false, "", 0, 0);
                        }
                    }
                });
            }
        }
    }
}

namespace KFC.ArtSchenanigans
{
    public class KoalaArtHandler : MonoBehaviour
    {
        public GameObject owner;
        public void Update()
        {
            if (owner == null)
            {
                Destroy(gameObject);
            }
            if (owner.scene.name == "DontDestroyOnLoad")
            {
                Destroy(gameObject);
            }
        }
        public void DestroyMe()
        {
            Destroy(gameObject);
        }
    }
    public class Aahahsdhahh : MonoBehaviour
    {
        public KoalaArtHandler handler;
        public void OnDestroy()
        {
            handler.DestroyMe();
        }
    }
}