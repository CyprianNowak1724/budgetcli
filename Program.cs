using System;
using BudgetCLI.Models;

class Program
{
    static void Main(string[] args)
    {
        List<Transaction> transactions = new List<Transaction>();
        
        bool isRunning = true;
        while(isRunning)
        {
            Console.Clear();
            
            Console.WriteLine("---BudgetCLI---");
            Console.WriteLine("[1] - Add Transaction");
            Console.WriteLine("[2] - Show Transactions");
            Console.WriteLine("[5] - Exit");
            Console.WriteLine("-----------------");

            Console.Write("Select action: ");
            string input = Console.ReadLine();

            if(int.TryParse(input, out int selectedAction))
            {
                continue;
            }
            else 
            {
                Console.WriteLine("You didn't specify a number!");
            }

            switch(selectedAction)
            {
                case 1:
                    AddTransaction(transactions);
                    Console.WriteLine("Transaction added.");
                    break;
                case 2:
                    Show(transactions);
                    break;
                case 5:
                    Console.WriteLine("Exiting...");
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("That action does not exist!");
                    break;
            }
            
        }
    }
    public static void AddTransaction(List<Transaction> transactions)
    {
        Console.Write("Specify amount: ");
        int amount = int.Parse(Console.ReadLine());
        Console.WriteLine("");  

        Console.Write("Specify category: ");
        string? category = Console.ReadLine();
        Console.WriteLine("");

        Console.Write("Specify description: ");
        string? desc = Console.ReadLine();
        Console.WriteLine("");

        DateTime date = DateTime.Now;

        Transaction transaction = new Transaction(amount, category, desc, date);
        transactions.Add(transaction);
    }
    public static void Show(List<Transaction> transactions)
    {
        Console.WriteLine($"---Transactions---");
        foreach(Transaction transaction in transactions)
        {
            Console.WriteLine($"Amount: {transaction.Amount}");
            Console.WriteLine($"Category: {transaction.Category}");
            Console.WriteLine($"Description: {transaction.Description}");
            Console.WriteLine($"Date: {transaction.Date}");
            Console.WriteLine("-------------------");
        }
    }
}
