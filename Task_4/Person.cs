namespace Task_4
{
    public abstract class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        private string identificationNumber;
        public string IdentificationNumber
        {
            get { return identificationNumber; }

            set
            {
                if (value.Length == 11)
                {
                    identificationNumber = value;
                }
                else
                {
                    throw new Exception("Length of the identification Number should be 11");
                }
            }
        }

    }
}
