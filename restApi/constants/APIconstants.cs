using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.constants
{
    public static class APIconstants
    {
        #region AuthenticationController
        public const string login = "login";
        #endregion
        #region TodoController
        public const string TodoAdd = "";
        public const string TodoUpdate = "{id}";
        public const string TodoGetById = "{id}";
        public const string TodoFilters = "";
        #endregion
    }
}
