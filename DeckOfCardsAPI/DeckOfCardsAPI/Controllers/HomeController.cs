using DeckOfCardsAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DeckOfCardsAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayCards()
        {
            string newDeckApiText = GetNewDeckText();
            JToken jsonData = JToken.Parse(newDeckApiText);
            Deck deck = new Deck();
            deck.DeckId = jsonData["deck_id"].ToString();
            string fiveCardsApiText = DrawFiveCards(deck.DeckId);
            List<Card> fiveCards = ConvertListOfCards(fiveCardsApiText);

            return View(fiveCards);

        }

        public string GetNewDeckText()
        {
            string url = $"https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1";

            HttpWebRequest request = WebRequest.CreateHttp(url);
            //sometimes extra steps here
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string apiText = rd.ReadToEnd();

            return apiText;
        }

        public string DrawFiveCards(string deckId)
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://deckofcardsapi.com/api/deck/{deckId}/draw/?count=5");
            //sometimes extra steps here
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string fiveCardsApiText = rd.ReadToEnd();
            return fiveCardsApiText;
        }

        public List<Card> ConvertListOfCards(string apiText)
        {
            JToken jsonData = JToken.Parse(apiText);
            List<JToken> cardTokens = jsonData["cards"].ToList();
            List<Card> cards = new List<Card>();


            foreach (JToken j in cardTokens)
            {
                Card c = new Card();
                c.Value = j["value"].ToString();
                c.Suit = j["suit"].ToString();
                c.ImageUrl = j["image"].ToString();

                cards.Add(c);
            }

            return cards;
        }


    }
}