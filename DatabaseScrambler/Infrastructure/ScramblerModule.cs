using Autofac;

namespace DatabaseScrambler.Infrastructure
{
    public class ScramblerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ScrambleCoordinator).Assembly)
                .AsImplementedInterfaces();
        }
    }
}
