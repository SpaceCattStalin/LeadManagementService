using Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static void AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHttpClient<IUserServiceClient, UserServiceClient>(client =>
        {
            client.BaseAddress = new Uri("");
        });
        builder.Services.AddHttpClient<ICollegeInfoServiceClient, CollegeInfoService>(client =>
        {
            client.BaseAddress = new Uri("");
        });
    }
}
