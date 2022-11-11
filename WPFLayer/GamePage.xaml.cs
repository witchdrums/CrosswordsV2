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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WPFLayer.ServicesImplementation;

namespace WPFLayer
{
    /* by witchdrums
     * TO DO
        [   ] Turn into PAGE
        [   ] Connect this with the lobby window
     */
    public partial class GamePage : Window, IMessagesCallback, IGameRoomManagementCallback, IGameManagementCallback, IUsersManagerCallback
    {
        private DispatcherTimer gameTimer = new DispatcherTimer();
        private TimeSpan timeSpan = new TimeSpan();
        private UniformGrid crosswordGrid;
        private Label[,] labelMatrix = new Label[20, 20];
        private ServicesImplementation.Boards board;
        private List<Label> selectedWordLabels = new List<Label>();
        private List<Label> selectedWordLabelsCopy = new List<Label>();
        private bool hasTurn;
        private int currentPlayerIndex = 0;

        //room info
        private List<ServicesImplementation.GamesPlayers> gamePlayersRoom = 
            new List<ServicesImplementation.GamesPlayers>();
        private List<ServicesImplementation.Users> usersRoom =
            new List<ServicesImplementation.Users>();
        private Users userLogin;
        private int idRoom;

        public GamePage(Users userLogin, int idRoom, List<Users> usersRoom) // should receive room info as parameters
        {
            this.userLogin = userLogin;
            this.idRoom = idRoom;
            this.usersRoom = usersRoom;
            GetGamePlayersRoom(usersRoom);
            JoinGame();

            Brush colorSorroundingLetters = System.Windows.Media.Brushes.Gray;
            InitializeComponent();
            GameSetup();
            
            InitializeBoard(colorSorroundingLetters);
            //InitializeWordList(colorSorroundingLetters);

            GetBoard();
            //ParseWordMatrix();
            PlaceAllWordsInBoard();
            
        }

        private void GetGamePlayersRoom(List<Users> usersRoom)
        {
            ServicesImplementation.UsersManagerClient userManagerclient = new UsersManagerClient(new InstanceContext(this));

            foreach (Users user in usersRoom)
            {
                Players userPlayer = new Players();
                userPlayer.user = user;
                userPlayer = userManagerclient.GetPlayerInformation(userPlayer);
                

                GamesPlayers gamesPlayer = new GamesPlayers();
                gamesPlayer.idPlayer = userPlayer.idPlayer;
                gamesPlayer.Player = userPlayer;

                this.gamePlayersRoom.Add(gamesPlayer);
            }
        }

        private void GameSetup()
        {
            timeSpan = TimeSpan.FromSeconds(20);
            gameTimer.Interval = TimeSpan.FromSeconds(1);

            gameTimer.Tick += SecondPasses;
            gameTimer.Start();
            this.ListView_HorizontalClueList.SelectedIndex = 0;
            IsMyTurn(false);
            GameManagementClient gameManagementClient = GetGameManagementClient();
            gameManagementClient.GetFirstTurn(this.gamePlayersRoom.ToArray());
        }

        private void RestartGameTimer()
        {
            timeSpan = TimeSpan.FromSeconds(6);
            gameTimer.Start();
        }

        private void SecondPasses(object sender, EventArgs e)
        {
            if (timeSpan == TimeSpan.FromSeconds(1)) 
            {
                
                PassTurn();
            }
            else if (hasTurn)
            {
                timeSpan = timeSpan.Add(TimeSpan.FromSeconds(-1));
                this.Label_Timer.Content = timeSpan.ToString("c");
            }
        }


        private void JoinGame()
        {


            InstanceContext instanceContext = new InstanceContext(this);
            GameManagementClient gameManagementClient = new GameManagementClient(instanceContext);
            MessagesClient messagesClient = new MessagesClient(instanceContext);
            GameRoomManagementClient roomManagementClient = new GameRoomManagementClient(instanceContext);
            gameManagementClient.JoinGame(this.userLogin);
            messagesClient.ConnectMessages(this.userLogin);
        }

        private GameManagementClient GetGameManagementClient()
        {
            InstanceContext instanceContext = new InstanceContext(this);
            return new GameManagementClient(instanceContext);
        }

