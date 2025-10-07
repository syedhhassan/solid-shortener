namespace SolidShortener.Application.Interfaces.Services;

public interface IShortCodeGenerator
{
    string Generate(long id);
}