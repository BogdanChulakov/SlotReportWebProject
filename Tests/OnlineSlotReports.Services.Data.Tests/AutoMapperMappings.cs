namespace OnlineSlotReports.Services.Data.Tests
{
    using System;
    using System.Reflection;

    using OnlineSlotReports.Services.Mapping;
    using OnlineSlotReports.Web.ViewModels;

    public class AutoMapperMappings : IDisposable
    {
        public AutoMapperMappings()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}