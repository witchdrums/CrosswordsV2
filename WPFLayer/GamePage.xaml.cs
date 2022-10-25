using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WPFLayer
{
    /* by witchdrums
     * TO DO
        [   ] Turn into PAGE
        [   ] Connect this with the lobby window
     */
    public partial class GamePage : Window
    {
        private UniformGrid crosswordGrid;
        private Label[,] labelMatrix;
        private char[,] wordMatrix;

        public GamePage()
        {
            this.labelMatrix = new Label[25, 25];
            this.wordMatrix = new char[25, 25];
            Brush colorSorroundingLetters = System.Windows.Media.Brushes.Gray;
            InitializeComponent();
            InitializeBoard(colorSorroundingLetters);
            InitializeWordList(colorSorroundingLetters);
        }

        private void InitializeWordList(Brush colorSorroundingLetters)
        {

            List<string> wordList = new List<string>()
            { "PERRO", "ROSA", "GATO", "ALEJANDRO", "ARMANDO", "JAVIER" };
            for (int index = 0; index < wordList.Count; index++)
            {
                string word = wordList.ElementAt(index);
                int amountOfLetters = word.Count();
                for (int letterIndex = 0; letterIndex < amountOfLetters; letterIndex++)
                {
                    Label currentLabel = this.labelMatrix[letterIndex, index];
                    currentLabel.Content = word.ElementAt(letterIndex);
                    currentLabel.Background = System.Windows.Media.Brushes.White;
                    currentLabel.FontSize = 15;
                }

                Console.WriteLine(amountOfLetters);
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
                    Thickness borderThickness = new Thickness(1,1,1,1);
                    Label label_cell = new Label()
                    {
                        //Content = 1,
                        BorderBrush=colorSorroundingLetters,
                        BorderThickness=borderThickness,
                        Background=colorSorroundingLetters,
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    };
                    labelMatrix[columnIndex, rowIndex] = label_cell;
                    this.crosswordGrid.Children.Add(label_cell);
                }
            }
            this.Grid_CrosswordPanel.Children.Add(crosswordGrid);
        }

        private void TextBox_WordGuess_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
