using System.Security.AccessControl;

namespace BudgetCLI.Models;

public class Transaction
{
    public int Amount {get; set;}
    public string? Category {get; set;}
    public string? Description {get; set;}
    public DateTime Date {get; set;}

    public Transaction(int amount, string category, string description, DateTime date)
    {
        Amount = amount;
        Category = category;
        Description = description;
        Date = date;
    }

    

}