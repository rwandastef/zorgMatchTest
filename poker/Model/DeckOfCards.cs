using poker.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poker.Model
{
    public class DeckOfCards
    {
        public Card[] Deck { get;  }
        
        public DeckOfCards()
        {
            Deck = MakeDeck();
        }

        private Card[] MakeDeck()
        {
            List<Card> cards = new List<Card>();           
            var cardTypes = System.Enum.GetValues(typeof(CardType));
            var cardRanks = System.Enum.GetValues(typeof(CardRank));
            foreach (var cardType in cardTypes)
            {
                foreach (var cardRank in cardRanks)
                {
                    Card card = new Card();
                    card.cardRank = (CardRank)cardRank;
                    card.cardType = (CardType)cardType;
                    cards.Add(card);


                }
            }

            return cards.ToArray();
        }
    }
}
