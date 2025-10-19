namespace SolidShortener.Infrastructure.Services;

public class Base62ShortCodeGenerator : IShortCodeGenerator
{
    private const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public string Generate(long id)
    {
        if (id == 0) return Base62Chars[0].ToString();

        var shortCode = new StringBuilder();
        while (id > 0)
        {
            shortCode.Insert(0, Base62Chars[(int)(id % 62)]);
            id /= 62;
        }

        return shortCode.ToString();
    }
}
