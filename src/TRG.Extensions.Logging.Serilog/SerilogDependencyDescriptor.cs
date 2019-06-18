﻿using TRG.Extensions.DependencyInjection;

namespace TRG.Extensions.Logging.Serilog
{
    internal class SerilogDependencyDescriptor : DependencyDescriptor
    {
        public SerilogDependencyDescriptor()
        {
            IncludeRegister(container =>
            {
                container.RegisterConditional(
                    typeof(ILogger),
                    c => typeof(SerilogLogger<>).MakeGenericType(c.Consumer.ImplementationType),
                    SimpleInjector.Lifestyle.Singleton,
                    c => true);
            });
        }
    }
}