using Library.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace DomainEntities.Application
{
    /// <summary>
    /// Singleton Pattern to hold Web.Configuration AppSettings values
    /// </summary>
    /// <remarks>uses Lazy<T> object</remarks>
    public sealed class Configuration
    {
        private Configuration() { }

        private static readonly Lazy<Configuration> configuration = new Lazy<Configuration>(() => new Configuration(), true);

        public static Configuration Instance { get { return configuration.Value; } }

        //  Web configuration AppSettings as properities
        public string PlaceBaseUrl => ConfigurationManager.AppSettings["PlaceBaseUrl"];

        public bool? DisplayErrors => ConfigurationManager.AppSettings["DisplayErrors"].ToNullableBool();

    }
}
