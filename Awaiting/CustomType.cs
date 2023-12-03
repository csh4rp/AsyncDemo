namespace Awaiting;

public class CustomType
{
    public CustomAwaiter GetAwaiter() => new();
}