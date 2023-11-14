using System;
using System.Linq;
using System.Text;
using ComputerInterface;
using ComputerInterface.Interfaces;
using ComputerInterface.ViewLib;
using UnityEngine;

namespace Gorilla_Wordle.CI
{
    internal class WordleView : ComputerView
    {
        internal string targetWord = "MONKE";

        internal string blabla = "if you see this, fuck you. you leaked the wordle answer";

        internal StringBuilder guessedWord = new StringBuilder();
        internal StringBuilder guessedWords = new StringBuilder();
        internal int remainingGuesses = 5; 

        public override void OnShow(object[] args)
        {
            base.OnShow(args);
            DrawPage();
        }

        private void DrawPage()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder
                .BeginCenter()
                .MakeBar('=', SCREEN_WIDTH, 0)
                .Append("\nGorilla Wordle\nBy Chin\n")
                .MakeBar('=', SCREEN_WIDTH, 0)
                .AppendLines(1);

            stringBuilder.Append($"{SetColoursForWords(guessedWord, true)}\n");

            if (guessedWords.Length > 0)
            {
                string[] guesses = guessedWords.ToString().Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

                foreach (var guess in guesses)
                {
                    stringBuilder.Append($"{SetColoursForWords(new StringBuilder(guess), false)}\n");
                }

                stringBuilder.MakeBar('=', SCREEN_WIDTH, 0).AppendLines(1);
            }

            stringBuilder
                .Append($"Guesses Left: {remainingGuesses}\n")
                .MakeBar('=', SCREEN_WIDTH, 0);

            stringBuilder.BeginAlign("left");

            SetText(stringBuilder);
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            if (key == EKeyboardKey.Back)
            {
                ReturnToMainMenu();
            }
            if (remainingGuesses <= 0 || !canGuess)
            {
                
                return;
            }

            if (key == EKeyboardKey.Delete)
            {
                if (guessedWord.Length > 0)
                {
                    guessedWord.Remove(guessedWord.Length - 1, 1);
                    DrawPage();
                }
            }
            else if (key == EKeyboardKey.Enter && guessedWord.Length > 0)
            {
                HandleComputerInput(guessedWord.ToString());
                guessedWords.Append($"{guessedWord.ToString()} ");
                guessedWord.Clear();
                DrawPage();
            }
            else if (!key.IsFunctionKey() && guessedWord.Length < targetWord.Length)
            {
                guessedWord.Append(key.ToString());
                DrawPage();
            }
        }
        

        private string SetColoursForWords(StringBuilder word, bool isCurrentInput)
        {
            StringBuilder enteredWord = new StringBuilder();
            for (int i = 0; i < word.Length; i++)
            {
                char letter = word[i];
                if (!isCurrentInput)
                {
                    if (letter == targetWord[i])
                    {
                        enteredWord.Append($"<color=#019A01>{letter}</color>"); // correct
                    }
                    else if (targetWord.Contains(letter))
                    {
                        enteredWord.Append($"<color=#ffC425>{letter}</color>"); // correct but not right place
                    }
                    else
                    {
                        enteredWord.Append($"<color=#B0B0B0>{letter}</color>"); // not in word
                    }
                }
                else
                {
                    enteredWord.Append(letter);
                }

                enteredWord.Append(" ");
            }

            return enteredWord.ToString();
        }

        internal bool canGuess = true;

        public void HandleComputerInput(string guessedWordInput)
        {
            if (remainingGuesses <= 1 && guessedWord.ToString() != targetWord)
            {
                Debug.Log("Game Over");
                remainingGuesses = 0;
                return;
            }

            if (guessedWordInput.Length != targetWord.Length)
            {
                
                return;
            }

            remainingGuesses--;

            bool isCorrectGuess = true;

            for (int i = 0; i < targetWord.Length; i++)
            {
                if (targetWord[i] != guessedWordInput[i])
                {
                    isCorrectGuess = false;
                    break;
                }
            }

            if (isCorrectGuess)
            {
                guessedWord.Clear();
                guessedWord.Append(guessedWordInput);

                if (guessedWord.ToString() == targetWord)
                {
                    Debug.Log("Congratulations! You guessed the word!");
                    canGuess = false;
                }
            }
        }
    }

    internal class WordleEntry : IComputerModEntry
    {
        public string EntryName => "Gorilla Wordle";
        public Type EntryViewType => typeof(WordleView);
    }
}
