namespace IntermediateTest2.Domain.ValueObjects
{
    public class ResponseToken
    {
        public ResponseToken(bool authenticated, string message, string created = null, string expiration = null,
            string accessToken = null)
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            Message = message;
        }

        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
    }
}