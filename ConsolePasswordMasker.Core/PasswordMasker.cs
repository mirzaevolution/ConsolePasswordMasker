using System;
using System.Linq;
using System.Text;
namespace ConsolePasswordMasker.Core
{
    /// <summary>
    /// This class represents data from console input
    /// and its status whether or not input is cancelled.
    /// </summary>
    public class ConsoleKeyData
    {
        public ConsoleKeyData(string data, bool isCancelled)
        {
            Data = data;
            IsCancelled = isCancelled;
        }
        /// <summary>
        /// Gets data from input.
        /// </summary>
        public string Data
        {
            get;
        }
        /// <summary>
        /// Gets boolean status whether or not input has been cancelled by user.
        /// </summary>
        public bool IsCancelled
        {
            get;
        }
    }

    /// <summary>
    /// Class that performs input checking and masking for every character from console input.
    /// </summary>
    public class PasswordMasker
    {
        private char[] definedChars =
        {   '~','!','@','#','$','%','^','&','*','(',')','_','+','-','=',
            '`','1','2','3','4','5','6','7','8','9','0','Q','q','W','w','E','e',
            'R','r','T','t','Y','y','U','u','I','i','O','o',
            'P','p','[','{',']','}','A','a','S','s','D','d','F','f','G','g','H','h',
            'J','j','K','k','L','l',';',':','\'','\"','\\',
            'Z','z','X','x','C','c','V','v','B','b','N','n','M','m','N',',','<','.','>','/','?',' '
        };

