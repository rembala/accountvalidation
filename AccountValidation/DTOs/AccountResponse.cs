namespace AccountPresentationLayer.DTOs
{
    public class AccountResponse
    {
        public bool FileValid { get; set; }

        public List<string> invalidLines { get; set; }
    }
}
