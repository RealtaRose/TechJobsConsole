using System;
using System.Collections.Generic;

namespace TechJobsConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create dictionary for top-level menu options(actionChoices)
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            // Create dictionary for job data(columnChoices)
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");


            // Allow user to search/list until they manually quit with ctrl+c
            while (true)
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices); // Ask user to view jobs by search or list, then store that response as a string(actionChoice)

                if (actionChoice.Equals("list")) //If user selects view by list
                {

                    string columnChoice = GetUserSelection("List", columnChoices); // Ask user which list they would like to view, and store their response as a string(columnChoice)

                    if (columnChoice.Equals("all")) //If user selects view all lists
                    {
                        //print all lists
                        PrintJobs(JobData.FindAll());

                    }

                    else // If user selects one list
                    {

                        List<string> results = JobData.FindAll(columnChoice); //Create a list of search results(results) from the data base based on user's choice(columnChoice)


                        //Print the user's desired list using the reuslts listvvvvvvvvv
                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        {

                            Console.WriteLine(item);

                        }
                        //Print the user's desired list using the results list^^^^^^^^^^

                    }

                }

                else // choice is "search"
                {

                    // How does the user want to search (e.g. by skill or employer)
                    string columnChoice = GetUserSelection("Search", columnChoices);

                    // Ask user for search term
                    Console.WriteLine("\nSearch term: ");
                    string searchTerm = Console.ReadLine().ToLower();

                    List<Dictionary<string, string>> searchResults;

                    // Fetch results
                    if (columnChoice.Equals("all"))
                    {

                        searchResults = JobData.FindByValue(searchTerm);
                        PrintJobs(searchResults);

                    }

                    else
                    {

                        searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        PrintJobs(searchResults);

                    }

                }

            }

        }




        /*
         * Returns the key of the selected item from the choices Dictionary
         */
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {

            int choiceIdx = -1; //will house user's choice as an integer
            bool isValidChoice = false; // will update to true once user has made a valid input


            string[] choiceKeys = new string[choices.Count]; // Create a string of arrays(choiceKeys) as big as the user has options(choices)
            int i = 0;

            foreach (KeyValuePair<string, string> choice in choices) //Fill choiceKeys with the keys for each option
            {

                choiceKeys[i] = choice.Key;
                i++;

            }

            do
            {
                //display to the user all availible options
                Console.WriteLine("\n" + choiceHeader + " by:");

                for (int j = 0; j < choiceKeys.Length; j++)
                {

                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);

                }

                //store user's choice of options as an integer (choiceIdx)
                string input = Console.ReadLine().ToLower();

                choiceIdx = int.Parse(input);


                //Check if user input is valid
                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length) // user input was not valid, isValidChoice remains false
                {

                    Console.WriteLine("Invalid choices. Try again.");

                }

                else // user input is valid, isValidChoice updates to true
                {

                    isValidChoice = true;

                }

            } while (!isValidChoice); //Repeat loop if user input is invalid

            return choiceKeys[choiceIdx];

        }



        private static void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            if (someJobs.Count == 0)
            {
                Console.WriteLine("No search results found.");
            }

            foreach (Dictionary<string, string> job in someJobs) // For each job dictionary matching user's search
            {

                Console.WriteLine("*****");

                foreach (KeyValuePair<string, string> details in job) //
                {

                    Console.WriteLine(details.Key + ": " + details.Value);

                }
                Console.WriteLine("*****");

            }

        }

    }

}
