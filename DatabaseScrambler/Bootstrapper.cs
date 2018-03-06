using Autofac;
using DatabaseScrambler.Infrastructure;

namespace DatabaseScrambler
{
    public class Bootstrapper
    {
        public static Infrastructure.IContainer Bootstrap()
        {
            var container = new AutofacContainer(
                builder =>
                {
                    builder.RegisterModule<ScramblerModule>();
                });

            container.Add((Infrastructure.IContainer)container);

            //ServiceLocator.SetContainer(container);

            return container;
        }
    }
}