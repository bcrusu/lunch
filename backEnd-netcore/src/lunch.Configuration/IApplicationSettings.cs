namespace lunch.Configuration
{
    public interface IApplicationSettings
    {
        string ConnectionString { get; }

        string DefaultAccessControlAllowOrigin { get; }

        string LinkedinClientSecret { get; }

        byte[] JwtSignKey { get; }
    }
}
