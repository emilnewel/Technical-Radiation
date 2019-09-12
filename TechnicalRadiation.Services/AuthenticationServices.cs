namespace TechnicalRadiation.Services
{
    public class AuthenticationServices
    {
        private string AuthToken = "RosaLeyniToken";

        public bool Validate(string token){
            if(token == AuthToken) return true;
            return false;
        }
    }
}