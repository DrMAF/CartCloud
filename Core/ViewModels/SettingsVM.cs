namespace Core.ViewModels
{
    public class JWTSettings
    {
        public string ValidAudience { get; set; }   
        public string ValidSubject { get; set; }
        public string ValidIssuer { get; set; }        
        public string Secret { get; set; }
    }
}
