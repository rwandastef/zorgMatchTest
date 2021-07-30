using poker.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poker.Model
{
    public class Card
    {
        public CardType cardType { get; set; }
        public CardRank cardRank { get; set; }        
    }
}
