using Common.Interfaces;
using Dal.UserRepositories;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DependenciesRegistrations
    {
        public void SetDependencies(Container container)
        {
            container.Register<IValidation, Validation>(Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
        }
    }
}
