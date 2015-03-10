namespace Qvc.Exception
{
    public class InvalidException : System.Exception
    {
        public InvalidException(string fieldName, string errorMessage)
        {
            FieldName = fieldName;
            ErrorMessage = errorMessage;
        }

        public string FieldName { get; set; }

        public string ErrorMessage { get; set; }
    }
}