        private void GetBoard()
        {
            ServicesImplementation.GameManagementClient client = GetGameManagementClient();
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
                    Thickness padding = new Thickness(-10);
                    Label cell = new Label()
                    {
                        BorderBrush = colorSorroundingLetters,
                        BorderThickness = borderThickness,
                        Background = colorSorroundingLetters,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        FontWeight = FontWeights.Bold,
                        Content = "",
                        Padding = padding,
                    };
                    labelMatrix[columnIndex, rowIndex] = cell;
                    this.crosswordGrid.Children.Add(cell);
                }
            }
            this.Grid_CrosswordPanel.Children.Add(crosswordGrid);
        }

        private void TextBox_WordGuess_TextChanged(object sender, TextChangedEventArgs e)
        {

            this.selectedWordLabels.ForEach(label => label.Content = "");
            for (int xIndex = 0; xIndex < this.TextBox_WordGuess.Text.Length; xIndex++)
            {

                if (xIndex < this.selectedWordLabels.Count && this.selectedWordLabels.ElementAt(xIndex).Content.ToString() == "")
                {
                    this.selectedWordLabels.ElementAt(xIndex).Content = this.TextBox_WordGuess.Text[xIndex];
                }
            }

        }

        private void ListView_HorizontalClueList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectWordLabels();
        }

        private void SelectWordLabels()
        {
            DeselectAllBoardLabels();

            WordsBoard selectedWord = GetSelectedWordInClueList();
            ClearSelectedWordLabels(selectedWord);
            InitializeTextBoxToSolveWord(selectedWord);

            int xIndex = selectedWord.xStart;
            int yIndex = selectedWord.yStart;

            foreach (char letter in selectedWord.Word.term)
            {
                Label selectedWordLabel = this.labelMatrix[xIndex, yIndex];
                selectedWordLabel.Background = System.Windows.Media.Brushes.Red;

                this.selectedWordLabels.Add(selectedWordLabel);
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

            CopySelectedWordLabels();
        }

        private void CopySelectedWordLabels()
        {
            foreach (Label wordLabel in selectedWordLabels)
            {
                Label wordLabelCopy = new Label();
                wordLabelCopy.Content = wordLabel.Content;
                selectedWordLabelsCopy.Add(wordLabelCopy);
            }


        }

        private void InitializeTextBoxToSolveWord(WordsBoard selectedWord)

        {
            this.TextBox_WordGuess.IsEnabled = false;
            if (!selectedWord.Word.isSolved && hasTurn)
            {
                this.TextBox_WordGuess.Text = "";
                this.TextBox_WordGuess.IsEnabled = true;
                this.TextBox_WordGuess.MaxLength = selectedWord.Word.term.Length;
            }
        }

        private void DeselectAllBoardLabels()
        {

            for (int rows = 0; rows < 20; rows++)
            {
                for (int columns = 0; columns < 20; columns++)
                {
                    Label currentLabel = this.labelMatrix[rows, columns];
                    if (currentLabel.Background == System.Windows.Media.Brushes.Red)
                    {
                        currentLabel.Background = System.Windows.Media.Brushes.White;

                    }

                }
            }

        }



        private void PlaceAllWordsInBoard()
        {

            foreach (WordsBoard word in board.WordsBoards)
            {
                PlaceWordInBoard(word);
            }
        }


        private void Button_Guess_Click(object sender, RoutedEventArgs e)
        {
            GuessWord();
        }

        private void GuessWord()
        {
            String playerGuess = this.TextBox_WordGuess.Text.ToUpperInvariant();
            WordsBoard selectedWord = GetSelectedWordInClueList();

            //IsMyTurn(false);
            if (playerGuess == selectedWord.Word.term)
            {


                selectedWord.Word.isSolved = true;
                InstanceContext instanceContext = new InstanceContext(this);
                GameManagementClient gameManagementClient = new GameManagementClient(instanceContext);
                gameManagementClient.SendSolvedWordsBoard(this.usersRoom.ToArray(), this.userLogin, selectedWord);
                PassTurn();
                ClearSelectedWordLabels(selectedWord);
                RestartGameTimer();
            }
            else
            {
                this.selectedWordLabelsCopy.ForEach(label => Console.WriteLine(label.Content));
                RestoreIntersectedWords();
            }

            this.TextBox_WordGuess.Text = "";
        }

        private void IsMyTurn(bool isMyTurn)
        {
            this.hasTurn = isMyTurn;

            this.TextBox_WordGuess.IsEnabled = hasTurn;
            this.Button_Guess.IsEnabled = hasTurn;
        }

        private void ClearSelectedWordLabels(WordsBoard word)
        {
            Console.WriteLine(word.Word.term);
            Console.WriteLine(word.Word.isSolved);



            this.selectedWordLabels.Clear();
            this.selectedWordLabelsCopy.Clear();
        }


        private WordsBoard GetSelectedWordInClueList()
        {
            ListViewItem item = (ListViewItem)this.ListView_HorizontalClueList.SelectedItem;
            WordsBoard selectedWord = (WordsBoard)item.Tag;
            return selectedWord;
        }

        public void ReciveChatMessage(Users userOrigin, string message)
        {
            this.TextBlock_Chat.Items.Add(userOrigin.username + ": " + message);
            this.ScrollViewer_Chat.ScrollToBottom();

        }

        private void Button_SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TextBox_Message.Text))
            {
                InstanceContext context = new InstanceContext(this);
                ServicesImplementation.IMessages messages = new ServicesImplementation.MessagesClient(context);
                messages.SendChatMessage(this.usersRoom.ToArray(), this.userLogin, TextBox_Message.Text);
            }
        }

        public void ReceiveSolvedWordsBoard(Users usersOrigin, WordsBoard solvedWordsBoard)
        {
            WordsBoard localSolvedWordsBoard = new WordsBoard();
            ListViewItem solvedItem = new ListViewItem();
            foreach (ListViewItem item in ListView_HorizontalClueList.Items)
            {
                localSolvedWordsBoard = (WordsBoard)item.Tag;
                if (localSolvedWordsBoard.Word.term == solvedWordsBoard.Word.term)
                {
                    solvedItem = item;
                    localSolvedWordsBoard.Word.isSolved = true;
                    break;
                }
            }

            solvedItem.Foreground = System.Windows.Media.Brushes.LightGray;

            PlaceWordInBoard(localSolvedWordsBoard);
        }

        private void PlaceWordInBoard(WordsBoard word)
        {
            int xIndex = word.xStart;
            int yIndex = word.yStart;

            foreach (char letter in word.Word.term)
            {
                Label currentLabel = this.labelMatrix[xIndex, yIndex];
                currentLabel.Background = System.Windows.Media.Brushes.White;

                if (word.Word.isSolved)
                {
                    currentLabel.Content = letter;
                }
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
        private void OnKeyDownHandler(object sender, KeyEventArgs keyEvent)
        {
            if (keyEvent.Key == Key.Enter)
            {
                GuessWord();
            }
            else if ((keyEvent.Key == Key.Delete) || (keyEvent.Key == Key.Back))
            {
                Console.WriteLine("Backspace");
                this.TextBox_WordGuess.Text = "";

                RestoreIntersectedWords();
            }
        }

        private void RestoreIntersectedWords()
        {
            for (int selectedWordLabelIndex = 0; selectedWordLabelIndex < selectedWordLabelsCopy.Count; selectedWordLabelIndex++)
            {

                this.selectedWordLabels.ElementAt(selectedWordLabelIndex).Content =
                    this.selectedWordLabelsCopy.ElementAt(selectedWordLabelIndex).Content;
            }
        }

        private void PassTurn()
        {
            RestartGameTimer();
            IsMyTurn(false);
            InstanceContext instanceContext = new InstanceContext(this);
            GameManagementClient gameManagementClient = new GameManagementClient(instanceContext);
            gameManagementClient.PassTurn(this.gamePlayersRoom.ToArray(), this.currentPlayerIndex);
        }

        public void ReciveInvitationToRoom(int idRoom)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoom(Users[] usersInRoom)
        {
            throw new NotImplementedException();
        }

        public void ForceExitToRoom()
        {
            throw new NotImplementedException();
        }

        public void EnterGame()
        {
            throw new NotImplementedException();
        }

        public void Response([MessageParameter(Name = "response")] bool response1)
        {
            throw new NotImplementedException();
        }

        public void ReceiveTurn()
        {
            IsMyTurn(true);
        }

        public void SetCurrentPlayerIndex(int currentPlayerIndex)
        {
            this.currentPlayerIndex = currentPlayerIndex;
        }
    }
}
