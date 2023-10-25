namespace TelegramBotApp
{
    public class Distinct
    {
        public void Execute()
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            int sCount = Convert.ToInt32(Console.ReadLine().Trim());

            List<string> s = new List<string>();

            for (int i = 0; i < sCount; i++)
            {
                string sItem = Console.ReadLine();
                s.Add(sItem);
            }

            int tCount = Convert.ToInt32(Console.ReadLine().Trim());

            List<string> t = new List<string>();

            for (int i = 0; i < tCount; i++)
            {
                string tItem = Console.ReadLine();
                t.Add(tItem);
            }

            textWriter.Flush();
            textWriter.Close();
        }
    }
}
