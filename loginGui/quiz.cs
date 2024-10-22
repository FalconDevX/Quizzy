namespace loginGui
{
    public class Question
    {
        private String answer;
        private bool isTrue;
        public Question(String A, bool B)
        {
            answer = A;
            isTrue = B;
        }
    }
    public class Quiz
    {
        private String question;
        private Question[] array = new Question[4];
        public Quiz(String S)
        {
            question = S;
        }
    }


}
