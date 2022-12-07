using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace TicTacToe
{
    internal class MainGame
    {
        public bool gameActive { get; set; }
        private int activePlayer;
        public string[] playerName = new string[2];
        private char[] playersTags;
        private char[] tags;
        private char[,] tictac;

        public MainGame(){
            this.gameActive = true;
            this.playerName[0] = this.playerName[1] = String.Empty;
            this.playersTags = new char[2];
            this.tags = new char[] { '1', '2', '3', 
                                    '4', '5', '6', 
                                    '7', '8', '9' };

            this.tictac = new char[13, 15]{
                {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
                {'|',' ',' ',' ','|',' ',' ',' ',' ','|',' ',' ',' ',' ','|'},
                {'|',' ',tags[0],' ','|',' ',' ',tags[1],' ','|',' ',' ',tags[2],' ','|'},
                {'|',' ',' ',' ','|',' ',' ',' ',' ','|',' ',' ',' ',' ','|'},
                {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
                {'|',' ',' ',' ','|',' ',' ',' ',' ','|',' ',' ',' ',' ','|'},
                {'|',' ',tags[3],' ','|',' ',' ',tags[4],' ','|',' ',' ',tags[5],' ','|'},
                {'|',' ',' ',' ','|',' ',' ',' ',' ','|',' ',' ',' ',' ','|'},
                {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
                {'|',' ',' ',' ','|',' ',' ',' ',' ','|',' ',' ',' ',' ','|'},
                {'|',' ',tags[6],' ','|',' ',' ',tags[7],' ','|',' ',' ',tags[8],' ','|'},
                {'|',' ',' ',' ','|',' ',' ',' ',' ','|',' ',' ',' ',' ','|'},
                {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'}
            }; ;
        }

        public void displayMainMenu(){

            try {
                animateText("Enter player 1's name:",false);
                this.playerName[0] = Console.ReadLine();
            }catch(Exception InvalidChoiceException) {
                this.playerName[0] = "Player1";
            }

            try {
                animateText("Enter player 2's name:",false);
                this.playerName[1] = Console.ReadLine();
            } catch (Exception InvalidChoiceException) {
                this.playerName[1] = "Player2";
            }

            while (this.playerName[0] == this.playerName[1]){
                animateText("Enter a distinct name for player 2:", false,10);
                this.playerName[1] = Console.ReadLine();
            }
            animateText("choosing randomly the first player to begin...",true);
            Thread.Sleep(2000);

            int firstPlayerIndex = new Random().Next(1, 100);
            

            if(firstPlayerIndex %2 == 1){
                this.activePlayer = 1;
            }else{
                this.activePlayer = 0;
            }

            Console.WriteLine("The first player to start is: " + ((this.activePlayer == 1) ? this.playerName[1] : this.playerName[0]) + "\n\n");

            char choice;
            do {
                Console.Write(this.playerName[this.activePlayer] + " choose your hero (X/O) : ");
                try {
                    choice = char.Parse(Console.ReadLine());
                } catch (Exception InvalidChoiceException) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invlaid choice, default hero X is set for " + this.playerName[this.activePlayer]);
                    Console.ResetColor();
                    choice = 'X';
                }
            } while (char.ToUpper(choice) != 'X' && char.ToUpper(choice) != 'O');

            this.playersTags[this.activePlayer] = char.ToUpper(choice);

            if (playersTags[this.activePlayer] == 'X'){
                playersTags[(this.activePlayer + 1)% 2] = 'O';

            }else{
                playersTags[(this.activePlayer+ 1) % 2] = 'X';
            }
            
            
            
        }

        public void playerMove() {

            displayGrid();
            int cell = 0;
            do {
                Console.WriteLine("it's " + ((this.activePlayer == 1) ? this.playerName[1] : this.playerName[0]) + "'s turn choose an empty cell: ");
                cell = int.Parse(Console.ReadLine());

            } while (this.tags[cell - 1] == 'X' && this.tags[cell - 1] == 'O');

            this.tags[cell - 1] = this.playersTags[this.activePlayer];
            this.activePlayer = (this.activePlayer + 1) % 2;

            this.tictac = new char[13, 15]{
                {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
                {'|',' ',' ',' ','|',' ',' ',' ',' ','|',' ',' ',' ',' ','|'},
                {'|',' ',tags[0],' ','|',' ',' ',tags[1],' ','|',' ',' ',tags[2],' ','|'},
                {'|',' ',' ',' ','|',' ',' ',' ',' ','|',' ',' ',' ',' ','|'},
                {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
                {'|',' ',' ',' ','|',' ',' ',' ',' ','|',' ',' ',' ',' ','|'},
                {'|',' ',tags[3],' ','|',' ',' ',tags[4],' ','|',' ',' ',tags[5],' ','|'},
                {'|',' ',' ',' ','|',' ',' ',' ',' ','|',' ',' ',' ',' ','|'},
                {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
                {'|',' ',' ',' ','|',' ',' ',' ',' ','|',' ',' ',' ',' ','|'},
                {'|',' ',tags[6],' ','|',' ',' ',tags[7],' ','|',' ',' ',tags[8],' ','|'},
                {'|',' ',' ',' ','|',' ',' ',' ',' ','|',' ',' ',' ',' ','|'},
                {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'}
            };
            checkWinner();
        }
        public void displayGrid(){
            for (int i = 0; i < 13; i++){
                for (int j = 0; j < 15; j++){
                    Console.Write(this.tictac[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void checkWinner()
        {
            //Check Horizontal cells
            for (int i = 0; i < 9; i += 3){
                if (this.tags[i] == this.tags[i + 1] && this.tags[i + 1] == this.tags[i + 2]){
                    this.gameActive = false;
                    break;
                }
            }

            //Check vertical cells
            for (int i = 2; i >= 0; i--){
                if (this.tags[5-i] == this.tags[2-i] && this.tags[2- i] == this.tags[8 - i]){
                    this.gameActive = false;
                    break;
                }
            }

            //Check Diaognal cells
            if ((tags[0] == tags[4] && tags[4] == tags[8]) || tags[2] == tags[4] && tags[4] == tags[6]){
                this.gameActive = false;
            }

            bool tie = true;
            for (int i = 0; i < 9; ++i) {
                if (tags[i] != 'O' && tags[i]!= 'X') {
                    tie = false;
                }
            }


            if (tie) {
                resultMessage("It's a tie! Try again", ConsoleColor.Blue);
                this.gameActive = false;
                Thread.Sleep(3000);
            }else if(!this.gameActive){
                resultMessage(((this.activePlayer == 1) ? this.playerName[1] : this.playerName[0]) + " is The winner, Congrats!\n",ConsoleColor.Green);
                Thread.Sleep(3000);
            }
        }

        public void resultMessage(string textMessage, ConsoleColor color){
            Console.Clear();
            displayGrid();
            Console.ForegroundColor = color;
            Console.WriteLine();
            animateText(textMessage,true, 50);
            Console.ResetColor();
        }

        

        private void animateText(string text, bool nl, int speed = 40) {
            foreach (char c in text) {
                Console.Write(c);
                Thread.Sleep(speed);
            }

            if (nl) {
                Console.WriteLine();
            }

        }

    }
}
