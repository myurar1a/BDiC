namespace BDiC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Test test = new Test();
            //await test.ConfirmationOfArrivalDateAsync();
            //await test.GetTest();
            await test.ConfirmationOfIncomeListAsync();
        }
    }
}