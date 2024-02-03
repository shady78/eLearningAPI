namespace eLearningAPI.Helpers
{
    public class JWT
    {
        public string ValidIssuer { get; set; }
        public string ValidAudiance { get; set; }
        public double DurationInDay { get; set; }
        public string Secret { get; set; }
    }
}
