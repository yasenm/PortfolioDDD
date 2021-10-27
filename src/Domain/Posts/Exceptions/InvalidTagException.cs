namespace Portfolio.Domain.Posts.Exceptions
{
    using Portfolio.Common.Domain;

    public class InvalidTagException : BaseDomainException
    {
        public InvalidTagException()
        {

        }

        public InvalidTagException(string error) => this.Error = error;
    }
}
