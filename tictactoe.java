import java.util.Scanner;
public class tictactoe
{
    // check for win
    public boolean checkWin(String[][] board, String symbol) 
    {
        // Check rows and columns
        for (int i = 0; i < 3; i++) 
        {
            if (board[i][0].equals(symbol) && board[i][1].equals(symbol) && board[i][2].equals(symbol)) return true;
            if (board[0][i].equals(symbol) && board[1][i].equals(symbol) && board[2][i].equals(symbol)) return true;
        }
        // Check diagonals
        if (board[0][0].equals(symbol) && board[1][1].equals(symbol) && board[2][2].equals(symbol)) return true;
        if (board[0][2].equals(symbol) && board[1][1].equals(symbol) && board[2][0].equals(symbol)) return true;
        return false;
    }
    // print board
    public void printBoard(String[][] board)
    {
        for (int i = 0; i < board.length; i++) 
        {
            for (int j = 0; j < board[i].length; j++) 
            {
                System.out.print(" " + board[i][j]);
                if (j < board[i].length - 1) System.out.print(" |");
            }
            System.out.println();
            if (i < board.length - 1) System.out.println("---+---+---");
        }
    }
    public void playTicTacToe()
{
    System.out.println("Welcome to Tic Tac Toe!");
    System.out.println("You will be playing against the computer as 'X' and the computer will be 'O'.");
    System.out.println("The board is numbered as follows:");
    String board[][] = {{" ", " ", " "}, {" ", " ", " "}, {" ", " ", " "}};
    printBoard(board);
    int moves = 9;
    int spaces[] = {1, 2, 3, 4, 5, 6, 7, 8, 9};
    Scanner playerInput = new Scanner(System.in);

    while(moves > 0)
    {
        // Player move
        System.out.println("Enter a number from 1-9 to place your 'X': ");
        int playerMove = playerInput.nextInt();
        if(playerMove < 1 || playerMove > 9)
        {
            System.out.println("Please enter a number from 1-9.");
            continue;
        }
        boolean placed = false;
        for(int h = 0; h < spaces.length; h++)
        {
            if(spaces[h] == playerMove)
            {
                int row = (playerMove - 1) / 3;
                int col = (playerMove - 1) % 3;
                if(board[row][col].equals(" "))
                {
                    board[row][col] = "X";
                    spaces[h] = 0;
                    moves -= 1;
                    placed = true;
                    break;
                }
            }
        }
        if(!placed)
        {
            System.out.println("That space is already taken. Please choose another.");
            continue;
        }
        printBoard(board);
        // Check for win
        if(checkWin(board, "X"))
        {
            System.out.println("You win! You will now continue to the main game.");
            break;
        }
        if(moves == 0)
        {
            System.out.println("It's a tie! You will now continue to the main game.");
            break;
        }
        // Computer move
        int computerMove;
        while(true)
        {
            computerMove = (int)(Math.random()*9 + 1);
            boolean compPlaced = false;
            for(int h = 0; h < spaces.length; h++)
            {
                if(spaces[h] == computerMove)
                {
                    int row = (computerMove - 1) / 3;
                    int col = (computerMove - 1) % 3;
                    if(board[row][col].equals(" "))
                    {
                        board[row][col] = "O";
                        spaces[h] = 0;
                        moves -= 1;
                        compPlaced = true;
                        break;
                    }
                }
            }
            if(compPlaced) break;
        }
        printBoard(board);
        // Check for win
        if(checkWin(board, "O"))
        {
            System.out.println("You lose! You will now continue to the main game.");
            break;
        }
        if(moves == 0)
        {
            System.out.println("It's a tie! You will now continue to the main game.");
            break;
        }
    }
    playerInput.close();
}
    public static void main(String[] args) {
        tictactoe game = new tictactoe();
        game.playTicTacToe();
    }
}