        /// <summary>
        /// Gets default characters used by this class.
        /// </summary>
        public char[] DefinedChars
        {
            get
            {
                return definedChars;
            }
        }
        /// <summary>
        /// Check every character and mask them with '*' asterik and returns the result when
        /// user hits 'enter' key. By default this method does not cancel input when it receives 'escape' character. 
        /// This method only filters chars from DefinedChars property.
        /// </summary>
        /// <returns>Input result from console.</returns>
        public string Mask()
        {
            StringBuilder sb = new StringBuilder();
            ConsoleKeyInfo consoleKey;
            bool exit = false;
            do
            {
                consoleKey = Console.ReadKey(true);
                if ((consoleKey.Key == ConsoleKey.Enter))
                    exit = true;
                else if (consoleKey.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                        sb.Remove(sb.Length - 1, 1);
                }
                else if (definedChars.Contains(consoleKey.KeyChar))
                {
                    sb.Append(consoleKey.KeyChar);
                }
                Console.Clear();
                if (sb.Length > 0)
                {
                    Console.Write("".PadRight(sb.Length, '*'));
                }
            } while (!exit);
            Console.WriteLine();
            return sb.ToString();
        }
        /// <summary>
        /// Check every character and mask them with char from charMask parameter and returns the result when
        /// user hits 'enter' key. By default this method does not cancel input when it receives 'escape' character. 
        /// This method only filters chars from DefinedChars property.
        /// </summary>
        /// <param name="charMask">An output character to console.</param>
        /// <param name="useBeep">Determines whether or not it invokes beep method when user hits key.</param>
        /// <returns>Input result from console.</returns>
        public string Mask(char charMask = '*', bool useBeep = false)
        {
            StringBuilder sb = new StringBuilder();
            ConsoleKeyInfo consoleKey;
            bool exit = false;
            do
            {
                consoleKey = Console.ReadKey(true);
                if (useBeep)
                    Console.Beep();
                if ((consoleKey.Key == ConsoleKey.Enter))
                    exit = true;
                else if (consoleKey.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                        sb.Remove(sb.Length - 1, 1);
                }
                else if (definedChars.Contains(consoleKey.KeyChar))
                {
                    sb.Append(consoleKey.KeyChar);
                }
                Console.Clear();
                if (sb.Length > 0)
                {
                    Console.Write("".PadRight(sb.Length, charMask));
                }
            } while (!exit);
            Console.WriteLine();
            return sb.ToString();
        }
        /// <summary>
        /// Check every character and mask them with char from charMask parameter and returns ConsoleKeyData
        /// that contains input data and status whether input is cancelled or not.
        /// This method supports input cancellation by hitting 'escape' key and only filters chars from DefinedChars property.
        /// </summary>
        /// <param name="charMask">An output character to console.</param>
        /// <param name="useBeep">Determines whether or not it invokes beep method when user hits key.</param>
        /// <param name="cancelOnEscape">Cancels input when it sets to true.</param>
        /// <returns>Input data and status whether input is cancelled or not.</returns>
        public ConsoleKeyData Mask(char charMask = '*', bool useBeep = false, bool cancelOnEscape = false)
        {
            StringBuilder sb = new StringBuilder();
            ConsoleKeyInfo consoleKey;
            bool exit = false;
            bool isCancelled = false;
            do
            {
                consoleKey = Console.ReadKey(true);
                if (useBeep)
                    Console.Beep();
                if ((consoleKey.Key == ConsoleKey.Enter))
                    exit = true;
                else if (consoleKey.Key == ConsoleKey.Escape)
                {
                    if (cancelOnEscape)
                    {
                        exit = true;
                        isCancelled = true;
                        sb.Clear();
                    }
                }
                else if (consoleKey.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                        sb.Remove(sb.Length - 1, 1);
                }
                else if (definedChars.Contains(consoleKey.KeyChar))
                {
                    sb.Append(consoleKey.KeyChar);
                }
                Console.Clear();
                if (sb.Length > 0)
                {
                    Console.Write("".PadRight(sb.Length, charMask));
                }
            } while (!exit);
            Console.WriteLine();
            return new ConsoleKeyData(sb.ToString(), isCancelled);
        }
        /// <summary>
        /// Check every character and mask them with char from charMask parameter and returns ConsoleKeyData
        /// that contains input data and status whether input is cancelled or not.
        /// This method supports input cancellation by hitting 'escape' key and custom checker that receives ConsoleKeyInfo.
        /// It only filters input from customChecker parameter.
        /// </summary>
        /// <param name="customChecker">A custom checker to filter every character from console.</param>
        /// <param name="charMask">An output character to console.</param>
        /// <param name="useBeep">Determines whether or not it invokes beep method when user hits key.</param>
        /// <param name="cancelOnEscape">Cancels input when it sets to true.</param>
        /// <returns>Input data and status whether input is cancelled or not.</returns>
        /// <exception cref="ArgumentNullException">`customChecker` cannot be null</exception>
        public ConsoleKeyData Mask(Func<ConsoleKeyInfo, bool> customChecker, char charMask = '*', bool useBeep = false, bool cancelOnEscape = false)
        {
            if (customChecker == null)
                throw new ArgumentNullException(nameof(customChecker), "`customChecker` cannot be null");
            StringBuilder sb = new StringBuilder();
            ConsoleKeyInfo consoleKey;
            bool exit = false;
            bool isCancelled = false;
            do
            {
                consoleKey = Console.ReadKey(true);
                if (useBeep)
                    Console.Beep();
                if ((consoleKey.Key == ConsoleKey.Enter))
                    exit = true;
                else if (consoleKey.Key == ConsoleKey.Escape)
                {
                    if (cancelOnEscape)
                    {
                        exit = true;
                        isCancelled = true;
                        sb.Clear();
                    }
                }
                else if (consoleKey.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                        sb.Remove(sb.Length - 1, 1);
                }
                else if (customChecker(consoleKey))
                {
                    sb.Append(consoleKey.KeyChar);
                }
                Console.Clear();
                if (sb.Length > 0)
                {
                    Console.Write("".PadRight(sb.Length, charMask));
                }
            } while (!exit);
            Console.WriteLine();
            return new ConsoleKeyData(sb.ToString(), isCancelled);
        }
    }
}
