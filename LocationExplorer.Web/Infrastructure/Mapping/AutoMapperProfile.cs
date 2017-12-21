namespace LocationExplorer.Web.Infrastructure.Mapping
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Common.Mapping;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            var allTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.Contains("LocationExplorer"))
                .SelectMany(a => a.GetTypes())
                .ToList();

            allTypes.Where(t =>
                    t.IsClass
                    && !t.IsAbstract
                    && t.GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => i.GetGenericTypeDefinition())
                        .Contains(typeof(IMapFrom<>)))
                .Select(t => new
                {
                    Destination = t,
                    Source = t
                        .GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => new
                        {
                            Definition = i.GetGenericTypeDefinition(),
                            Arguments = i.GetGenericArguments()
                        })
                        .Where(i => i.Definition == typeof(IMapFrom<>))
                        .SelectMany(i => i.Arguments)
                        .First()
                })
                .ToList()
                .ForEach(m => this.CreateMap(m.Source, m.Destination));

            allTypes
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && typeof(IHaveCustomMapping).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHaveCustomMapping>()
                .ToList()
                .ForEach(t => t.ConfigureMapping(this));
        }
    }
}
