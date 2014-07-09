using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge170Intermediate.Cards;

namespace Challenge170Intermediate
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Hand initialHand = new Hand(Console.ReadLine().Split(',').Select(s => new Card(s.Trim())), Hand.OrderByRank);
            Card replacementCard = new Card(Console.ReadLine());

            for (int i = 0; i < 7; i++)
            {
                Card[] testHand = new Card[7];
                initialHand.CopyTo(testHand);
                Card replacedCard = testHand[i];
                testHand[i] = replacementCard;
                Func<Card, int>[] sortMethods = new[] { Hand.OrderByRank, Hand.OrderBySuit };
                foreach (Func<Card, int> sortMethod in sortMethods)
                {
                    Hand testHandSorted = new Hand(testHand, sortMethod);
                    if (testHandSorted.IsWinningHand(false, sortMethod) || testHandSorted.IsWinningHand(true, sortMethod))
                    {
                        Console.WriteLine("Swap the new card for the {0} to win!", replacedCard);
                        Console.ReadKey();
                        return;
                    }
                }
            }

            Console.WriteLine("No possible winning hand.");
            Console.ReadKey();
        }

        public static bool IsWinningHand(this Hand hand, bool reverse, Func<Card, int> sortMethod)
        {
            if (reverse) hand = new Hand(hand, sortMethod, reverse);
            {
                IEnumerable<Card> first3 = hand.GetFromHand(Hand.HandPosition.Start, 3);
                IEnumerable<Card> last4 = hand.GetFromHand(Hand.HandPosition.End, 4);
                if (first3.IsValidMeld(reverse) && last4.IsValidMeld(reverse)) return true;
            }

            {
                IEnumerable<Card> first4 = hand.GetFromHand(Hand.HandPosition.Start, 4);
                IEnumerable<Card> last3 = hand.GetFromHand(Hand.HandPosition.End, 3);
                if (first4.IsValidMeld(reverse) && last3.IsValidMeld(reverse)) return true;
            }

            return false;
        }

        public static bool IsValidMeld(this IEnumerable<Card> cards, bool reverse)
        {
            var cardArray = (reverse ? cards.Reverse() : cards).ToArray();

            if (IsValidSet(cardArray))
            {
                return true;
            }
            else if (IsValidRun(cardArray))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidSet(Card[] cards)
        {
            for (int i = 1; i < cards.Length; i++)
            {
                if (cards[0].Rank != cards[i].Rank) return false;
            }
            // Console.WriteLine("VS");
            return true;
        }

        public static bool IsValidRun(Card[] cards)
        {
            for (int i = 1; i < cards.Length; i++)
            {
                if (cards[i].Suit != cards[0].Suit ||
                    cards[i].Rank != cards[0].Rank + i) return false;
            }
            // Console.WriteLine("VR");
            return true;
        }
    }
}
