using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Poker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string path = @"..\..\Data\TextFileRecord.txt";
        public static string NewName = "";
        public List<Record> listRecord = new List<Record>();
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public DateTime dateTime = new DateTime();
        public int countTime = 0;
        public static double betValue = 0;
        public static int countHighLowWin = 1;
        public static double NewbetValue = 0;
        bool changeBetValue = false;
        public static double Balance = 1000;
        public static double NewBalance = 1000;
        public string TextBlockText = "BET and DRAW";
        public int cardNumber1 = 0;
        public int cardNumber2 = 0;
        public int cardNumber3 = 0;
        public int cardNumber4 = 0;
        public int cardNumber5 = 0;
        bool holdCard1 = false;
        bool holdCard2 = false;
        bool holdCard3 = false;
        bool holdCard4 = false;
        bool holdCard5 = false;
        bool emptyCards = true;
        bool highCard = false;
        bool lowCard = false;
        bool lose = false;
        bool cheat = false;
        int countDraw = 0;
        public Image image = new Image();
        public Image imageEmpty = new Image();
        bool highLow = false;
        bool useHighLow = false;
        int winValue = 0;
        double HighLowWinValue = 0;
        public List<Card> cards = new List<Card>();

        public class Record
        {
            public int Number { get; set; }
            public string Name { get; set; }
            public string Time { get; set; }
        }

        public class Card
        {
            public int number { get; set; }
            public int Value { get; set; }
            public string Sign { get; set; }
            public bool Hold { get; set; }
        }
        public void GetAllCards()
        {
            for (int i = 1; i < 15; i++)
            {
                Card card1 = new Card();
                card1.number = i;
                card1.Value = i + 1;
                card1.Sign = "T";
                card1.Hold = false;
                cards.Add(card1);
                Card card2 = new Card();
                card2.number = i + 1;
                card1.Value = i + 1;
                card2.Sign = "C";
                card2.Hold = false;
                cards.Add(card2);
                Card card3 = new Card();
                card3.number = i + 2;
                card1.Value = i + 1;
                card3.Sign = "P";
                card3.Hold = false;
                cards.Add(card3);
                Card card4 = new Card();
                card4.number = i + 3;
                card1.Value = i + 1;
                card4.Sign = "H";
                card4.Hold = false;
                cards.Add(card4);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            GetImages();
            TimerSet();
            PrintBalance();
            GetAllCards();
        }

        private void TimerSet()
        {
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            dateTime = dateTime.AddSeconds(1);
            TextBlockInfo.Content = TextBlockText;
            countTime++;
            if (highLow &&  countDraw == 2)
            {
                ButtonHighLow.Visibility = Visibility.Visible;
                ButtonLow.Visibility = Visibility.Visible;
                if (countTime % 2 == 1)
                {
                    ButtonHighLow.Background = new SolidColorBrush(Colors.GreenYellow);
                    ButtonLow.Background = new SolidColorBrush(Colors.Red);
                }
                if (countTime % 2 == 0)
                {
                    ButtonHighLow.Background = new SolidColorBrush(Colors.Red);
                    ButtonLow.Background = new SolidColorBrush(Colors.GreenYellow);
                }
            }
        }

        private void PrintBalance()
        {          
            LabelBalanceValue.Content = "$ " + Balance.ToString("N", new CultureInfo("en-US"));
                    
            if (Balance <= 0 && betValue <= 0)
            {
                //MessageBox.Show("Game over !");
                TextBoxMessage.Visibility = Visibility.Visible;
                TextBoxMessage.Text = "Game over !";
                TextBlockInfo.Content = "Game over !";
                Balance = 1000;
                betValue = 0;
                Balance = Balance - betValue;
                PrintBetValue();
                PrintBalance();              
                PrintTextBlockInfo();
                StartNewGame();
            }
        }

        private void PrintTextBlockInfo()
        {
            TextBlockInfo.Content = TextBlockText;
        }

        private void PrintBetValue()
        {
            //TextBoxBetValue.Text = betValue.ToString("N", new CultureInfo("en-US"));
            TextBoxBetValue.Text = betValue.ToString();
        }

        private void ButtonHold1_Click(object sender, RoutedEventArgs e)
        {
            if (emptyCards)
            {
                return;
            }
            if (TextBlockHold1.Visibility == Visibility.Hidden)
            {
                TextBlockHold1.Visibility = Visibility.Visible;
                holdCard1 = true;
            }
            else
            {
                TextBlockHold1.Visibility = Visibility.Hidden;
                holdCard1 = false;
            }
        }

        private void ButtonHold2_Click(object sender, RoutedEventArgs e)
        {
            if (emptyCards)
            {
                return;
            }
            if (TextBlockHold2.Visibility == Visibility.Hidden)
            {
                TextBlockHold2.Visibility = Visibility.Visible;
                holdCard2 = true;
            }
            else
            {
                TextBlockHold2.Visibility = Visibility.Hidden;
                holdCard2 = false;
            }
        }

        private void ButtonHold3_Click(object sender, RoutedEventArgs e)
        {
            if (emptyCards)
            {
                return;
            }
            if (TextBlockHold3.Visibility == Visibility.Hidden)
            {
                TextBlockHold3.Visibility = Visibility.Visible;
                holdCard3 = true;
            }
            else
            {
                TextBlockHold3.Visibility = Visibility.Hidden;
                holdCard3 = false;
            }
        }

        private void ButtonHold4_Click(object sender, RoutedEventArgs e)
        {
            if (emptyCards)
            {
                return;
            }
            if (TextBlockHold4.Visibility == Visibility.Hidden)
            {
                TextBlockHold4.Visibility = Visibility.Visible;
                holdCard4 = true;
            }
            else
            {
                TextBlockHold4.Visibility = Visibility.Hidden;
                holdCard4 = false;
            }
        }

        private void ButtonHold5_Click(object sender, RoutedEventArgs e)
        {
            if (emptyCards)
            {
                return;
            }
            if (TextBlockHold5.Visibility == Visibility.Hidden)
            {
                TextBlockHold5.Visibility = Visibility.Visible;
                holdCard5 = true;
            }
            else
            {
                TextBlockHold5.Visibility = Visibility.Hidden;
                holdCard5 = false;
            }
        }

        private void ButtonBetOne_Click(object sender, RoutedEventArgs e)
        {
            Balance = Balance + betValue;
            Balance--;
            betValue = 1;            
            PrintBetValue();
            PrintBalance();
        }

        private void ButtonDraw_Click(object sender, RoutedEventArgs e)
        {
            if (lose)
            {
                lose = false;
                StartNewGame();
            }
            if (useHighLow)
            {
                Balance = Balance + HighLowWinValue;
                betValue = NewbetValue;
                PrintBetValue();
                PrintBalance();
            }
            if (countDraw == 2)
            {
                EndRound();
                countDraw = 0;
                PrintBalance();
                PrintBetValue();
                return;
            }
            PrintBalance();
            PrintBetValue();
            TextBlockInfo.Content = TextBlockText;           
            PrintTextBlockInfo();
            ButtonMinus.IsEnabled = false;
            ButtonBetOne.IsEnabled = false;
            TextBoxBetValue.IsReadOnly = true;
            if (countDraw >= 0)
            {
                TextBoxMessage.Visibility = Visibility.Hidden;
                TextBlockText = "DRAW ";
              
                ButtonBetMax.IsEnabled = false;
                ButtonPlus.IsEnabled = false;
                TextBoxBetValue.IsReadOnly = true;
                ButtonPlus10.Visibility = Visibility.Hidden;
                ButtonPlus100.Visibility = Visibility.Hidden;
                ButtonX10.Visibility = Visibility.Hidden;
                ButtonX100.Visibility = Visibility.Hidden;
                ButtonPayOut.Visibility = Visibility.Hidden;
            }
            if (countDraw == 0)
            {
                TextBlockInfo2.Content = "";
                if (changeBetValue)
                {
                    Balance = NewBalance;
                    betValue = NewbetValue;
                    changeBetValue = false;
                   
                }
                if (!changeBetValue && Balance >= betValue)
                {
                    Balance = Balance - betValue;
                }
                
                if (betValue == 0)
                {
                    betValue = 1;
                    Balance = Balance - betValue;
                    if (Balance < 0)
                    {
                        betValue = 0;
                        Balance = 0;
                    }
                }                               
                PrintBalance();
                PrintBetValue();
            }          

            if (countDraw < 2)
            {
                emptyCards = false;
                GetRandomCards();
                countDraw++;

                if (holdCard1 && holdCard2 && holdCard3 && holdCard4 && holdCard5)
                {
                    countDraw = 2;
                }
                if (countDraw == 2)
                {
                    CheckWin();
                   
                }
            }
            if (countDraw == 1)
            {
                CheckWin();
            }
           
        }

        private void StartNewGame()
        {
            ResetGame();
        }      

        private void ResetGame()
        {
            TextBlockInfo2.Content = "";
            ButtonDraw.Content = "DRAW";
            lose = false;
            countHighLowWin = 1;
            TextBoxMessage.Visibility = Visibility.Hidden;
            winValue = 0;
            useHighLow = false;
            highLow = false;
            ButtonHighLow.Visibility = Visibility.Hidden;
            ButtonLow.Visibility = Visibility.Hidden;
            TextBoxBetValue.IsReadOnly = false;
            ButtonMinus.IsEnabled = true;
            ButtonBetOne.IsEnabled = true;
            ButtonBetMax.IsEnabled = true;
            ButtonPlus.IsEnabled = true;
            ButtonPlus10.Visibility = Visibility.Visible;
            ButtonPlus100.Visibility = Visibility.Visible;
            ButtonX10.Visibility = Visibility.Visible;
            ButtonX100.Visibility = Visibility.Visible;
            ButtonPayOut.Visibility = Visibility.Visible;
            emptyCards = true;
            countDraw = 0;
            Card1.Source = imageEmpty.Source;
            Card2.Source = imageEmpty.Source;
            Card3.Source = imageEmpty.Source;
            Card4.Source = imageEmpty.Source;
            Card5.Source = imageEmpty.Source;
            TextBlockHold1.Visibility = Visibility.Hidden;
            TextBlockHold2.Visibility = Visibility.Hidden;
            TextBlockHold3.Visibility = Visibility.Hidden;
            TextBlockHold4.Visibility = Visibility.Hidden;
            TextBlockHold5.Visibility = Visibility.Hidden;
            holdCard1 = false;
            holdCard2 = false;
            holdCard3 = false;
            holdCard4 = false;
            holdCard5 = false;
          
            //PrintBalance();
            //PrintBetValue();
           
            TextBlockText = "BET";
        }

        private void GetImages()
        {
            List<char> sign = new List<char>()
            {
                'T','C','P','H'
            };
            List<char> signNumber = new List<char>()
            {
                'A','J','Q','K'
            };
            int count = 0;
            for (int i = 2; i < 15; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    count++;
                    Image image = new Image();
                    image.Name = "Card" + count.ToString("0#");
                    if (i == 11)
                    {
                        image.Tag = "Cards_" + count.ToString("0#") + sign[j].ToString() + "A";
                       
                        image.Source = new BitmapImage(new Uri(@"Cards/Cards_" + count.ToString("0#") + sign[j].ToString() + "A" + ".jpg", UriKind.Relative));
                        RegisterName(image.Name, image);
                        continue;
                    }
                    if (i == 12)
                    {
                        image.Tag = "Cards_" + count.ToString("0#") + sign[j].ToString() + "J";
                        image.Source = new BitmapImage(new Uri(@"Cards/Cards_" + count.ToString("0#") + sign[j].ToString() + "J" + ".jpg", UriKind.Relative));
                        RegisterName(image.Name, image);
                        continue;
                    }
                    if (i == 13)
                    {
                        image.Tag = "Cards_" + count.ToString("0#") + sign[j].ToString() + "Q";
                        image.Source = new BitmapImage(new Uri(@"Cards/Cards_" + count.ToString("0#") + sign[j].ToString() + "Q" + ".jpg", UriKind.Relative));
                        RegisterName(image.Name, image);
                        continue;
                    }
                    if (i == 14)
                    {
                        image.Tag = "Cards_" + count.ToString("0#") + sign[j].ToString() + "K";
                        image.Source = new BitmapImage(new Uri(@"Cards/Cards_" + count.ToString("0#") + sign[j].ToString() + "K" + ".jpg", UriKind.Relative));
                        RegisterName(image.Name, image);
                        continue;
                    }
                    image.Tag = "Cards_" + count.ToString("0#") + sign[j].ToString() + i.ToString("0#");
                    image.Source = new BitmapImage(new Uri(@"Cards/Cards_" + count.ToString("0#") + sign[j].ToString() + i.ToString("0#") + ".jpg", UriKind.Relative));
                    RegisterName(image.Name, image);
                  
                }
            }
           
            imageEmpty = new Image();
            imageEmpty.Name = "Card" + "00";
            imageEmpty.Tag = "Cards_53EMP";
            imageEmpty.Source = new BitmapImage(new Uri(@"Cards/Cards_53EMP.jpg", UriKind.Relative));
            RegisterName(imageEmpty.Name, image);
        }

        private void GetRandomCards()
        {
            Random random = new Random();
            var randomNumbers = Enumerable.Range(1, 52).OrderBy(x => random.Next()).Take(21).ToList();
           
            if (!holdCard1)
            {
                //cardNumber1 = random.Next(1, 53);
                cardNumber1 = randomNumbers[0];
                Image image1 = (Image)this.FindName("Card" + cardNumber1.ToString("0#"));
                Card1.Source = image1.Source;
            }
            if (!holdCard2)
            {
                //cardNumber2 = random.Next(1, 53);
                cardNumber2 = randomNumbers[1];
                Image image2 = (Image)this.FindName("Card" + cardNumber2.ToString("0#"));
                Card2.Source = image2.Source;
            }
            if (!holdCard3)
            {
                //cardNumber3 = random.Next(1, 53);
                cardNumber3 = randomNumbers[2];
                Image image3 = (Image)this.FindName("Card" + cardNumber3.ToString("0#"));
                Card3.Source = image3.Source;
            }
            if (!holdCard4)
            {
                //cardNumber4 = random.Next(1, 53);
                cardNumber4 = randomNumbers[3];
                Image image4 = (Image)this.FindName("Card" + cardNumber4.ToString("0#"));
                Card4.Source = image4.Source;
            }
            if (!holdCard5)
            {
                //cardNumber5 = random.Next(1, 53);
                cardNumber5 = randomNumbers[4];
                Image image5 = (Image)this.FindName("Card" + cardNumber5.ToString("0#"));
                Card5.Source = image5.Source;
            }
            if (cheat)
            {
                Cheat();
            }
        }

        private void ButtonBetMax_Click(object sender, RoutedEventArgs e)
        {
          
            betValue = betValue + Balance;
            Balance = 0;
            PrintBetValue();
            PrintBalance();
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            if (Balance > 0)
            {
                betValue++;
                Balance--;
                PrintBetValue();
                PrintBalance();
            }
           
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            if (betValue > 1)
            {
                betValue--;
                Balance++;
                PrintBetValue();
                PrintBalance();
            }
           
        }

        private void CheckWin()
        {
            NewbetValue = betValue;
            //Royal Flush     10,J,Q,K,A                  250 * bet
            //Five of a Kind  77777 AAAAA                 150    
            //Straight Flush  Pic 6,7,8,9,10              50      
            //Four of a Kind  7777  AAAA                  25        
            //Full House      888,44  AAA,KK              9      
            //Flush           Pic Q,10,7,6,2              6   
            //Straight        9,8,7,6,5                   4      
            //Tree of a Kind  777, 4,2                    3 
            //Two Pairs       QQ,77, 4                    2
            //One Pair        10,10, K,4,3                1    
            //No Pair         high card                   0     
            int royalFlush = 250;
            int fiveOfaKind = 150;
            int StraightFlush = 50;
            int FourOfAKind = 25;
            int FullHouse = 9;
            int Flush = 6;
            int Straight = 4;
            int TreeOfAKind = 3;
            int TwoPairs = 2;
            int OnePair = 1;
            //int highCard = 0;
            string cardSign1 = "";
            string cardNumber1 = "";
            string cardSign2 = "";
            string cardNumber2 = "";
            string cardSign3 = "";
            string cardNumber3 = "";
            string cardSign4 = "";
            string cardNumber4 = "";
            string cardSign5 = "";
            string cardNumber5 = "";
            //card 1
            string cardSource1 = Card1.Source.ToString().Substring(Card1.Source.ToString().Length - 9, 5);
            if (cardSource1.Substring(0,1) == "_")
            {
                cardSource1 = cardSource1.Substring(1,4);
                 cardSign1 = cardSource1.Substring(2, 1);
                 cardNumber1 = cardSource1.Substring(3, 1);
            }
            else 
            {
                 cardSign1 = cardSource1.Substring(2, 1);
                 cardNumber1 = cardSource1.Substring(3, 2);
            }
            //card 2
            string cardSource2 = Card2.Source.ToString().Substring(Card2.Source.ToString().Length - 9, 5);
            if (cardSource2.Substring(0, 1) == "_")
            {
                cardSource2 = cardSource2.Substring(1, 4);
                 cardSign2 = cardSource2.Substring(2, 1);
                 cardNumber2 = cardSource2.Substring(3, 1);
            }
            else 
            {
                 cardSign2 = cardSource2.Substring(2, 1);
                 cardNumber2 = cardSource2.Substring(3, 2);
            }
            //card 3
            string cardSource3 = Card3.Source.ToString().Substring(Card3.Source.ToString().Length - 9, 5);
            if (cardSource3.Substring(0, 1) == "_")
            {
                cardSource3 = cardSource3.Substring(1, 4);
                 cardSign3 = cardSource3.Substring(2, 1);
                 cardNumber3 = cardSource3.Substring(3, 1);
            }
            else 
            {
                 cardSign3 = cardSource3.Substring(2, 1);
                 cardNumber3 = cardSource3.Substring(3, 2);
            }
            //card 4
            string cardSource4 = Card4.Source.ToString().Substring(Card4.Source.ToString().Length - 9, 5);
            if (cardSource4.Substring(0, 1) == "_")
            {
                cardSource4 = cardSource4.Substring(1, 4);
                 cardSign4 = cardSource4.Substring(2, 1);
                 cardNumber4 = cardSource4.Substring(3, 1);
            }
            else
            {
                 cardSign4 = cardSource4.Substring(2, 1);
                 cardNumber4 = cardSource4.Substring(3, 2);
            }
            //card 5
            string cardSource5 = Card5.Source.ToString().Substring(Card5.Source.ToString().Length - 9, 5);
            if (cardSource5.Substring(0, 1) == "_")
            {
                cardSource5 = cardSource5.Substring(1, 4);
                 cardSign5 = cardSource5.Substring(2, 1);
                 cardNumber5 = cardSource5.Substring(3, 1);
            }
            else 
            {
                 cardSign5 = cardSource5.Substring(2, 1);
                 cardNumber5 = cardSource5.Substring(3, 2);
            }

            //cardNumber1 = "10";
            //cardNumber2 = "A";
            //cardNumber3 = "J";
            //cardNumber4 = "Q";
            //cardNumber5 = "K";
            //cardSign1 = "T";
            //cardSign2 = "T";
            //cardSign3 = "T";
            //cardSign4 = "T";
            //cardSign5 = "T";
            List<string> CardNumberList = new List<string>() { cardNumber1, cardNumber2, cardNumber3, cardNumber4, cardNumber5 };
            List<string> CardSignList = new List<string>() { cardSign1, cardSign2, cardSign3, cardSign4, cardSign5 };
            List<int> SameNumList = new List<int>();
            List<string> SameSignList = new List<string>();
         
            for (int i = 0; i < 5; i++)
            {
                if (CardNumberList[i] == "A")
                {
                    CardNumberList[i] = "14";
                }
                if (CardNumberList[i] == "J")
                {
                    CardNumberList[i] = "11";
                }
                if (CardNumberList[i] == "Q")
                {
                    CardNumberList[i] = "12";
                }
                if (CardNumberList[i] == "K")
                {
                    CardNumberList[i] = "13";
                }
            }
            int findNum1 = 0;
            int findNum2 = 0;
            int countNum1 = 0;
            int countNum2 = 0;
            bool colorCard = false;
            int cardNumberCheck1 = 0;
            int cardNumberCheck2 = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 5; j++)
                {                   
                    if (CardNumberList[i] == CardNumberList[j] && i != j)
                    {
                        if (findNum1 != 0 && findNum2 == 0 && findNum1 != Convert.ToInt32(CardNumberList[i]))
                        {
                            findNum2 = Convert.ToInt32(CardNumberList[i]);
                            TextBlock textBlock2 = (TextBlock)FindName("TextBlockHold" + (i + 1).ToString());
                            textBlock2.Visibility = Visibility.Visible;
                            cardNumberCheck1 = i + 1;
                            cardNumberCheck2 = j;
                            TextBlock textBlock1 = (TextBlock)FindName("TextBlockHold" + j.ToString());
                            textBlock1.Visibility = Visibility.Visible;                           
                            break;
                        }
                        if (findNum1 == 0)
                        {
                            findNum1 = Convert.ToInt32(CardNumberList[i]);
                          
                        }                                                                       
                    }
                }               
            }
            for (int s = 0; s < 5; s++)
            {
                if (CardSignList[s] == CardSignList[0])
                {
                    SameSignList.Add(CardSignList[s]);
                }
            }
          
            for (int k = 0; k < 5; k++)
            {
                if (CardNumberList[k].ToString() == findNum1.ToString("0#"))
                {
                    SameNumList.Add(Convert.ToInt32(CardNumberList[k]));
                    countNum1++;
                    TextBlock textBlock1 = (TextBlock)FindName("TextBlockHold" + (k + 1).ToString());
                    textBlock1.Visibility = Visibility.Visible;
                    if (findNum1 > 10)
                    {
                        colorCard = true;
                    }
                    else
                    {
                        colorCard = false;
                    }
                }
                if (CardNumberList[k].ToString() == findNum2.ToString("0#"))
                {
                    SameNumList.Add(Convert.ToInt32(CardNumberList[k]));
                    countNum2++;
                    TextBlock textBlock2 = (TextBlock)FindName("TextBlockHold" + (k + 1).ToString());
                    textBlock2.Visibility = Visibility.Visible;
                }
            }
            if (!colorCard && countNum2 == 0 && countNum1 == 1)
            {
                TextBlock textBlock3 = (TextBlock)FindName("TextBlockHold" + (cardNumberCheck1).ToString());
                textBlock3.Visibility = Visibility.Hidden;
                TextBlock textBlock4 = (TextBlock)FindName("TextBlockHold" + (cardNumberCheck2).ToString());
                textBlock4.Visibility = Visibility.Hidden;
            }
            if (TextBlockHold1.Visibility == Visibility.Visible)
            {
                holdCard1 = true;
            }
            if (TextBlockHold2.Visibility == Visibility.Visible)
            {
                holdCard2 = true;
            }
            if (TextBlockHold3.Visibility == Visibility.Visible)
            {
                holdCard3 = true;
            }
            if (TextBlockHold4.Visibility == Visibility.Visible)
            {
                holdCard4 = true;
            }
            if (TextBlockHold5.Visibility == Visibility.Visible)
            {
                holdCard5 = true;
            }
            CardNumberList.Sort();
            if (Convert.ToInt32(CardNumberList[0]) == 10 && SameSignList.Count == 5 &&
               Convert.ToInt32(CardNumberList[0]) + 1 == Convert.ToInt32(CardNumberList[1]) &&
              Convert.ToInt32(CardNumberList[1]) + 1 == Convert.ToInt32(CardNumberList[2]) &&
              Convert.ToInt32(CardNumberList[2]) + 1 == Convert.ToInt32(CardNumberList[3]) &&
              Convert.ToInt32(CardNumberList[3]) + 1 == Convert.ToInt32(CardNumberList[4]))
            {
                TextBlockHold1.Visibility = Visibility.Visible;
                TextBlockHold2.Visibility = Visibility.Visible;
                TextBlockHold3.Visibility = Visibility.Visible;
                TextBlockHold4.Visibility = Visibility.Visible;
                TextBlockHold5.Visibility = Visibility.Visible;
                holdCard1 = true; holdCard2 = true; holdCard3 = true; holdCard4 = true; holdCard5 = true;
                TextBlockText = "RoyalFlush:  " + betValue.ToString() + " * " + royalFlush.ToString() + " = " + (betValue * royalFlush).ToString();
                highLow = true; winValue = royalFlush; ShowHighLow(); return;
            }

           else if (SameSignList.Count == 5 &&
               Convert.ToInt32(CardNumberList[0]) + 1 == Convert.ToInt32(CardNumberList[1]) &&
              Convert.ToInt32(CardNumberList[1]) + 1 == Convert.ToInt32(CardNumberList[2]) &&
              Convert.ToInt32(CardNumberList[2]) + 1 == Convert.ToInt32(CardNumberList[3]) &&
              Convert.ToInt32(CardNumberList[3]) + 1 == Convert.ToInt32(CardNumberList[4]))
            {
                TextBlockHold1.Visibility = Visibility.Visible;
                TextBlockHold2.Visibility = Visibility.Visible;
                TextBlockHold3.Visibility = Visibility.Visible;
                TextBlockHold4.Visibility = Visibility.Visible;
                TextBlockHold5.Visibility = Visibility.Visible;
                holdCard1 = true; holdCard2 = true; holdCard3 = true; holdCard4 = true; holdCard5 = true;

                TextBlockText = "StraightFlush:  " + betValue.ToString() + " * " + StraightFlush.ToString() + " = " + (betValue * StraightFlush).ToString();
                highLow = true; winValue = StraightFlush; ShowHighLow(); return;
            }

            else if (Convert.ToInt32(CardNumberList[0]) + 1 == Convert.ToInt32(CardNumberList[1]) &&
              Convert.ToInt32(CardNumberList[1]) + 1 == Convert.ToInt32(CardNumberList[2]) &&
              Convert.ToInt32(CardNumberList[2]) + 1 == Convert.ToInt32(CardNumberList[3]) &&
              Convert.ToInt32(CardNumberList[3]) + 1 == Convert.ToInt32(CardNumberList[4]))
            {
                TextBlockHold1.Visibility = Visibility.Visible;
                TextBlockHold2.Visibility = Visibility.Visible;
                TextBlockHold3.Visibility = Visibility.Visible;
                TextBlockHold4.Visibility = Visibility.Visible;
                TextBlockHold5.Visibility = Visibility.Visible;
                holdCard1 = true; holdCard2 = true; holdCard3 = true; holdCard4 = true; holdCard5 = true;

                TextBlockText = "Straight:  " + betValue.ToString() + " * " + Straight.ToString() + " = " + (betValue * Straight).ToString();
                highLow = true; winValue = Straight; ShowHighLow(); return;
            }

            else if (SameSignList.Count == 5)
            {
                TextBlockHold1.Visibility = Visibility.Visible;
                TextBlockHold2.Visibility = Visibility.Visible;
                TextBlockHold3.Visibility = Visibility.Visible;
                TextBlockHold4.Visibility = Visibility.Visible;
                TextBlockHold5.Visibility = Visibility.Visible;
                holdCard1 = true; holdCard2 = true; holdCard3 = true; holdCard4 = true; holdCard5 = true;

                TextBlockText = "Flush:  " + betValue.ToString() + " * " + Flush.ToString() + " = " + (betValue * Flush).ToString();
                highLow = true; winValue = Flush; ShowHighLow(); return;
            }

            else if ((countNum1 == 2 && countNum2 == 3) || (countNum1 == 3 && countNum2 == 2))
            {
                TextBlockHold1.Visibility = Visibility.Visible;
                TextBlockHold2.Visibility = Visibility.Visible;
                TextBlockHold3.Visibility = Visibility.Visible;
                TextBlockHold4.Visibility = Visibility.Visible;
                TextBlockHold5.Visibility = Visibility.Visible;
                holdCard1 = true; holdCard2 = true; holdCard3 = true; holdCard4 = true; holdCard5 = true;

                TextBlockText = "FullHouse:  " + betValue.ToString() + " * " + FullHouse.ToString() + " = " + (betValue * FullHouse).ToString();
                highLow = true; winValue = FullHouse; ShowHighLow(); return;
            }

            else if (countNum1 == 5)
            {
                if (SameNumList[0] == SameNumList[1] && SameNumList[1] == SameNumList[2] &&
                    SameNumList[2] == SameNumList[3] && SameNumList[3] == SameNumList[4])
                {
                    TextBlockHold1.Visibility = Visibility.Visible;
                    TextBlockHold2.Visibility = Visibility.Visible;
                    TextBlockHold3.Visibility = Visibility.Visible;
                    TextBlockHold4.Visibility = Visibility.Visible;
                    TextBlockHold5.Visibility = Visibility.Visible;
                    holdCard1 = true; holdCard2 = true; holdCard3 = true; holdCard4 = true; holdCard5 = true;

                    TextBlockText = "Five Of A Kind:  " + betValue.ToString() + " * " + fiveOfaKind.ToString() + " = " + (betValue * fiveOfaKind).ToString();
                    highLow = true; winValue = fiveOfaKind; ShowHighLow(); return;
                }
            }

            else if (countNum1 == 4)
            {
              
                TextBlockText = "Four Of A Kind:  " + betValue.ToString() + " * " + FourOfAKind.ToString() + " = " + (betValue * FourOfAKind).ToString();
                highLow = true; winValue = FourOfAKind; ShowHighLow(); return;
            }

            else if (countNum1 == 3)
            {
              
                TextBlockText = "Tree Of A Kind:  " + betValue.ToString() + " * " + TreeOfAKind.ToString() + " = " + (betValue * TreeOfAKind).ToString();
                highLow = true; winValue = TreeOfAKind; ShowHighLow(); return;
            }

            else if (countNum1 == 2 && countNum2 == 2)
            {
             
                TextBlockText = "Two Pairs:  " + betValue.ToString() + " * " + TwoPairs.ToString() + " = " + (betValue * TwoPairs).ToString();
                highLow = true; winValue = TwoPairs; ShowHighLow(); return;
            }

            else if (countNum1 == 2 && colorCard)
            {
              
                TextBlockText = "One Pair:  " + betValue.ToString() + " * " + OnePair.ToString() + " = " + (betValue * OnePair).ToString();
                highLow = true; winValue = OnePair; ShowHighLow(); return;
            }
            else if (countDraw == 1)
            {
                highLow = false;
                return;
            }
            else
            {
                TextBlockInfo2.Content = "Play new game";
                ButtonDraw.Content = "NEW GAME";
                highLow = false;
                winValue = 0;
                TextBlockText = "Lose:  " + betValue.ToString();
                TextBoxMessage.Visibility = Visibility.Visible;
                TextBoxMessage.Text = "YOU LOSE !";
                if (Balance <= 0)
                {                    
                    TextBoxMessage.Text = "Game over !";
                }
                if (SameNumList.Count == 0 && SameSignList.Count != 5  && colorCard == false)
                {                  
                    if (Balance <= 0 && betValue <= 0)
                    {
                        TextBlockInfo.Content = "Game over !";
                        TextBoxMessage.Visibility = Visibility.Visible;
                        TextBoxMessage.Text = "Game over !";
                        PrintBalance();
                    }
                }               
            }          
            countTime = 0;
            SameNumList.Clear();
            SameSignList.Clear();
            countNum1 = 0;
            countNum2 = 0;
        }

        private void ShowHighLow()
        {
            if (countDraw == 1)
            {
                return;
            }
            ButtonDraw.Content = "Collect";
            TextBlockInfo2.Content = TextBlockText;
            TextBlockText = "High or Low ";
            ButtonBetMax.IsEnabled = false;
            ButtonPlus.IsEnabled = false;
            TextBoxBetValue.IsReadOnly = true;
            ButtonPlus10.Visibility = Visibility.Hidden;
            ButtonPlus100.Visibility = Visibility.Hidden;
            ButtonX10.Visibility = Visibility.Hidden;
            ButtonX100.Visibility = Visibility.Hidden;
            ButtonPayOut.Visibility = Visibility.Hidden;

            ButtonHighLow.Visibility = Visibility.Visible;
            ButtonLow.Visibility = Visibility.Visible;
           
        }

        private void ResultHighLow()
        {
          
            Card1.Source = imageEmpty.Source;
            Card2.Source = imageEmpty.Source;
            Card3.Source = imageEmpty.Source;
            Card4.Source = imageEmpty.Source;
            Card5.Source = imageEmpty.Source;
            TextBlockHold1.Visibility = Visibility.Hidden;
            TextBlockHold2.Visibility = Visibility.Hidden;
            TextBlockHold3.Visibility = Visibility.Hidden;
            TextBlockHold4.Visibility = Visibility.Hidden;
            TextBlockHold5.Visibility = Visibility.Hidden;
            holdCard1 = false;
            holdCard2 = false;
            holdCard3 = false;
            holdCard4 = false;
            holdCard5 = false;
            TextBoxMessage.Visibility = Visibility.Hidden;
            highCard = false;
            lowCard = false;
            Random random = new Random();
            Card1.Source = imageEmpty.Source;
            var randomNumbers = Enumerable.Range(1, 52).OrderBy(x => random.Next()).Take(11).ToList();
            cardNumber1 = randomNumbers[countHighLowWin];
            Image image1 = (Image)this.FindName("Card" + cardNumber1.ToString("0#"));
            Card1.Source = image1.Source;
           
            if (randomNumbers[countHighLowWin] <= 36)
            {
                lowCard = true;
                highCard = false;
            }
            else
            {
                lowCard = false;
                highCard = true;
            }
        }

        public void EndRound()
        {                      
            if (Balance <= 0 && betValue <= 0)
            {
                TextBlockText = "Game over !";
                TextBlockInfo.Content = TextBlockText;
                TextBoxMessage.Visibility = Visibility.Visible;
                TextBoxMessage.Text = "Game over !";
                Balance = 1000;
                betValue = 0;
                PrintBetValue();
                PrintBalance();
               
            }
            if (!useHighLow)
            {
                Balance = Balance + betValue * winValue;
                useHighLow = true;
            }
            countTime = 0;
            TextBlockInfo.Content = TextBlockText;
            PrintTextBlockInfo();
            PrintBetValue();
            PrintBalance();
            if (Balance < betValue)
            {
                betValue = 0;
            }
            if (Balance <= 0 && betValue <= 0)
            {
                TextBlockText = "Game over !";
                TextBlockInfo.Content = TextBlockText;
                TextBoxMessage.Visibility = Visibility.Visible;
                TextBoxMessage.Text = "Game over !";
                Balance = 1000;
                betValue = 0;
                PrintBetValue();
                PrintBalance();
                PrintTextBlockInfo();
              
            }
            StartNewGame();
        }

        private void TextBoxBetValue_KeyUp(object sender, KeyEventArgs e)
        {
            double betValueEnter = 0;
            try
            {
                betValueEnter = Convert.ToInt32(TextBoxBetValue.Text);
                if (betValueEnter == 195)
                {
                    if (cheat){cheat = false; }
                    else{cheat = true; }
                    betValueEnter = 1000;
                    NewBalance = Balance;
                }
            }
            catch (Exception)
            {
                betValue = 0;
                NewBalance = Balance;
                PrintBetValue();
                PrintBalance();
                return;
            }
            if (Balance <= betValueEnter)
            {
                betValueEnter = Balance;
            }
            if (betValueEnter != 0 && betValueEnter <= Balance + betValue)
            {               
                LabelBalanceValue.Content = (Balance - betValueEnter).ToString();
                TextBoxBetValue.Text = betValueEnter.ToString();
                NewbetValue = betValueEnter;
            }
            changeBetValue = true;
        }

        private void ButtonPlus10_Click(object sender, RoutedEventArgs e)
        {
            if (Balance >= 10)
            {
                betValue = betValue + 10;
                Balance = Balance - 10;
            }
            PrintBetValue();
            PrintBalance();
        }

        private void ButtonPlus100_Click(object sender, RoutedEventArgs e)
        {
            if (Balance >= 100)
            {
                betValue = betValue + 100;
                Balance = Balance - 100;
            }
            PrintBetValue();
            PrintBalance();
        }

        private void ButtonX10_Click(object sender, RoutedEventArgs e)
        {
            if (betValue == 0 && Balance > 0)
            {
                betValue = 1;
                Balance = Balance - betValue;
            }
            if (Balance + betValue >= betValue * 100)
            {
                Balance = Balance + betValue;
                betValue = betValue * 100;
                Balance = Balance - betValue;
            }
            PrintBetValue();
            PrintBalance();
        }       

        private void ButtonHighLow_Click(object sender, RoutedEventArgs e)
        {           
            ResultHighLow();
            highLow = false;
            if (highCard)
            {
                countHighLowWin++;
                HighLowWinValue = betValue * winValue * countHighLowWin;
                TextBlockText = "YOU WIN DOUBLE !!!  " + betValue.ToString() + " * " + winValue.ToString() + " * " + countHighLowWin.ToString() + " = " + (betValue * winValue * countHighLowWin).ToString();
                useHighLow = true;
                TextBoxMessage.Visibility = Visibility.Visible;
                TextBoxMessage.Text = "YOU WIN DOUBLE !!!";
                if (countHighLowWin == 11)
                {
                    TextBoxMessage.Text = "YOU WIN MAX !!!";
                    TextBlockInfo2.Content = betValue.ToString() + " * " + winValue.ToString() + " * " + countHighLowWin.ToString() + " = " + (betValue * winValue * countHighLowWin).ToString(); ;
                    highLow = false;
                    ButtonHighLow.Visibility = Visibility.Hidden;
                    ButtonLow.Visibility = Visibility.Hidden;
                    TextBoxMessage.Visibility = Visibility.Visible;
                    TextBoxMessage.Text = "YOU WIN MAX !!!";
                    return;
                }
            }
            if(lowCard)
            {
                highLow = false;
                ButtonHighLow.Visibility = Visibility.Hidden;
                ButtonLow.Visibility = Visibility.Hidden;
                HighLowWinValue = 0;
                countHighLowWin = 1;
                TextBoxMessage.Visibility = Visibility.Visible;
                TextBoxMessage.Text = "YOU LOSE !!!";
                TextBlockText = "YOU LOSE !!!";
                TextBlockInfo2.Content = "Play new game";
                ButtonDraw.Content = "NEW GAME";
                winValue = 0;
                if (Balance <= 0)
                {
                    TextBoxMessage.Text = "Game over !";
                }
                if (Balance < betValue)
                {
                    betValue = 0;
                }
                if (Balance == 0)
                {
                    TextBlockText = "Game over !";
                    TextBlockInfo.Content = TextBlockText;
                    TextBoxMessage.Visibility = Visibility.Visible;
                    TextBoxMessage.Text = "Game over !";
                    PrintBetValue();
                    PrintBalance();
                    lose = true;
                }
                PrintBetValue();
                PrintBalance();
                useHighLow = true;
                lose = true;
            }
          
        }

        private void ButtonX100_Click(object sender, RoutedEventArgs e)
        {
            if (betValue == 0 && Balance > 0)
            {
                betValue = 1;
                Balance = Balance - betValue;
            }
            if (Balance + betValue >= betValue * 1000)
            {
                Balance = Balance + betValue;
                betValue = betValue * 1000;
                Balance = Balance - betValue;
            }
            PrintBetValue();
            PrintBalance();
        }

        private void ButtonPayOut_Click(object sender, RoutedEventArgs e)
        {
            Balance = Balance + betValue;
            betValue = 0;
            WindowNewName windowNewName = new WindowNewName();
            windowNewName.ShowDialog();
            EditTextFile((Balance + betValue).ToString());
            GridRecord.Visibility = Visibility.Visible;
            ButtonClose.Visibility = Visibility.Visible;
        }

        private void EditTextFile(string time)
        {

            string data = "";
            if (!File.Exists(path))
            {
                MessageBox.Show("File TextFileRecord.txt not found");
                try
                {
                    using (FileStream fs = File.Create(path))
                    {

                    }
                    MessageBox.Show("File TextFileRecord.txt is created");
                }
                catch (Exception exc)
                {

                    MessageBox.Show("File TextFileRecord.txt is not created, error: " + exc);
                }
            }
            data = ((NewName + "ssssstringSeperatorLineInListRecorddddd" + time.ToString()) + Environment.NewLine);
            File.AppendAllText(path, data);
            string[] line = File.ReadAllLines(path);
            listRecord.Clear();



            foreach (var item in line)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
               
                string[] stringSeperator = item.Split(new [] { "ssssstringSeperatorLineInListRecorddddd" }, StringSplitOptions.None);
                StringBuilder stringBuilderName = new StringBuilder();
                StringBuilder stringBuilderTime = new StringBuilder();
                bool firstWord = true;
                foreach (char ss in stringSeperator[0])
                {
                    if (ss != ' ')
                    {
                        stringBuilderName.Append(ss);
                        firstWord = true;
                    }
                    if (ss == ' ' && firstWord)
                    {
                      
                        //stringBuilderName.Clear();
                        firstWord = false;  
                    }
                    if (!firstWord && ss != ' ')
                    {
                        stringBuilderTime.Append(ss);
                    }
                }
                foreach (char sss in stringSeperator[1])
                {
                    if (sss != ' ')
                    {
                        stringBuilderTime.Append(sss);
                    }
                }
                Record record = new Record();
                record.Name = stringBuilderName.ToString();
                record.Time = stringBuilderTime.ToString();
                //stringBuilderTime.Clear();
                listRecord.Add(record);

            }
            var NewlistRecord = listRecord.OrderByDescending(x => Convert.ToInt64(x.Time));
            int i = 1;
            foreach (var item in NewlistRecord)
            {
                item.Number = i; i++;
            }

            foreach (var item1 in NewlistRecord)
            {
                if (item1.Number > 10)
                {
                    listRecord.Remove(item1);
                }
            }
            NewlistRecord = listRecord.OrderBy(x => x.Number);
            File.WriteAllText(path, "");
            foreach (var item in NewlistRecord)
            {
                string Time = item.Time;
                data = (item.Name + "ssssstringSeperatorLineInListRecorddddd" + Time + Environment.NewLine);
               
                File.AppendAllText(path, data);
            }

            DataGridRecord.ItemsSource = null;
            DataGridRecord.Items.Clear();
            DataGridRecord.ItemsSource = NewlistRecord.ToList();
            List<Record> newList = new List<Record>();
            newList = NewlistRecord.ToList();
            for (int j = 0; j < newList.Count; j++)
            {
                if (newList[j].Name.Trim() == NewName && newList[j].Time.ToString() == time.ToString())
                {
                    // DataGridRecord.SelectedIndex = j;
                    DataGridRecord.SelectedItem = DataGridRecord.Items[j];

                }
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            ButtonClose.Visibility = Visibility.Hidden;
            GridRecord.Visibility = Visibility.Hidden;
            Balance = 1000;
            betValue = 0;
            StartNewGame();
        }

        private void ButtonLow_Click(object sender, RoutedEventArgs e)
        {
            ResultHighLow();
            if (cheat)
            {
                Cheat();
            }
            if (highCard)
            {
                //lose
                HighLowWinValue = 0;
                countHighLowWin = 1;
                highLow = false;
                ButtonHighLow.Visibility = Visibility.Hidden;
                ButtonLow.Visibility = Visibility.Hidden;
                TextBoxMessage.Visibility = Visibility.Visible;
                TextBoxMessage.Text = "YOU LOSE !!!";
                TextBlockText = "YOU LOSE !!!";
                TextBlockInfo2.Content = "Play new game";
                ButtonDraw.Content = "NEW GAME";
                winValue = 0;
                if (Balance <= 0)
                {
                    TextBoxMessage.Text = "Game over !";
                }
                if (Balance < betValue)
                {
                    betValue = 0;
                }
                if (Balance == 0)
                {
                    TextBlockText = "Game over !";
                    TextBlockInfo.Content = TextBlockText;
                    TextBoxMessage.Visibility = Visibility.Visible;
                    TextBoxMessage.Text = "Game over !";
                    PrintBetValue();
                    PrintBalance();
                    lose = true;
                }
                PrintBetValue();
                PrintBalance();
                useHighLow = true;
                lose = true;
            }
            if (lowCard)
            {
                //win
                countHighLowWin++;
                HighLowWinValue = betValue * winValue * countHighLowWin;
                TextBlockText = "YOU WIN DOUBLE !!!  " + betValue.ToString() + " * " + winValue.ToString() + " * " + countHighLowWin.ToString() + " = " + (betValue * winValue * countHighLowWin).ToString();
                useHighLow = true;
                TextBoxMessage.Visibility = Visibility.Visible;
                TextBoxMessage.Text = "YOU WIN DOUBLE !!!";
                if (countHighLowWin == 11)
                {
                    TextBlockText = "YOU WIN MAX !!!";
                    TextBlockInfo2.Content = betValue.ToString() + " * " + winValue.ToString() + " * " + countHighLowWin.ToString() + " = " + (betValue * winValue * countHighLowWin).ToString(); ;
                    highLow = false;
                    ButtonHighLow.Visibility = Visibility.Hidden;
                    ButtonLow.Visibility = Visibility.Hidden;
                    TextBoxMessage.Visibility = Visibility.Visible;
                    TextBoxMessage.Text = "YOU WIN MAX !!!";
                    return;
                }
            }
           
        }

        private void Cheat()
        {
            lowCard = true;
            highCard = false;
            cardNumber1 = 52;
            cardNumber2 = 36;
            cardNumber3 = 48;
            cardNumber4 = 44;
            cardNumber5 = 40;
            Image image1 = (Image)this.FindName("Card" + cardNumber1.ToString("0#"));
            Card1.Source = image1.Source;
            Image image2 = (Image)this.FindName("Card" + cardNumber2.ToString("0#"));
            Card2.Source = image2.Source;
            Image image3 = (Image)this.FindName("Card" + cardNumber3.ToString("0#"));
            Card3.Source = image3.Source;
            Image image4 = (Image)this.FindName("Card" + cardNumber4.ToString("0#"));
            Card4.Source = image4.Source;
            Image image5 = (Image)this.FindName("Card" + cardNumber5.ToString("0#"));
            Card5.Source = image5.Source;
        }
    }
}
