namespace NewLoginGui
{
    internal static class Program
    {
        struct Answer
        {
            string q;
            bool isTrue;
        }
        class Quiz
        {
            string quest;
            bool hasPicture;
            Answer[] ans = new Answer[4];
            public Quiz(string quest)
            {
                this.quest = quest;
            }
        }
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new StartScreen());
        }
    }
}