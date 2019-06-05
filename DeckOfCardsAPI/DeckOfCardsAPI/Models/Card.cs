using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeckOfCardsAPI.Models
{
    public class Card
    {
        public string Value { get; set; }
        public string Suit { get; set; }
        public string ImageUrl { get; set; }
    }
}