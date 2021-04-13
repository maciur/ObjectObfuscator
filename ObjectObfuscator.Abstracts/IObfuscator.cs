namespace ObjectObfuscator.Abstracts
{
    public interface IObfuscator
    {
        object Handle(object @object);
        TObject Handle<TObject>(TObject @object) 
            where TObject : class;
    }
}