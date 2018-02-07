using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTravelingSalesperson
{
    
    public class ConsoleView
    {
        #region FIELDS

        private const int MAXIMUM_ATTEMPTS = 3;
        private const int MINIMUM_BUYSELL_AMOUNT = 0;
        private const int MAXIMUM_BUYSELL_AMOUNT = 100;

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        public ConsoleView()
        {
            InitializeConsole();
        }

        #endregion

        #region METHODS

       

        private void InitializeConsole()
        {
            ConsoleUtil.HeaderText = "The Traveling Salesperson Application";
        }

        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            ConsoleUtil.DisplayMessage("");

            Console.CursorVisible = true;
        }

     
        public void DisplayExitPrompt()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Thank you for using the application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }


       
        public void DisplayWelcomeScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.DisplayReset();

            sb.Clear();
            sb.AppendFormat("You are a traveling salesperson buying and selling products ");
            sb.AppendFormat("around the country. You will be prompted regarding which city ");
            sb.AppendFormat("you wish to travel to and will then be asked whether you wish to buy ");
            sb.AppendFormat("or sell products.");
            ConsoleUtil.DisplayMessage(sb.ToString());
            ConsoleUtil.DisplayMessage("");

            sb.Clear();
            sb.AppendFormat("Your first task will be to set up your account details.");
            ConsoleUtil.DisplayMessage(sb.ToString());

            DisplayContinuePrompt();
        }

        public Salesperson DisplaySetupAccount()
        {
            Salesperson salesperson = new Salesperson();

            ConsoleUtil.HeaderText = "Account Setup";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Setup your account now.");
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your first name: ");
            salesperson.FirstName = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your last name: ");
            salesperson.LastName = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your account ID: ");
            salesperson.AccountID = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayMessage("Product Types");
            ConsoleUtil.DisplayMessage("");

           
            foreach (string productTypeName in Enum.GetNames(typeof(Product.ProductType)))
            {
            
                if (productTypeName != Product.ProductType.None.ToString())
                {
                    ConsoleUtil.DisplayMessage(productTypeName);
                }

            }

          
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayPromptMessage("Enter the product type: ");
            Product.ProductType productType;
            if (Enum.TryParse<Product.ProductType>(UppercaseFirst(Console.ReadLine()), out productType))
            {
                salesperson.CurrentStock.Type = productType;
            }
            else
            {
                salesperson.CurrentStock.Type = Product.ProductType.None;
            }

           
            if (ConsoleValidator.TryGetIntegerFromUser(0, 100, 3, "products", out int numberOfUnits))
            {
                salesperson.CurrentStock.AddProducts(numberOfUnits);
            }
            else
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficulty setting the number of products in your stock.");
                ConsoleUtil.DisplayMessage("By default, the number of products in your inventory are now set to zero.");
                salesperson.CurrentStock.AddProducts(0);
                DisplayContinuePrompt();
            }

            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Your account has been setup");

            DisplayContinuePrompt();

            return salesperson;
        }

       
        public void DisplayClosingScreen()
        {
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thank you for using The Traveling Salesperson Application.");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// get the menu choice from the user
        /// </summary>
        public MenuOption DisplayGetUserMenuChoice()
        {
            MenuOption userMenuChoice = MenuOption.None;
            bool usingMenu = true;

            while (usingMenu)
            {
                //
                // set up display area
                //
                ConsoleUtil.DisplayReset();
                Console.CursorVisible = false;

                //
                // display the menu
                //
                ConsoleUtil.DisplayMessage("Please type the number of your menu choice.");
                ConsoleUtil.DisplayMessage("");
                Console.Write(
                    "\t" + "1. Travel" + Environment.NewLine +
                    "\t" + "2. Buy" + Environment.NewLine +
                    "\t" + "3. Sell" + Environment.NewLine +
                    "\t" + "4. Display Inventory" + Environment.NewLine +
                    "\t" + "5. Display Cities" + Environment.NewLine +
                    "\t" + "6. Display Account Info" + Environment.NewLine +
                    "\t" + "E. Exit" + Environment.NewLine);

                //
                // get and process the user's response
                // note: ReadKey argument set to "true" disables the echoing of the key press
                //
                ConsoleKeyInfo userResponse = Console.ReadKey(true);
                switch (userResponse.KeyChar)
                {
                    case '1':
                        userMenuChoice = MenuOption.Travel;
                        usingMenu = false;
                        break;
                    case '2':
                        userMenuChoice = MenuOption.Buy;
                        usingMenu = false;
                        break;
                    case '3':
                        userMenuChoice = MenuOption.Sell;
                        usingMenu = false;
                        break;
                    case '4':
                        userMenuChoice = MenuOption.DisplayInventory;
                        usingMenu = false;
                        break;
                    case '5':
                        userMenuChoice = MenuOption.DisplayCities;
                        usingMenu = false;
                        break;
                    case '6':
                        userMenuChoice = MenuOption.DisplayAccountInfo;
                        usingMenu = false;
                        break;
                    case 'E':
                    case 'e':
                        userMenuChoice = MenuOption.Exit;
                        usingMenu = false;
                        break;
                    default:
                        ConsoleUtil.DisplayMessage(
                            "It appears you have selected an incorrect choice." + Environment.NewLine +
                            "Press any key to continue or the ESC key to quit the application.");

                        userResponse = Console.ReadKey(true);
                        if (userResponse.Key == ConsoleKey.Escape)
                        {
                            usingMenu = false;
                        }
                        break;
                }
            }
            Console.CursorVisible = true;

            return userMenuChoice;
        }
        
        public string DisplayGetNextCity()
        {
            string nextCity = "";

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayPromptMessage("Enter the name of the next city:");
            nextCity = Console.ReadLine();

            return nextCity;
        }

      
        public int DisplayGetNumberOfUnitsToBuy(Product product)
        {
            ConsoleUtil.HeaderText = "Buy Inventory";
            ConsoleUtil.DisplayReset();

          
            ConsoleUtil.DisplayMessage("Buying " + product.Type.ToString() + " products.");
            ConsoleUtil.DisplayMessage("");

            if (!ConsoleValidator.TryGetIntegerFromUser(MINIMUM_BUYSELL_AMOUNT, MAXIMUM_BUYSELL_AMOUNT, MAXIMUM_ATTEMPTS, "products", out int numberOfUnitsToBuy))
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficulty setting the number of products to buy.");
                ConsoleUtil.DisplayMessage("By default, the number of products to sell will be set to zero.");
                numberOfUnitsToBuy = 0;
                DisplayContinuePrompt();
            }

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage(numberOfUnitsToBuy + " " + product.Type.ToString() + " products have been added to the inventory.");

            DisplayContinuePrompt();

            return numberOfUnitsToBuy;
        }

        public int DisplayGetNumberOfUnitsToSell(Product product)
        {
            ConsoleUtil.HeaderText = "Sell Inventory";
            ConsoleUtil.DisplayReset();

          
            ConsoleUtil.DisplayMessage("Selling " + product.Type.ToString() + " products.");
            ConsoleUtil.DisplayMessage("");

            if (!ConsoleValidator.TryGetIntegerFromUser(MINIMUM_BUYSELL_AMOUNT, MAXIMUM_BUYSELL_AMOUNT, MAXIMUM_ATTEMPTS, "products", out int numberOfUnitsToSell))
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficulty setting the number of products to sell.");
                ConsoleUtil.DisplayMessage("By default, the number of products to sell will be set to zero.");
                numberOfUnitsToSell = 0;
                DisplayContinuePrompt();
            }

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage(numberOfUnitsToSell + " " + product.Type.ToString() + " products have been subtracted from the inventory.");

            DisplayContinuePrompt();

            return numberOfUnitsToSell;
        }

        
        public void DisplayBackorderNotification(Product product, int numberOfUnitsSold)
        {
            ConsoleUtil.HeaderText = "Inventory Backorder Notification";
            ConsoleUtil.DisplayReset();

            int numberOfUnitsBackordered = Math.Abs(product.NumberOfUnits);
            int numberOfUnitsShipped = numberOfUnitsSold - numberOfUnitsBackordered;

            ConsoleUtil.DisplayMessage("Products Sold: " + numberOfUnitsSold);
            ConsoleUtil.DisplayMessage("Products Shipped: " + numberOfUnitsShipped);
            ConsoleUtil.DisplayMessage("Products on Backorder: " + numberOfUnitsBackordered);

            DisplayContinuePrompt();
        }

        
        public void DisplayInventory(Product product)
        {
            ConsoleUtil.HeaderText = "Current Inventory";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Product type: " + product.Type.ToString());
            ConsoleUtil.DisplayMessage("Number of units: " + product.NumberOfUnits.ToString());
            ConsoleUtil.DisplayMessage("");

            DisplayContinuePrompt();
        }

       
        public void DisplayCitiesTraveled(Salesperson salesperson)
        {
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("You have traveled to the following cities.");
            ConsoleUtil.DisplayMessage("");

            foreach (string city in salesperson.CitiesVisited)
            {
                ConsoleUtil.DisplayMessage(city);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the current account information
        /// </summary>
        public void DisplayAccountInfo(Salesperson salesperson)
        {
            ConsoleUtil.HeaderText = "Account Info";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("First Name: " + salesperson.FirstName);
            ConsoleUtil.DisplayMessage("Last Name: " + salesperson.LastName);
            ConsoleUtil.DisplayMessage("Account ID: " + salesperson.AccountID);
            ConsoleUtil.DisplayMessage("Product Type: " + salesperson.CurrentStock.Type);
            if (!salesperson.CurrentStock.OnBackorder)
            {
                ConsoleUtil.DisplayMessage("Units of Products in Inventory: " + salesperson.CurrentStock.NumberOfUnits);
            }
            else
            {
                ConsoleUtil.DisplayMessage("Units of Products on Backorder: " + Math.Abs(salesperson.CurrentStock.NumberOfUnits));
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// changes string to lowercase with first letter uppercase
        /// adapted from: https://www.dotnetperls.com/uppercase-first-letter
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static string UppercaseFirst(string s)
        {
            
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }

        #endregion
    }
}