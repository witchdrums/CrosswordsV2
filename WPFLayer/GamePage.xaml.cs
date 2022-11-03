using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFLayer.ServicesImplementation;

namespace WPFLayer
{
    /* by witchdrums
     * TO DO
        [   ] Turn into PAGE
        [   ] Connect this with the lobby window
     */
    public partial class GamePage : Window, ServicesImplementation.IMessagesCallback
    {
        private UniformGrid crosswordGrid;
        private Label[,] labelMatrix;
        private char[,] wordMatrix;
        private Board board;
        List<Label> selectedWordLabels;
        private List<ServicesImplementation.Users> players = new List<ServicesImplementation.Users>();
        private Users userlogin;
        private int idRoom;

        public GamePage()
        {
            this.labelMatrix = new Label[20, 20];
            this.wordMatrix = new char[20, 20];
            this.selectedWordLabels = new List<Label>();
            Brush colorSorroundingLetters = System.Windows.Media.Brushes.Gray;
            InitializeComponent();
            InitializeBoard(colorSorroundingLetters);
            //InitializeWordList(colorSorroundingLetters);


            GetBoard();
            //ParseWordMatrix();
            PlaceWordsInBoard();
        }

        private void GetBoard()
        {
            ServicesImplementation.GameManagementClient client = new ServicesImplementation.GameManagementClient();
            this.board = client.GetBoardById(1);
            foreach (WordsBoard wordsBoards in board.WordsBoards)
            {
                ListViewItem wordItem = new ListViewItem();
                wordItem.Tag = wordsBoards;
                wordItem.Content = wordsBoards.Word.clue;
                this.ListView_HorizontalClueList.Items.Add(wordItem);
            }

        }


        private void InitializeBoard(Brush colorSorroundingLetters)
        {
            int gridSize = 20;
            this.crosswordGrid = new UniformGrid() { Columns = gridSize, Rows = gridSize };
            for (int rowIndex = 0; rowIndex < gridSize; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < gridSize; columnIndex++)
                {
                    Thickness borderThickness = new Thickness(1, 1, 1, 1);
                    Label cell = new Label()
                    {
                        BorderBrush = colorSorroundingLetters,
                        BorderThickness = borderThickness,
                        Background = colorSorroundingLetters,
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    };
                    labelMatrix[columnIndex, rowIndex] = cell;
                    this.crosswordGrid.Children.Add(cell);
                }
            }
            this.Grid_CrosswordPanel.Children.Add(crosswordGrid);
        }

        private void TextBox_WordGuess_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
            this.selectedWordLabels.ForEach(label => label.Content = "");
            for (int xIndex = 0; xIndex < this.TextBox_WordGuess.Text.Length; xIndex++)
            {
                if (this.selectedWordLabels.ElementAt(xIndex).Content == "")
                {
                    this.selectedWordLabels.ElementAt(xIndex).Content = this.TextBox_WordGuess.Text[xIndex];
                }
            }*/

        }

        private void ListView_HorizontalClueList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeselectAllLettersInBoard();
            WordsBoard selectedWord = GetSelectedWordInClueList();
            InitializeTextBoxToSolveWord(selectedWord);
            this.TextBox_WordGuess.Focus();

            int xIndex = selectedWord.xStart; ;
            int yIndex = selectedWord.yStart;

            foreach (char letter in selectedWord.Word.term)
            {
                this.labelMatrix[xIndex, yIndex].Background = System.Windows.Media.Brushes.Red;
                this.selectedWordLabels.Add(this.labelMatrix[xIndex, yIndex]);
                bool wordIsHorizontal = selectedWord.yStart == selectedWord.yEnd;
                if (wordIsHorizontal)
                {
                    xIndex++;
                }
                else
                {
                    yIndex++;
                }
            }

        }

        private void InitializeTextBoxToSolveWord(WordsBoard selectedWord)
        {
            this.TextBox_WordGuess.IsEnabled = !selectedWord.Word.isSolved;
            this.TextBox_WordGuess.MaxLength = selectedWord.Word.term.Length;
        }

        private void DeselectAllLettersInBoard()
        {

            for (int rows = 0; rows < 20; rows++)
            {
                for (int columns = 0; columns < 20; columns++)
                {
                    Label currentLabel = this.labelMatrix[rows, columns];
                    if (currentLabel.Background == System.Windows.Media.Brushes.Red)
                    {
                        currentLabel.Background = System.Windows.Media.Brushes.White;
                        this.selectedWordLabels.Remove(currentLabel);
                    }

                }
            }

        }

        private void PlaceWordsInBoard()
        {

            foreach (WordsBoard word in board.WordsBoards)
            {
                int xIndex = word.xStart;
                int yIndex = word.yStart;
                foreach (char letter in word.Word.term)
                {
                    Label currentLabel = this.labelMatrix[xIndex, yIndex];
                    currentLabel.Background = System.Windows.Media.Brushes.White;
                    currentLabel.FontSize = 15;
                    if (word.yStart == word.yEnd)
                    {
                        xIndex++;
                    }
                    else
                    {
                        yIndex++;
                    }
                }

            }
        }

        private void Button_Guess_Click(object sender, RoutedEventArgs e)
        {
            String playerGuess = this.TextBox_WordGuess.Text.ToUpperInvariant();
            WordsBoard selectedWord = GetSelectedWordInClueList();

            if (playerGuess == selectedWord.Word.term)
            {
                selectedWord.Word.isSolved = true;
                ListViewItem item = (ListViewItem)this.ListView_HorizontalClueList.SelectedItem;
                item.Foreground = System.Windows.Media.Brushes.LightGray;
                for (int letterIndex = 0; letterIndex < playerGuess.Length; letterIndex++)
                {
                    this.selectedWordLabels.ElementAt(letterIndex).Content = playerGuess[letterIndex];
                    this.TextBox_WordGuess.Clear();
                }
            }
        }

        private WordsBoard GetSelectedWordInClueList()
        {
            ListViewItem item = (ListViewItem)this.ListView_HorizontalClueList.SelectedItem;
            WordsBoard selectedWord = (WordsBoard)item.Tag;
            return selectedWord;
        }

        public void ReciveChatMessage(Users userOrigin, string message)
        {
            TextBlock_Chat.Text += userOrigin.username + " : " + message + "\n";
        }

        private void Button_SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TextBox_Message.Text))
            {
                InstanceContext context = new InstanceContext(this);
                ServicesImplementation.IMessages messages = new ServicesImplementation.MessagesClient(context);
                messages.SendChatMessage(this.players.ToArray(), this.userlogin, TextBox_Message.Text);
            }
        }
    }
}
