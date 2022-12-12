using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Validation;
using WPFLayer.Properties;
using WPFLayer.ServicesImplementation;

namespace WPFLayer
{

    public partial class GamePage : Page, IMessagesCallback, IGameManagementCallback, IPingCallback
    {
        private DispatcherTimer gameTimer = new DispatcherTimer();
        private TimeSpan timeSpan = new TimeSpan();
        private Label[,] labelMatrix = new Label[20, 20];
        private ServicesImplementation.Boards board;
        private List<Label> selectedWordLabels = new List<Label>();
        private List<Label> selectedWordLabelsCopy = new List<Label>();
        private List<Label> enemyScores = new List<Label>();
        private Dictionary<int, EnemyPortrait> enemyPortraits = new Dictionary<int, EnemyPortrait>();
        private List<GamesPlayers> enemies;
        private bool hasTurn;
        private int remainingTurns = 0;
        private WordsBoard currentWordSelected;

        private Queue<GamesPlayers> gamePlayersQueue = new Queue<GamesPlayers>();
        private List<ServicesImplementation.Users> usersRoom;
        private Users userLogin;
        public int IdRoom { get; set; }

        public GamesPlayers Player { get; set; }
        private GameConfiguration gameConfiguration;

        public GamePage(Users userLogin, int idRoom, GameConfiguration gameConfiguration)
        {
            InitializeComponent();
            this.userLogin = userLogin;
            this.IdRoom = idRoom;
            this.gameConfiguration = gameConfiguration;
            this.remainingTurns = gameConfiguration.TurnAmount;
            this.usersRoom = gameConfiguration.UsersRoom.ToList();
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.PingClient pingClient = new ServicesImplementation.PingClient(context);
            pingClient.ConnectPingManagement(userLogin);
            GetGamePlayer();
            JoinGame();

            Brush colorSorroundingLetters = System.Windows.Media.Brushes.Black;
            GameSetup();
            InitializeEnemyPortraits();
            InitializeBoard(colorSorroundingLetters);
            GetBoard();
            PlaceAllWordsInBoard();
            WaitForHostToStart();
        }

        private void WaitForHostToStart()
        {
            if (this.userLogin.idUser == this.IdRoom)
            {
                this.Button_Player.Content = Properties.Resources.Game_Button_StartGame;
                this.Button_Player.Background = System.Windows.Media.Brushes.Violet;
                this.Button_Player.Click += new RoutedEventHandler(StartGame);
            }
        }

        private void GetGamePlayer()
        {
            this.gamePlayersQueue = gameConfiguration.GamePlayerQueue;
            foreach (GamesPlayers gamePlayer in gamePlayersQueue)
            {
                if (gamePlayer.Player.User.idUser == this.userLogin.idUser)
                {
                    this.Player = gamePlayer;
                }
            }
            this.ImageBrush_Avatar1.ImageSource = Assets.ImageHelper.GetBitmapImageFor(this.Player.Player);
        }

        private void StartGame(object sender, EventArgs eventArguments)
        {
            this.Button_Player.Content = this.Player.Player.playerName;
            this.Button_Player.Background = System.Windows.Media.Brushes.White;
            this.Button_Player.Click -= (StartGame);
            this.Button_Player.Click += (SortPlayersByScore);
            EndTurn();
        }

        private void GameSetup()
        {
            timeSpan = TimeSpan.FromSeconds(1);
            gameTimer.Interval = TimeSpan.FromSeconds(1);

            gameTimer.Tick += SecondPasses;
            this.ListView_HorizontalClueList.ItemsSource = new List<string>();

            IsMyTurn(false);
        }

        private void RestartGameTimer()
        {
            timeSpan = TimeSpan.FromSeconds(this.gameConfiguration.TurnSeconds);
            gameTimer.Start();
        }

        private void SecondPasses(object sender, EventArgs eventArguments)
        {
            if (timeSpan == TimeSpan.FromSeconds(1))
            {
                EndTurn();
            }
            else if (hasTurn)
            {
                timeSpan = timeSpan.Add(TimeSpan.FromSeconds(-1));
                this.Label_Timer.Content = timeSpan.TotalSeconds.ToString();
            }
        }

        private void JoinGame()
        {
            this.Button_PlayerAvatar.Tag = this.Player;
            this.Button_Player.Content = this.Player.Player.playerName;
            this.Label_PlayerScore.Content = this.Player.gameScore;
            InstanceContext instanceContext = new InstanceContext(this);
            GameManagementClient gameManagementClient = new GameManagementClient(instanceContext);
            MessagesClient messagesClient = new MessagesClient(instanceContext);
            gameManagementClient.JoinGame(this.Player);
            messagesClient.ConnectMessages(this.userLogin);
        }

