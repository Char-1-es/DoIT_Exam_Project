namespace Task_4
{
    public class Account : Person
    {
        private int id;
        public int Id 
        { 
            set
            {
                if (value > 0) 
                { 
                    id = value;
                }
                else
                {
                    throw new Exception("Id should be positive number");
                }
            }
            get { return id; } 
        }
        private float balance;
        public float Balance
        {
            set
            {
                if (value > 0)
                {
                    balance = value;
                }
                else
                {
                    throw new Exception("Insufficient funds.");
                }
            }
            get { return balance; }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (value.Length == 4)
                {
                    password = value;
                }
                else
                {
                    throw new Exception("Password should be 4 characters long");
                }
            }
        }
        public override string ToString()
        {
            return $"{Id}: {Firstname} {Lastname} - ID:{IdentificationNumber}, Balance: {Balance}, Pass: {Password}";
        }
        public static float CheckBalance(Account account)
        {
            return account.Balance;
        }
    }
}