using System;
using System.Text.Json;
using BudgetCLI.Models;

class Program
{
    static void Main(string[] args)
    {
        List<Transaction> transactions = new List<Transaction>();
        
        transactions = LoadTransactionsFromFile("Transactions.json");
        
        bool isRunning = true;
        while(isRunning)
        {  
            Console.WriteLine("---BudgetCLI---");
            Console.WriteLine("[1] - Add Transaction");
            Console.WriteLine("[2] - Show Transactions");
            Console.WriteLine("[5] - Exit");
            Console.WriteLine("-----------------");

            Console.Write("Select action: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int selectedAction))
            {
                Console.WriteLine("You didn't specify a number!");
                continue;
            }

            switch(selectedAction)
            {
                case 1:
                    AddTransaction(transactions);
                    Console.WriteLine("Transaction added.");
                    SaveTransactionsToFile(transactions, "Transactions.json");
                    break;
                case 2:
                    Console.Write("Do you want to use filtering? [1] - YES [0] - NO: ");
                    string filter = Console.ReadLine();

                    if (!int.TryParse(filter, out int isFiltered))
                    {
                        Console.WriteLine("You didn't specify a number!");
                        continue;
                    }

                    switch(isFiltered)
                    {
                        case 0:
                            Show(transactions);
                            break;
                        case 1:
                            Console.WriteLine("---FILTER---");
                            Console.WriteLine("[1] - Filter by Date");
                            Console.WriteLine("[2] - Filter by Category");

                            Console.Write("Select a filter: ");
                            string whichFilter = Console.ReadLine();

                            if (!int.TryParse(whichFilter, out int selectedFilter))
                            {
                                Console.WriteLine("You didn't specify a number!");
                                continue;
                            }

                            if(selectedFilter == 1)
                            {
                                var transactionsOrderedByDate = transactions.OrderBy(t => t.Date).ToList();
                                Show(transactionsOrderedByDate);
                            }
                            else if(selectedFilter == 2)
                            {
                                var transactionsGroupedByCategory = transactions.GroupBy(t => t.Category).ToList();

                                foreach (var group in transactionsGroupedByCategory)
                                {
                                    Console.WriteLine($"Category: {group.Key}");
                                    foreach (var transaction in group)
                                    {
                                        Console.WriteLine($"  Amount: {transaction.Amount}, Description: {transaction.Description}, Date: {transaction.Date}");
                                    }
                                    Console.WriteLine("-------------------");
                                }
                            }
                            else 
                            {
                                Console.WriteLine("That action does not exist!");
                            }
                            break;
                        default:
                            Console.WriteLine("Enter a good number!");
                            continue;
                    }
                    break;
                case 5:
                    Console.WriteLine("Exiting...");
                    SaveTransactionsToFile(transactions, "Transactions.json");
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

    public static void SaveTransactionsToFile(List<Transaction> transactions, string filePath)
    {
        string json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(filePath, json);
        Console.WriteLine($"Transactions saved to {filePath}");
    }

    public static List<Transaction> LoadTransactionsFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File {filePath} does not exist. Returning an empty list.");
            return new List<Transaction>();
        }

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
    }
    
}
