using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTravelingSalesperson
{
    public static class ConsoleValidator
    {
        
        public static bool TryGetIntegerFromUser(int minValue, int maxValue, int maxAttempts, string pluralName, out int userInteger)
        {
            bool validInput = false;
            bool maxAttemptsExceeded = false;
            string userResponse;
            string feedbackMessage = "";
            int attempts = 1;
            userInteger = 0;

            while (!validInput && !maxAttemptsExceeded)
            {

                if (attempts <= maxAttempts)
                {
                    ConsoleUtil.DisplayPromptMessage($"Please enter the number between {minValue} and {maxValue}, of {pluralName}:");
                    userResponse = Console.ReadLine();
                    ConsoleUtil.DisplayMessage("");

                    if (int.TryParse(userResponse, out userInteger))
                    {

                        if (userInteger >= minValue && userInteger <= maxValue)
                        {
                            validInput = true;
                        }                        
                        else
                        {
                            feedbackMessage = $"The number {userInteger} is not in the specified range.";
                        }
                    }
                   
                    else
                    {
                        feedbackMessage = $"{userResponse} is not an integer.";
                    }

                    if (!validInput && attempts <= maxAttempts)
                    {
                        ConsoleUtil.DisplayMessage($"You entered: {userResponse}");
                        ConsoleUtil.DisplayMessage(feedbackMessage);

                        if (attempts < maxAttempts)
                        {
                            ConsoleUtil.DisplayMessage($"Please enter an integer between {minValue} and {maxValue}.");
                            ConsoleUtil.DisplayMessage("Press any key to try again.");
                            Console.ReadKey();
                        }
                        else
                        {
                            ConsoleUtil.DisplayMessage("It appears you have exceeded the maximum number of attempts allowed.");
                            ConsoleUtil.DisplayMessage("Press any key to continue.");
                            Console.ReadKey();
                        }

                        Console.Clear();
                    }
                    else
                    {
                        ConsoleUtil.DisplayMessage("");
                    }

                    attempts++;
                }
                else
                {
                    maxAttemptsExceeded = true;
                }
            }

            return validInput;
        }

        public static string GetYesNoFromUser(int maxAttempts, string userPrompt, out bool maxAttemptsExceeded)
        {
            bool validInput = false;
            maxAttemptsExceeded = false;
            string userResponse = "";
            int attempts = 1;

            while (!validInput && !maxAttemptsExceeded)
            {
                Console.Write($"{userPrompt} [Yes / No] ");
                userResponse = Console.ReadLine();
                ConsoleUtil.DisplayMessage("");

                if (userResponse.ToUpper() == "YES" || userResponse.ToUpper() == "NO")
                {
                    validInput = true;
                }
               
                else
                {
                    ConsoleUtil.DisplayMessage($"You entered: {userResponse}");
                    ConsoleUtil.DisplayMessage($"\"{userResponse}\" is not a valid response.");

                    
                    if (attempts < maxAttempts)
                    {
                        ConsoleUtil.DisplayMessage($"Please enter either \"Yes\" or \"No\".");
                        ConsoleUtil.DisplayMessage("Press any key to try again.");
                    }                   
                    else
                    {
                        ConsoleUtil.DisplayMessage("It appears you have exceeded the maximum number of attempts allowed.");
                        ConsoleUtil.DisplayMessage("Press any key to continue.");
                        maxAttemptsExceeded = true;
                    }

                    Console.ReadKey();
                    Console.Clear();
                }

                attempts++;
            }

            return userResponse;
        }
    }
}