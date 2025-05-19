import java.util.ArrayList;
import java.util.Scanner;

public class minigame
{
    private int numLetters;
    private String word;
    private String[] words;
    
    public minigame()
    {
        words = new String[] {"house", "sausage", "food", "computer"};
        word = words[(int)(Math.random()*4)];
        numLetters = word.length();
    }
    public void hangman()
    {
        ArrayList <String> guesses = new ArrayList<> ();
        int wrongGuesses = 0;

        String[] playerWord = new String[word.length()];
        for(int i = 0; i < playerWord.length; i++)
        {
            playerWord[i] = "_";
        }

        Scanner playerGuess = new Scanner(System.in);
        System.out.println("Welcome to Hangman, you will be allowed five incorrect guesses before you lose. Good luck!");

        while(wrongGuesses < 5)
        {
            System.out.println("Guess a letter: ");
            String guess = playerGuess.nextLine();
            if(guess.length() != 1)
            {
                System.out.println("Please enter a single letter.");
                continue;
            }
            guess = guess.toLowerCase();
            if(guesses.contains(guess))
            {
                System.out.println("You have already guessed that letter.");
                continue;
            }
            if(word.indexOf(guess) == -1)
            {
                wrongGuesses += 1;
                System.out.println("Wrong guess! You have " + (5 - wrongGuesses) + " incorrect guesses left.");
                guesses.add(guess);
                System.out.println("Guesses: " + guesses);
            }
            else
            {
                System.out.println("Correct guess!");
                guesses.add(guess);
                for(int i = 0; i < word.length(); i++)
                {
                    if(word.charAt(i) == guess.charAt(0))
                    {
                        playerWord[i] = guess;
                    }
                }
            }
            System.out.print("Your word: ");
            for(int i = 0; i < word.length(); i++)
            {
                    System.out.print(playerWord[i]);
            }
            System.out.println();
            if (!String.join("", playerWord).contains("_"))
            {
                System.out.println("You win! The word was " + word + ". You will now continue to the main game.");
                break;
            }
        }
        playerGuess.close();
        if(wrongGuesses == 5)
        {
            System.out.println("You lose! The word was " + word + ". You will now continue to the main game.");
        }
    }

    public static void main(String[] args) {
    minigame game = new minigame();
    game.hangman();
}
}