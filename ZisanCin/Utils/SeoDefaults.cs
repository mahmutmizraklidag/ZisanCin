namespace ZisanCin.Utils;

public static class SeoDefaults
{
    public const string BaseUrl = "https://diyetcin.com";
    public const string SiteName = "Diyetisyen Zişan Cin";
    public const string DefaultTitle = "Bayraklı Diyetisyen Zişan Cin | İzmir Beslenme Danışmanlığı";
    public const string DefaultDescription = "İzmir Bayraklı'da Diyetisyen Zişan Cin ile kişiye özel, bilimsel ve sürdürülebilir yüz yüze veya online beslenme danışmanlığı alın.";

    public static string AbsoluteUrl(string? path)
    {
        if (string.IsNullOrWhiteSpace(path) || path == "/")
            return BaseUrl + "/";

        if (Uri.TryCreate(path, UriKind.Absolute, out var absolute))
            return absolute.AbsoluteUri;

        return new Uri(new Uri(BaseUrl + "/"), path.TrimStart('~', '/')).AbsoluteUri;
    }
}
