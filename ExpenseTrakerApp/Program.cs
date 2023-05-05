namespace ExpenseTrackerApp
{
    class Transaction
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"Title: {Title}, Description: {Description}, Amount: {Amount}, Date: {Date.ToString("dd/mm/yyyy")}";
        }
    }

    class Expense : Transaction
    {
        public Expense(string title, string description, decimal amount, DateTime date)
        {
            Title = title;
            Description = description;
            Amount = -Math.Abs(amount);
            Date = date;
        }
    }

    class Income : Transaction
    {
        public Income(string title, string description,
        decimal amount, DateTime date)
        {
            Title = title;
            Description = description;
            Amount = Math.Abs(amount);
            Date = date;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ExpenseTracker tracker = new ExpenseTracker();
            int choice = 0;
            while (true)
            {
                Console.WriteLine("Welcome to Expense Tracker App!");
                Console.WriteLine("Please select an option from the menu below:");
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. View Expenses");
                Console.WriteLine("3. View Income");
                Console.WriteLine("4. Check Available Balance");
                try
                {
                    Console.WriteLine("Enter Your choice: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter only Numbers");
                }

                switch (choice)
                {
                    case 1:
                        tracker.AddTransaction();
                        break;
                    case 2:
                        tracker.ViewExpenses();
                        break;
                    case 3:
                        tracker.ViewIncome();
                        break;
                    case 4:
                        tracker.CheckBalance();
                        break;
                    default:
                        Console.WriteLine("Wrong Choice Entered. Please try again.");
                        break;
                }
            }
        }
    }

    class ExpenseTracker
    {
        private List<Transaction> transactions;

        public ExpenseTracker()
        {
            transactions = new List<Transaction>();
        }

        public void AddTransaction()
        {
            Console.WriteLine("Please enter transaction details:");
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter Amount: ");
            decimal amount;
            if (decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.Write("Enter Date (dd/mm/yyyy): ");
                DateTime date;
                if (DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Transaction transaction;
                    if (amount < 0)
                    {
                        transaction = new Expense(title, description, amount, date);
                    }
                    else
                    {
                        transaction = new Income(title, description, amount, date);
                    }
                    transactions.Add(transaction);
                    Console.WriteLine("Transaction Added Successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid Date Format!");
                }
            }
            else
            {
                Console.WriteLine("Invalid Amount Format!");
            }
        }

        public void ViewExpenses()
        {
            Console.WriteLine("Expense Transactions:");
            foreach (Transaction transaction in transactions)
            {
                if (transaction is Expense)
                {
                    Console.WriteLine(transaction);
                }
            }
        }

        public void ViewIncome()
        {
            Console.WriteLine("Income Transactions:");
            foreach (Transaction transaction in transactions)
            {
                if (transaction is Income)
                {
                    Console.WriteLine(transaction);
                }
            }
        }

        public void CheckBalance()
        {
            decimal balance = 0;
            foreach (Transaction transaction in transactions)
            {
                balance += transaction.Amount;
            }
            Console.WriteLine($"Available Balance: {balance}");
        }
    }

    
}