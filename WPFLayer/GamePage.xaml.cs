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
        private Label[,] labelMatrix = new Label[20, 20];
        private ServicesImplementation.Boards board;
        private List<Label> selectedWordLabels = new List<Label>();
        private List<Label> selectedWordLabelsCopy = new List<Label>();
        private List<TextBlock> enemyNames = new List<TextBlock>();
        private List<Label> enemyScores = new List<Label>();
        private List<GamesPlayers> enemies;
        private bool hasTurn;
        private int remainingTurns = 0;

        //room info
        private Queue<GamesPlayers> gamePlayersQueue = new Queue<GamesPlayers>();
        private List<ServicesImplementation.Users> usersRoom =
            new List<ServicesImplementation.Users>();
        private Users userLogin;
        private int idRoom;

        private GamesPlayers player;
        private GameConfiguration gameConfiguration;

        public GamePage(Users userLogin, int idRoom, GameConfiguration gameConfiguration) // should receive room info as parameters
        {
            InitializeComponent();
            this.userLogin = userLogin;
            this.idRoom = idRoom;
            this.gameConfiguration = gameConfiguration;
            this.remainingTurns = gameConfiguration.TurnAmount;
            GetGamePlayer();
            JoinGame();

            Brush colorSorroundingLetters = System.Windows.Media.Brushes.Gray;
            GameSetup();
            InitializeEnemyPortraits();
            InitializeBoard(colorSorroundingLetters);
            GetBoard();
            PlaceAllWordsInBoard();
            EndTurn();
        }

        private void GetGamePlayer()
        {
            this.gamePlayersQueue = gameConfiguration.GamePlayerQueue;
            foreach (GamesPlayers gamePlayer in gamePlayersQueue)
            {
                if (gamePlayer.Player.user.idUser == this.userLogin.idUser)
                {
                    this.player = gamePlayer;
                }
            }
        }

        private void GameSetup()
        {
            timeSpan = TimeSpan.FromSeconds(1);
            gameTimer.Interval = TimeSpan.FromSeconds(1);

            gameTimer.Tick += SecondPasses;
            gameTimer.Start();
            this.ListView_HorizontalClueList.SelectedIndex = 0;
            
            IsMyTurn(false);
        }

        private void RestartGameTimer()
        {
            timeSpan = TimeSpan.FromSeconds(30);
            gameTimer.Start();
        }

        private void SecondPasses(object sender, EventArgs e)
        {
            if (timeSpan == TimeSpan.FromSeconds(1)) 
            {
                
                EndTurn();
            }
            else if (hasTurn)
            {
                timeSpan = timeSpan.Add(TimeSpan.FromSeconds(-1));
                this.Label_Timer.Content = timeSpan.ToString("c");
            }
        }


        private void JoinGame()
        {

            this.TextBlock_Player.Text = this.player.Player.playerName;
            this.Label_PlayerScore.Content = this.player.gameScore;
            InstanceContext instanceContext = new InstanceContext(this);
            GameManagementClient gameManagementClient = new GameManagementClient(instanceContext);
            MessagesClient messagesClient = new MessagesClient(instanceContext);
            GameRoomManagementClient roomManagementClient = new GameRoomManagementClient(instanceContext);
            gameManagementClient.JoinGame(this.player);
            messagesClient.ConnectMessages(this.userLogin);
        }

        private void InitializeEnemyPortraits()
        {
            this.enemies = 
                this.gamePlayersQueue.Where(enemy => enemy.Player.idPlayer != this.player.Player.idPlayer).ToList();
            this.enemyNames.Add(this.TextBlock_Enemy1);
            this.enemyNames.Add(this.TextBlock_Enemy2);
            this.enemyNames.Add(this.TextBlock_Enemy3);

            this.enemyScores.Add(this.Label_Enemy1Score);
            this.enemyScores.Add(this.Label_Enemy2Score);
            this.enemyScores.Add(this.Label_Enemy3Score);

            enemyScores.ForEach(label => label.Tag = 0);

            for (int enemyIndex = 0; enemyIndex < enemies.Count; enemyIndex++)
            {
                enemyNames[enemyIndex].Text = enemies[enemyIndex].Player.playerName;
                enemyScores[enemyIndex].Tag = enemies[enemyIndex].Player.idPlayer;
            }
        }

        private void AddScoreToSolver(GamesPlayers solver)
        {
            Console.WriteLine("addscore");
            foreach (Label enemyScore in this.enemyScores)
            {
                if (((int)enemyScore.Tag) == solver.idPlayer)
                {
                    enemyScore.Content = solver.gameScore;
                    break;
                }
            }
        }

        private GameManagementClient GetGameManagementClient()
        {
            InstanceContext instanceContext = new InstanceContext(this);
            return new GameManagementClient(instanceContext);
        }

        private void GetBoard()
        {
            this.board = this.gameConfiguration.Board;
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
            UniformGrid crosswordGrid = new UniformGrid() { Columns = gridSize, Rows = gridSize };
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
                    crosswordGrid.Children.Add(cell);
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

                AddScore(selectedWord);


                selectedWord.Word.isSolved = true;
                GetGameManagementClient().SendSolvedWordsBoard(this.gamePlayersQueue, this.player, selectedWord);
                
                ClearSelectedWordLabels(selectedWord);
            }
            else
            {
                this.selectedWordLabelsCopy.ForEach(label => Console.WriteLine(label.Content));
                RestoreIntersectedWords();
            }

            EndTurn();
            this.TextBox_WordGuess.Text = "";
        }

        private void AddScore(WordsBoard solvedWord)
        {
            int score = solvedWord.Word.term.Length * 100;
            this.player.gameScore += score;               
            this.Label_PlayerScore.Content = this.player.gameScore;
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

        public void ReceiveSolvedWordsBoard(GamesPlayers solver, WordsBoard solvedWordsBoard)
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
            AddScoreToSolver(solver);
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

        private void EndTurn()
        {
            RestartGameTimer();
            IsMyTurn(false);
            if (ThereAreWordsToBeSolvedStill() && remainingTurns > 0)
            {
                GetGameManagementClient().PassTurn(this.gamePlayersQueue, this.remainingTurns);
            }
            else 
            {
                FinishGame();
            }
            
        }

        private void FinishGame()
        {
            MessageBox.Show("das it mayne");
        }

        private bool ThereAreWordsToBeSolvedStill()
        {
            int wordsToBeSolved = 0;
            foreach (ListViewItem item in this.ListView_HorizontalClueList.Items)
            {
                WordsBoard word = item.Tag as WordsBoard;
                if (word.Word.isSolved == false)
                {
                    wordsToBeSolved += 1;
                }
                
            }
            return wordsToBeSolved > 0;
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

        public void EnterGame(GameConfiguration gameConfiguration)
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


        public void UpdateGamePlayersQueue(Queue<GamesPlayers> gamePlayers, int remainingTurns)
        {
            this.gamePlayersQueue = gamePlayers;
            this.remainingTurns = remainingTurns;
            this.Label_Turns.Content = remainingTurns;
        }
    }

}
