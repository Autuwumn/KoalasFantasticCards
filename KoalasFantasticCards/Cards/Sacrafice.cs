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
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using WillsWackyManagers.Utils;
using Photon.Realtime;
using ClassesManagerReborn.Util;
using System;
using UnboundLib.Utils;
using System.Runtime.InteropServices;
using ModdingUtils.GameModes;
using System.Data;
using UnityEngine.Experimental.PlayerLoop;
using ModdingUtils.Utils;

namespace KFC.Cards
{
    public class Sacrafice : SimpleCard
    {
        internal static CardInfo card = null;
        //public static Shop SellShop;
        //public static string ShopID = "Koala_Removal_Shop";
        public static List<CardInfo> playerCards;
        public override CardDetails Details => new CardDetails
        {
            Title = "Removal",
            Description = "Click one of your cards on cardbar to remove it, at the price of a curse",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Sacrafice"),
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("cantEternity"), RerollManager.instance.NoFlip, CurseManager.instance.curseSpawnerCategory, CustomCardCategories.instance.CardCategory("CardManipulation") };
        }
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Destroy(player.gameObject.GetOrAddComponent<ShopHandle>());
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<ShopHandle>();
            /**playerCards = player.data.currentCards.ToList();
            foreach(var c in playerCards) if(c.gameObject.GetComponent<ClassNameMono>() != null) playerCards.Remove(c);
            for(int i = 0; i < playerCards.Count()-1; i++)
            {
                for(int j = i+1; j < playerCards.Count(); j++)
                {
                    if(playerCards[i].cardName == playerCards[j].cardName) playerCards.Remove(playerCards[j]);
                }
            }
            if(playerCards.Count() == 0)
            {
                CurseManager.instance.CursePlayer(player, (curse) => {
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 20f);
                });
                return;
            }
            if (SellShop != null) ShopManager.instance.RemoveShop(SellShop);
            SellShop = ShopManager.instance.CreateShop(ShopID);
            SellShop.UpdateMoneyColumnName("Removals");
            StartCoroutine(SetUpShop());**/
        }
        /**public static IEnumerator SetUpShop()
        {
            List<ShopCards> items = new List<ShopCards>();
            foreach (var c in playerCards)
            {
                var a = new Dictionary<string, int>();
                a.Add("Removals", 1);
                items.Add(new ShopCards(c.name, a, new Tag[] { new Tag(c.rarity.ToString()) }, card.gameObject));
            }
            SellShop.AddItems(items.ToArray());
            yield break;
        }**/
    }
}

namespace KFC.MonoBehaviors
{
    public class ShopHandle : MonoBehaviour
    {
        public void Update()
        {
            var player = GetComponent<Player>();
            if(Input.GetMouseButtonDown(0))
            {
                try
                {
                    var b = CardBarUtils.instance.PlayersCardBar(player.playerID);
                    var t = b.GetFieldValue("currentCard").ToString();
                    if (t != null && player.gameObject.GetComponent<PhotonView>().IsMine)
                    {
                        var a = t.Substring(0, t.IndexOf('('));
                        var card = CardManager.GetCardInfoWithName(a);
                        if (card == Sacrafice.card) return;
                        ModdingUtils.Utils.Cards.instance.RemoveCardsFromPlayer(player, new int[] { player.data.currentCards.IndexOf(Sacrafice.card), player.data.currentCards.IndexOf(card) });
                        
                        KFC.instance.ExecuteAfterFrames(20, () =>
                        {
                            CurseManager.instance.CursePlayer(player);
                        });
                    }
                } catch { }
            }
        }
    }
    /**public class ShopCards : Purchasable
    {
        private string name;
        private Dictionary<string, int> cost;
        private Tag[] tags;
        private GameObject prefab;
        public ShopCards(string nameIn, Dictionary<string, int> costIn, Tag[] tagsIn, GameObject prefabIn)
        {
            name = nameIn;
            cost = costIn;
            tags = tagsIn;
            prefab = prefabIn;
        }

        public override string Name => name;
        public override Dictionary<string, int> Cost => cost;
        public override Tag[] Tags => tags;

        public override bool CanPurchase(Player player)
        {
            return true;
        }
        public override GameObject CreateItem(GameObject parent)
        {
            return GameObject.Instantiate(prefab, parent.transform);
        }

        public override void OnPurchase(Player player, Purchasable item)
        {
            var card = CardManager.GetCardInfoWithName(item.Name);
            ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, player.data.currentCards.IndexOf(card));
            CurseManager.instance.CursePlayer(player, (curse) => {
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 20f);
            });
        }
    }**/
}