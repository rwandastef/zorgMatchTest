using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using poker.Model;
using poker.Model.Enum;

namespace poker.Game
{
    public static class Compare 
    {
        
        public static int Wins(Card[] hand1, Card[] hand2)
        {
            if (Rules.WhatKindOfHand(hand1) > Rules.WhatKindOfHand(hand2))
            {
                return 0;
            }
            else if (Rules.WhatKindOfHand(hand1) < Rules.WhatKindOfHand(hand2))
            {
                return 1;
            }
            else 
            {
                var result = CompareEquals(hand1, hand2);
                return result;
            }

        }

        private static int CompareEquals(Card[] hand1, Card[] hand2)
        {
            int result = 3;
            switch (Rules.WhatKindOfHand(hand1))
            {
                case KindOfHand.StraightFlush:
                    result = CompareHighFlush(hand1, hand2);
                    break;
                case KindOfHand.ForOfKind:
                    result = CompareForOfXKind(hand1, hand2, 4);
                    break;
                case KindOfHand.FullHouse:
                    result = CompareForOfXKind(hand1, hand2, 3);
                    break;
                case KindOfHand.Flush:
                    result = CompareHighFlush(hand1, hand2);
                    break;
                case KindOfHand.Straight:
                    result = CompareHighFlush(hand1, hand2);
                    break;
                case KindOfHand.ThreeOfKind:
                    result = CompareForOfXKind(hand1, hand2, 3);
                    break;
                case KindOfHand.TwoPair:
                    result = CompareTwoPair(hand1, hand2);
                    break;
                case KindOfHand.TwoOfKind:
                    result = CompareForOfXKind(hand1, hand2, 2);
                    break;
                case KindOfHand.Nothing:
                    result = CompareHighFlush(hand1, hand2);
                    break;
                default:
                    break;
            }
            return result;
        }

        private static int CompareForOfXKind(Card[] hand1, Card[] hand2, int amountSame)
        {
            var kindOf1 = hand1.GroupBy(x => x.cardRank)
               .Select(g => new { name = g.Key, count = g.Count() }).Where(g => g.count == amountSame).First().name;
            
            var kindOf2 = hand2.GroupBy(x => x.cardRank)
               .Select(g => new { name = g.Key, count = g.Count() }).Where(g => g.count == amountSame).First().name;
            if(kindOf1>kindOf2)
            {
                return 0;
            }
            return 1;
        }

        private static int CompareTwoPair(Card[] hand1, Card[] hand2)
        {
            var kindOf1 = hand1.GroupBy(x => x.cardRank)
               .Select(g => new { name = g.Key, count = g.Count() }).Where(g => g.count == 2);
            var h1 = kindOf1.First().name > kindOf1.Last().name ? kindOf1.First() : kindOf1.Last();

            var kindOf2 = hand2.GroupBy(x => x.cardRank)
               .Select(g => new { name = g.Key, count = g.Count() }).Where(g => g.count == 2);
            var h2 = kindOf2.First().name > kindOf2.Last().name ? kindOf2.First() : kindOf2.Last();

            ;
            if (h1.name > h2.name)
            {
                return 0;
            }
            return 1;
        }



        private static int CompareHighFlush(Card[] hand1, Card[] hand2)
        {
            if (HighestCard(hand1).cardRank > HighestCard(hand2).cardRank)
            {
                return 0;
            }
            return 1;
        }

        private static Card HighestCard(Card[] hand)
        {
            var cards = hand.AsEnumerable().OrderByDescending(x => x.cardRank);
            return cards.First();
            
        }
    }
}
