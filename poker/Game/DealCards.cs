using poker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poker.Game
{
    public static class DealCards
    {
        public static List<Card[]> Deal(int amountOfPlayers)
        {
            List<Card[]> result = new List<Card[]>();

            var deckOfCards = new DeckOfCards();
            var deck = deckOfCards.Deck;
            
            var cardNumbers = GenerateCardNumbers(amountOfPlayers);
            for(int i=0; i < amountOfPlayers; i++)
            {
                var hand = new Card[5];
                for(int j = 0; j<5; j++)
                {                  
                    hand[j] = deck[cardNumbers[0]];
                    cardNumbers.RemoveAt(0);
                }
                result.Add(hand);
            }
            return result;
        }

        private static List<int> GenerateCardNumbers(int amountOfPlayers)
        {
            List<int> randomList = new List<int>();            
            var rand = new Random();
            for (int i = 0; i<amountOfPlayers*5; i++)
            {                
                var cardNumber = rand.Next(0, 52);
                while (randomList.Contains(cardNumber))
                {
                    cardNumber = rand.Next(0, 52);
                }
                randomList.Add(cardNumber);
            }
            return randomList; 
        } 
    }
}
