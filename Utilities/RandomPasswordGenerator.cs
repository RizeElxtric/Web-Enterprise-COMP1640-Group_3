namespace MarketingEvent.Api.Utilities
{
    public class RandomPasswordGenerator
    {
        public string GenerateRandomPassword()
        {
            Random rng = new Random();
            char[] chars = new char[8];
            for(int i = 0;i<4;i++)
            {
                chars[i] = (char)rng.Next('a', 'z'+1);
            }
            for(int i=4;i<8;i++)
            {
                chars[i] = (char)rng.Next('0', '9' + 1);
            }
            return chars.ToString();
        }
    }
}
