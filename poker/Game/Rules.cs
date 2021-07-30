using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using poker.Model;
using poker.Model.Enum;

namespace poker.Game
{
    public static class Rules
    {
        public static int CountMaxSameColor(Card[] hand)
        {
            return hand.GroupBy(x => x.cardType)
                .Select(g => new { name = g.Key, count = g.Count() })
                .Max(m => m.count);
        }

        public static int CountMaxSameRank(Card[] hand)
        {
            return hand.GroupBy(x => x.cardRank)
                .Select(g => new { name = g.Key, count = g.Count() })
                .Max(m => m.count);
        }
        public static KindOfHand WhatKindOfHand(Card[] hand)
        {           
            if (CountMaxSameColor(hand) == 5)
            {
                if (IsStreet(hand))
                {
                    if(hand.Any( x => x.cardRank == CardRank.Ace))
                    {
                        return KindOfHand.RoyalFlush;
                    }
                    return KindOfHand.StraightFlush;
                }
            }
            if (CountMaxSameRank(hand) == 4)
            {
                var kindOf = hand.GroupBy(x => x.cardRank)
                .Select(g => new { name = g.Key, count = g.Count()}).Where(g =>g.count==4).First();
                return KindOfHand.ForOfKind;
            }
            if (IsFullHouse(hand))
            {
                return KindOfHand.FullHouse;
            }
            if (CountMaxSameColor(hand) == 5)
            {
                return KindOfHand.Flush;
            }
            if (IsStreet(hand))
            {
                return KindOfHand.Straight;
            }
            if (IsThreeOfKind(hand))
            {
                return KindOfHand.ThreeOfKind;
            }
            if (IsTwoPair(hand))
            {
                return KindOfHand.TwoPair;
            }
            if (IsTwoOfKind(hand))
            {
                return KindOfHand.TwoOfKind;
            }

            return KindOfHand.Nothing;

        }

        public static bool IsStreet(Card[] hand)
        {
            var iets = hand.AsEnumerable();
            iets = iets.OrderBy(x => x.cardRank);
            var nogiets = iets.GroupBy(x => x.cardRank).Count();
            if (iets.GroupBy(x => x.cardRank).Count() == 5)
            {
                var max = (int)iets.Max(x => x.cardRank);
                var min = (int)iets.Min(x => x.cardRank);
                return (max-min==4);
            }


            return false;
        }
        
        public static bool IsFullHouse(Card[] hand)
        {
            var group = hand.GroupBy(x => x.cardRank)
                .Select(g => new { name = g.Key, count = g.Count() });
            var threeOfKind = group.Where(g => g.count == 3).FirstOrDefault();
            var twoOfKind = group.Where(g => g.count == 2).FirstOrDefault();
            if (threeOfKind!=null && twoOfKind != null)
            {
                return true;
            }
            return false;
        }

        public static bool IsTwoPair(Card[] hand)
        {
            var group = hand.GroupBy(x => x.cardRank)
                .Select(g => new { name = g.Key, count = g.Count() });
            return group.Where(g => g.count == 2).Count() == 2;
        }

        public static bool IsThreeOfKind(Card[] hand)
        {

            var group = hand.GroupBy(x => x.cardRank)
                .Select(g => new { name = g.Key, count = g.Count() });
            var threeOfKind = group.Where(g => g.count == 3).FirstOrDefault();
            return threeOfKind != null;
        }

        public static bool IsTwoOfKind(Card[] hand)
        {

            var group = hand.GroupBy(x => x.cardRank)
                .Select(g => new { name = g.Key, count = g.Count() });
            var threeOfKind = group.Where(g => g.count == 2).FirstOrDefault();
            return threeOfKind != null;
        }


    }
}