        private void InitializeEnemyPortraits()
        {
            this.enemies =
                this.gamePlayersQueue.Where(enemy => enemy.Player.idPlayer != this.Player.Player.idPlayer).ToList();

            List<Button> enemyNames = new List<Button>();
            enemyNames.Add(this.Button_Enemy1);
            enemyNames.Add(this.Button_Enemy2);
            enemyNames.Add(this.Button_Enemy3);

            this.enemyScores.Add(this.Label_Enemy1Score);
            this.enemyScores.Add(this.Label_Enemy2Score);
            this.enemyScores.Add(this.Label_Enemy3Score);

            List<Ellipse> enemyAvatars = new List<Ellipse>();
            enemyAvatars.Add(this.Ellipse_AvatarEnemy1);
            enemyAvatars.Add(this.Ellipse_AvatarEnemy2);
            enemyAvatars.Add(this.Ellipse_AvatarEnemy3);

            enemyScores.ForEach(label => label.Tag = 0);

            for (int enemyIndex = 0; enemyIndex < enemies.Count; enemyIndex++)
            {
                GamesPlayers currentEnemy = enemies[enemyIndex];
                enemyNames[enemyIndex].Content = currentEnemy.Player.playerName;
                enemyNames[enemyIndex].Visibility = Visibility.Visible;
                enemyNames[enemyIndex].Tag = currentEnemy;

                enemyScores[enemyIndex].Tag = currentEnemy.Player.idPlayer;
                enemyScores[enemyIndex].Visibility = Visibility.Visible;

                enemyAvatars[enemyIndex].Visibility = Visibility.Visible;
                (enemyAvatars[enemyIndex].Fill as ImageBrush).ImageSource =
                    Assets.ImageHelper.GetBitmapImageFor(currentEnemy.Player);
                EnemyPortrait enemyPortrait = new EnemyPortrait();
                enemyPortrait.enemyAvatar = enemyAvatars[enemyIndex];
                enemyPortrait.enemyScore = enemyScores[enemyIndex];
                enemyPortrait.enemyName = enemyNames[enemyIndex];
                this.enemyPortraits.Add(currentEnemy.Player.idPlayer, enemyPortrait);
            }
        }

        private void AddScoreToSolver(GamesPlayers solver)
        {

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
            this.ListView_HorizontalClueList.ItemsSource = this.board.WordsBoards;
            this.currentWordSelected = this.board.WordsBoards.ElementAt(0);
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
                        VerticalContentAlignment = VerticalAlignment.Center,
                        FontWeight = FontWeights.Bold,
                        Content = "",
                        FontSize = 12,
                        Padding = padding,

                    };

