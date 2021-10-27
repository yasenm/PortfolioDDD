namespace Portfolio.Domain.Posts.Exceptions
{
    using Portfolio.Common.Domain;

    public class InvalidPostException : BaseDomainException
    {
        public InvalidPostException()
        {

        }

        public InvalidPostException(string error) => this.Error = error;
    }
}
