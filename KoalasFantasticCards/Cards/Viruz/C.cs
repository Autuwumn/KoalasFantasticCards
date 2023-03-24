using UnityEngine;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using ClassesManagerReborn.Util;
using RarityLib.Utils;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace KFC.Cards
{
    public class Ccard : SimpleCard
    {
        internal static CardInfo card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = ViruzClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title = "increase c",
            Description = "Is this good, or bad?",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_CViruz"),
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.MagicPink,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "<#FFFF00>+1",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "c"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("cantEternity") };
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (!player.data.view.IsMine) return;
            var classCards = 0;
            foreach (var card in player.data.currentCards)
            {
                if (card.cardName.Equals("increase a") || card.cardName.Equals("increase b") || card.cardName.Equals("increase c"))
                {
                    classCards++;
                }
            }
            if (classCards >= 15)
            {
                CustomCardCategories.instance.MakeCardsExclusive(Ard.card, ViruzCard.card);
                CustomCardCategories.instance.MakeCardsExclusive(Bard.card, ViruzCard.card);
                CustomCardCategories.instance.MakeCardsExclusive(Ccard.card, ViruzCard.card);
                CustomCardCategories.instance.MakeCardsExclusive(ViruzCard.card, Ard.card);
                CustomCardCategories.instance.MakeCardsExclusive(ViruzCard.card, Bard.card);
                CustomCardCategories.instance.MakeCardsExclusive(ViruzCard.card, Ccard.card);
            }
        }
    }
}