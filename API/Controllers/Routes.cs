namespace API.Controllers;

public static class Routes
{
    public const string ApiPrefix = "api/v1";

    public static class Producers
    {
        public const string Prefix = $"{ApiPrefix}/producers";
        public const string ById = "{id:guid}";
        public const string Licenses = "{id:guid}/licenses";
        public const string Compliance = "{id:guid}/compliance";
    }
}
