#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Utility.AspNetCore.Infrastructure
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly ConcurrentDictionary<string, string> _all;

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly LocalizationOptions _options;

        private readonly string _baseResourceName;
        private readonly CultureInfo _cultureInfo;

        public LocalizedString this[string name] => Get(name);
        public LocalizedString this[string name, params object[] arguments] => Get(name, arguments);

        public JsonStringLocalizer(IHostingEnvironment hostingEnvironment, LocalizationOptions options, string baseResourceName, CultureInfo culture)
        {
            _options = options;
            _hostingEnvironment = hostingEnvironment;

            _cultureInfo = culture ?? CultureInfo.CurrentUICulture;
            _baseResourceName = baseResourceName + "." + _cultureInfo.Name;
            _all = GetAll();

        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _all.Select(t => new LocalizedString(t.Key, t.Value, true)).ToArray();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            if (culture == null)
                return this;

            CultureInfo.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;

            return new JsonStringLocalizer(_hostingEnvironment, _options, _baseResourceName, culture);
        }

        private LocalizedString Get(string name, params object[] arguments)
        {
            if (_all.ContainsKey(name))
            {
                var current = _all[name];
                return new LocalizedString(name, string.Format(_all[name], arguments));
            }
            return new LocalizedString(name, name, true);
        }

        private ConcurrentDictionary<string, string> GetAll()
        {
            var file = Path.Combine(_hostingEnvironment.ContentRootPath, _baseResourceName + ".json");
            if (!string.IsNullOrEmpty(_options.ResourcesPath))
                file = Path.Combine(_hostingEnvironment.ContentRootPath, _options.ResourcesPath, _baseResourceName + ".json");

            Debug.WriteLineIf(!File.Exists(file), "Path not found! " + file);

            if (!File.Exists(file))
                return new ConcurrentDictionary<string, string>();

            try
            {
                var txt = File.ReadAllText(file);

                return JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>(txt);
            }
            catch (Exception)
            {
            }

            return new ConcurrentDictionary<string, string>();
        }
    }
}
#endif