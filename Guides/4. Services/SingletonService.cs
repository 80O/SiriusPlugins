using System.Threading.Tasks;
using Sirius.Api.DependencyInjection;

namespace Services
{
    public interface ISingletonService
    {
        Task Bla();
    }

    /// <summary>
    /// You can use the attributes <see cref="SingletonAttribute"/>, <see cref="ScopedAttribute"/>
    /// and <see cref="TransientAttribute"/> to define the scope & lifetime of the service.
    ///
    /// While Sirius is capable of loading different scopes, by default only one is used. Services
    /// are by default registered as <see cref="ScopedAttribute"/> when no specific attribute is used.
    ///
    /// Its best practice to leave this to default unless you would want to run multiple scopes using the same instance.
    /// </summary>
    [Singleton]
    public class SingletonService : ISingletonService
    {
        public Task Bla()
        {
            return Task.CompletedTask;
        }
    }
}