                    labelMatrix[columnIndex, rowIndex] = cell;
                    crosswordGrid.Children.Add(cell);
                }
            }

            this.Grid_CrosswordPanel.Children.Add(crosswordGrid);
        }

        private void TextBox_WordGuess_TextChanged(object sender, TextChangedEventArgs eventArguments)
        {
            this.selectedWordLabels.ForEach(label => label.Content = "");
            for (int xIndex = 0; xIndex < this.TextBox_WordGuess.Text.Length; xIndex++)
            {

                if (xIndex < this.selectedWordLabels.Count 
                    && this.selectedWordLabels.ElementAt(xIndex).Content.ToString() == "")
                {
                    this.selectedWordLabels.ElementAt(xIndex).Content = this.TextBox_WordGuess.Text[xIndex];
                }
            }
        }

        private void ListView_HorizontalClueList_SelectionChanged(object sender, SelectionChangedEventArgs eventArguments)
        {
            if (!currentWordSelected.Word.isSolved)
            {
                DeleteGuess();
            }
            SelectWordLabels();
        }

        private void SelectWordLabels()
        {
            DeselectAllBoardLabels();

            this.currentWordSelected = GetSelectedWordInClueList();
            ClearSelectedWordLabels();
            InitializeTextBoxToSolveWord(currentWordSelected);

            int xIndex = currentWordSelected.xStart;
            int yIndex = currentWordSelected.yStart;

            foreach (char letter in currentWordSelected.Word.term)
            {
                Label selectedWordLabel = this.labelMatrix[xIndex, yIndex];
                selectedWordLabel.Background = System.Windows.Media.Brushes.Red;

                this.selectedWordLabels.Add(selectedWordLabel);
                bool wordIsHorizontal = currentWordSelected.yStart == currentWordSelected.yEnd;
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
            if (!selectedWord.Word.isSolved)
            {
                this.TextBox_WordGuess.Text = "";
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

        private void GuessWord()
        {
            String playerGuess = this.TextBox_WordGuess.Text.ToUpperInvariant();
            WordsBoard selectedWord = GetSelectedWordInClueList();
            if (playerGuess == selectedWord.Word.term)
            {
                AddScore(selectedWord);
                selectedWord.Word.isSolved = true;
                GetGameManagementClient().SendSolvedWordsBoard(this.gamePlayersQueue, this.Player, selectedWord);
                ClearSelectedWordLabels();
            }
            else
            {
                DeleteGuess();
            }
            EndTurn();
            this.TextBox_WordGuess.Text = "";
        }

        private void AddScore(WordsBoard solvedWord)
        {
            int score = solvedWord.Word.term.Length * 100;
            this.Player.gameScore += score;
            this.Label_PlayerScore.Content = this.Player.gameScore;
            foreach (GamesPlayers player in gamePlayersQueue)
            {
                if (player.idPlayer == this.Player.idPlayer)
                {
                    player.gameScore += score;
                }
            }
        }

        private void IsMyTurn(bool isMyTurn)
        {
            this.hasTurn = isMyTurn;

            this.TextBox_WordGuess.IsEnabled = hasTurn;
            this.Button_Guess.IsEnabled = hasTurn;
        }

        private void ClearSelectedWordLabels()
        {

            this.selectedWordLabels.Clear();
            this.selectedWordLabelsCopy.Clear();
            
        }


        private WordsBoard GetSelectedWordInClueList()
        {
            WordsBoard selectedWord = this.ListView_HorizontalClueList.SelectedItem as WordsBoard;
            return selectedWord;
        }

        public void ReciveChatMessage(Users userOrigin, string message)
        {

            this.TextBlock_Chat.Items.Add(userOrigin.username + ": " + message);
            this.ScrollViewer_Chat.ScrollToBottom();

        }

        private void Button_SendMessage_Click(object sender, RoutedEventArgs eventArguments)
        {
            if (!String.IsNullOrWhiteSpace(TextBox_Message.Text))
            {
                InstanceContext context = new InstanceContext(this);
                ServicesImplementation.IMessages messages = new ServicesImplementation.MessagesClient(context);
                messages.SendChatMessage(this.usersRoom.ToArray(), this.userLogin, TextBox_Message.Text);
                this.TextBox_Message.Clear();
            }
        }

        public void ReceiveSolvedWordsBoard(GamesPlayers solver, WordsBoard solvedWordsBoard)
        {
            WordsBoard solvedItem = new WordsBoard();
            foreach (WordsBoard item in ListView_HorizontalClueList.Items)
            {
                if (item.Word.term == solvedWordsBoard.Word.term)
                {
                    solvedItem = item;
                    item.Word.isSolved = true;
                    break;
                }
            }


            PlaceWordInBoard(solvedItem);
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

        private void KeyDownKeyboard(object sender, KeyEventArgs keyEvent)
        {

            if (hasTurn && !this.TextBox_Message.IsFocused)
            {
                HandleUserKeyboardInput(keyEvent);
            }
        }

        private void HandleUserKeyboardInput(KeyEventArgs keyEvent)
        {

            int keyValue = (int)keyEvent.Key;
            if (keyValue >= 44 && keyValue <= 69)
            {
                int currentLettersInGuess = this.TextBox_WordGuess.Text.Length;
                WordsBoard selectedWord = GetSelectedWordInClueList();

                if (this.TextBox_WordGuess.MaxLength > currentLettersInGuess && !selectedWord.Word.isSolved)
                {
                    this.TextBox_WordGuess.Text += keyEvent.Key.ToString();
                }
            }
            else if (keyEvent.Key == Key.Back)
            {
                DeleteGuess();
            }
            else if (keyEvent.Key == Key.Enter)
            {
                GuessWord();
            }
            keyEvent.Handled = true;
        }

        private void Button_Guess_Click(object sender, RoutedEventArgs eventArguments)
        {
            GuessWord();
        }

        private void DeleteGuess()
        {
            this.TextBox_WordGuess.Clear();
            for (int selectedWordLabelIndex = 0; 
                 selectedWordLabelIndex < selectedWordLabelsCopy.Count; 
                 selectedWordLabelIndex++)
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
                SortPlayersByScore(new object(), new RoutedEventArgs());
            }

        }

        private void SortPlayersByScore(object sender, RoutedEventArgs eventArguments)
        {
            List<GamesPlayers> finalPlayerPositions = gamePlayersQueue.ToList();
            finalPlayerPositions.Sort((a, b) => b.gameScore.CompareTo(a.gameScore));
            int gameRank = 1;
            int idGame = RegisterGame();
            foreach (GamesPlayers player in finalPlayerPositions)
            {
                player.gameRank = gameRank;
                gameRank = gameRank + 1;
                player.idGame = idGame;
            }
            GetGameManagementClient().EndGame(this.IdRoom, finalPlayerPositions.ToArray());
        }

        private int RegisterGame()
        {
            Games game = new Games();
            game.gameDate = DateTime.Now;
            int timeInSeconds = 
                (this.gameConfiguration.TurnAmount - this.remainingTurns) * this.gameConfiguration.TurnSeconds;        
            game.gameTime = TimeSpan.FromSeconds(timeInSeconds);

            return GetGameManagementClient().RegisterGame(game);
        }

        private bool ThereAreWordsToBeSolvedStill()
        {
            int wordsToBeSolved = 0;
            foreach (WordsBoard word in this.ListView_HorizontalClueList.Items)
            {
                if (!word.Word.isSolved)
                {
                    wordsToBeSolved += 1;
                }

            }
            return wordsToBeSolved > 0;
        }


        public void ReceiveTurn()
        {
            IsMyTurn(true);
            RestartGameTimer();
        }


        public void UpdateGamePlayersQueue(Queue<GamesPlayers> gamePlayers, int remainingTurns)
        {
            this.gamePlayersQueue = gamePlayers;
            this.remainingTurns = remainingTurns;
            this.Label_Turns.Content = remainingTurns;
        }

        public void ShowPlayerRanks(GamesPlayers[] playerRanks)
        {

            PodiumPage podiumPage = new PodiumPage(playerRanks.ToList());
            NavigationService.Navigate(podiumPage);
        }

        private void Button_Avatar_Click(object sender, RoutedEventArgs eventArguments)
        {
            GamesPlayers chosenPlayer = ((Button)sender).Tag as GamesPlayers;
            new ProfileWindow(this, chosenPlayer.Player).Show();
        }

        public void KickPlayer(Players chosenPlayer)
        {
            GameManagementClient gameManagementClient = GetGameManagementClient();
            gameManagementClient.PassTurn(this.gamePlayersQueue, this.remainingTurns);
            GetGameManagementClient().RemovePlayer(chosenPlayer, this.gamePlayersQueue);
        }

        private void Button_LeaveGame_Click(object sender, RoutedEventArgs eventArguments)
        {
            MessageBoxResult messageBoxResult = 
                System.Windows.MessageBox.Show
                (
                    Properties.Resources.Game_Message_Leave,
                    "", 
                    System.Windows.MessageBoxButton.YesNo
                );
            if (messageBoxResult == MessageBoxResult.Yes)
            {

                if (IdRoom == this.Player.Player.User.idUser)
                {
                    GetGameManagementClient().RemoveHost(this.Player, this.gamePlayersQueue);
                    SendLeavingUserToMainMenu();
                }
                else
                {
                    GetGameManagementClient().RemovePlayer(this.Player.Player, this.gamePlayersQueue);
                }
            }
        }

        public void RemoveLeavingUser(Players leavingPlayer, Queue<GamesPlayers> updatedQueue)
        {
            EnemyPortrait leavingEnemyPortrait = this.enemyPortraits[leavingPlayer.idPlayer];

            leavingEnemyPortrait.enemyScore.Visibility = Visibility.Hidden;
            leavingEnemyPortrait.enemyAvatar.Visibility = Visibility.Hidden;
            leavingEnemyPortrait.enemyName.Visibility = Visibility.Hidden;
            this.enemyPortraits.Remove(leavingPlayer.idPlayer);

            this.enemyScores.Remove(leavingEnemyPortrait.enemyScore);
            this.gamePlayersQueue = updatedQueue;

        }

        public void StopGame()
        {
            MessageBox.Show(Properties.Resources.Game_Message_ForceExit);
            SendLeavingUserToMainMenu();
        }

        public void SendLeavingUserToMainMenu()
        {
            NavigationService.RemoveBackEntry();
            NavigationService.GoBack();
        }

        internal class EnemyPortrait
        {
            public Button enemyName;
            public Label enemyScore;
            public Ellipse enemyAvatar;
        }

        public void Alive()
        {
        }

        public void BackMenu()
        {
            this.SendLeavingUserToMainMenu();
        }
    }

}
