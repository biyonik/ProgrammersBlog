namespace ProgrammersBlog.Shared.Utilities.Results.Abstract
{
    public interface IDataResult<out T>: IResult
    {
        public T Data { get;}
        
    }
}