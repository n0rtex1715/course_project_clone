namespace WebApplicationMVC.Models
{
    public class StringGeneratorService
    {
        public IEnumerable<string> Generate()
        {
            var rand = new Random();
            var strings = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                strings.Add(rand.Next(100, 500).ToString());
            }
            return strings;
        }
    }
}
