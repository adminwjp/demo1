﻿//#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
#if NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Runtime.CompilerServices;
using Utility.Json.Extensions;

namespace Utility.AspNetCore.Data
{  
    /// <summary>
   /// Represents data used to build an <c>ApiDescription</c>, stored as part of the
   /// <see cref="Abstractions.ActionDescriptor.Properties"/>.
   /// </summary>
    public class ApiDescriptionActionData
    {
        /// <summary>
        /// The <c>ApiDescription.GroupName</c> of <c>ApiDescription</c> objects for the associated
        /// action.
        /// </summary>
        public string GroupName { get; set; }
    }
    internal class ApiParameterContext
    {
        public ApiParameterContext(
            IModelMetadataProvider metadataProvider,
            ControllerActionDescriptor actionDescriptor,
            IReadOnlyList<TemplatePart> routeParameters)
        {
            MetadataProvider = metadataProvider;
            ActionDescriptor = actionDescriptor;
            RouteParameters = routeParameters;

            Results = new List<ApiParameterDescription>();
        }

        public ControllerActionDescriptor ActionDescriptor { get; }

        public IModelMetadataProvider MetadataProvider { get; }

        public IList<ApiParameterDescription> Results { get; }

        public IReadOnlyList<TemplatePart> RouteParameters { get; }
    }
    /// <summary>
    /// Implements a provider of <see cref="ApiDescription"/> for actions represented
    /// by <see cref="ControllerActionDescriptor"/>.
    /// </summary>
    public class CustomApiDescriptionProvider : IApiDescriptionProvider
    {
        private readonly MvcOptions _mvcOptions;
        private readonly ApiResponseTypeProvider _responseTypeProvider;
        private readonly RouteOptions _routeOptions;
        private readonly IInlineConstraintResolver _constraintResolver;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        /// <summary>
        /// Creates a new instance of <see cref="DefaultApiDescriptionProvider"/>.
        /// </summary>
        /// <param name="optionsAccessor">The accessor for <see cref="MvcOptions"/>.</param>
        /// <param name="constraintResolver">The <see cref="IInlineConstraintResolver"/> used for resolving inline
        /// constraints.</param>
        /// <param name="modelMetadataProvider">The <see cref="IModelMetadataProvider"/>.</param>
        /// <param name="mapper">The <see cref="IActionResultTypeMapper"/>.</param>
        /// <param name="routeOptions">The accessor for <see cref="RouteOptions"/>.</param>
        /// <remarks>The <paramref name="mapper"/> parameter is currently ignored.</remarks>
        public CustomApiDescriptionProvider(
            IOptions<MvcOptions> optionsAccessor,
            IInlineConstraintResolver constraintResolver,
            IModelMetadataProvider modelMetadataProvider,
            IActionResultTypeMapper mapper,
            IOptions<RouteOptions> routeOptions)
        {
            _mvcOptions = optionsAccessor.Value;
            _constraintResolver = constraintResolver;
            _modelMetadataProvider = modelMetadataProvider;
            _responseTypeProvider = new ApiResponseTypeProvider(modelMetadataProvider, mapper, _mvcOptions);
            _routeOptions = routeOptions.Value;
        }

        /// <inheritdoc />
        public int Order => -1000;

        /// <inheritdoc />
        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            foreach (var action in context.Actions.OfType<ControllerActionDescriptor>())
            {
                if (action.AttributeRouteInfo != null && action.AttributeRouteInfo.SuppressPathMatching)
                {
                    continue;
                }
                //Microsoft.AspNetCore.Mvc 命名空间下的 无法获取到 Microsoft.AspNetCore.Mvc.ApiDescriptionActionData
                var extensionData = action.GetProperty<ApiDescriptionActionData>();
                if (extensionData==null)
                {
                    foreach (var item in action.Properties)
                    {
                        if(item.Value.GetType().FullName== "Microsoft.AspNetCore.Mvc.ApiDescriptionActionData")
                        {
                            extensionData = item.Value.ToJson().ToObject<ApiDescriptionActionData>();
                            break;
                        }
                    }
                }
                if (extensionData != null)
                {
                    var httpMethods = GetHttpMethods(action);
                    foreach (var httpMethod in httpMethods)
                    {
                        context.Results.Add(CreateApiDescription(action, httpMethod, extensionData.GroupName));
                    }
                }
            }
        }

        /// <inheritdoc />
        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
        }

        private ApiDescription CreateApiDescription(
            ControllerActionDescriptor action,
            string httpMethod,
            string groupName)
        {
            var parsedTemplate = ParseTemplate(action);

            var apiDescription = new ApiDescription()
            {
                ActionDescriptor = action,
                GroupName = groupName,
                HttpMethod = httpMethod,
                RelativePath = GetRelativePath(parsedTemplate),
            };

            var templateParameters = parsedTemplate?.Parameters?.ToList() ?? new List<TemplatePart>();

            var parameterContext = new ApiParameterContext(_modelMetadataProvider, action, templateParameters);

            foreach (var parameter in GetParameters(parameterContext))
            {
                apiDescription.ParameterDescriptions.Add(parameter);
            }

            var requestMetadataAttributes = GetRequestMetadataAttributes(action);

            var apiResponseTypes = _responseTypeProvider.GetApiResponseTypes(action);
            foreach (var apiResponseType in apiResponseTypes)
            {
                apiDescription.SupportedResponseTypes.Add(apiResponseType);
            }

            // It would be possible here to configure an action with multiple body parameters, in which case you
            // could end up with duplicate data.
            if (apiDescription.ParameterDescriptions.Count > 0)
            {
                var contentTypes = GetDeclaredContentTypes(requestMetadataAttributes);
                foreach (var parameter in apiDescription.ParameterDescriptions)
                {
                    if (parameter.Source == BindingSource.Body)
                    {
                        // For request body bound parameters, determine the content types supported
                        // by input formatters.
                        var requestFormats = GetSupportedFormats(contentTypes, parameter.Type);
                        foreach (var format in requestFormats)
                        {
                            apiDescription.SupportedRequestFormats.Add(format);
                        }
                    }
                    else if (parameter.Source == BindingSource.FormFile)
                    {
                        // Add all declared media types since FormFiles do not get processed by formatters.
                        foreach (var contentType in contentTypes)
                        {
                            apiDescription.SupportedRequestFormats.Add(new ApiRequestFormat
                            {
                                MediaType = contentType,
                            });
                        }
                    }
                }
            }

            return apiDescription;
        }

        private IList<ApiParameterDescription> GetParameters(ApiParameterContext context)
        {
            // First, get parameters from the model-binding/parameter-binding side of the world.
            if (context.ActionDescriptor.Parameters != null)
            {
                foreach (var actionParameter in context.ActionDescriptor.Parameters)
                {
                    var visitor = new PseudoModelBindingVisitor(context, actionParameter);

                    ModelMetadata metadata;
                    if (actionParameter is ControllerParameterDescriptor controllerParameterDescriptor &&
                        _modelMetadataProvider is ModelMetadataProvider provider)
                    {
                        // The default model metadata provider derives from ModelMetadataProvider
                        // and can therefore supply information about attributes applied to parameters.
                        metadata = provider.GetMetadataForParameter(controllerParameterDescriptor.ParameterInfo);
                    }
                    else
                    {
                        // For backward compatibility, if there's a custom model metadata provider that
                        // only implements the older IModelMetadataProvider interface, access the more
                        // limited metadata information it supplies. In this scenario, validation attributes
                        // are not supported on parameters.
                        metadata = _modelMetadataProvider.GetMetadataForType(actionParameter.ParameterType);
                    }

                    var bindingContext = ApiParameterDescriptionContext.GetContext(
                        metadata,
                        actionParameter.BindingInfo,
                        propertyName: actionParameter.Name);
                    visitor.WalkParameter(bindingContext);
                }
            }

            if (context.ActionDescriptor.BoundProperties != null)
            {
                foreach (var actionParameter in context.ActionDescriptor.BoundProperties)
                {
                    var visitor = new PseudoModelBindingVisitor(context, actionParameter);
                    var modelMetadata = context.MetadataProvider.GetMetadataForProperty(
                        containerType: context.ActionDescriptor.ControllerTypeInfo.AsType(),
                        propertyName: actionParameter.Name);

                    var bindingContext = ApiParameterDescriptionContext.GetContext(
                        modelMetadata,
                        actionParameter.BindingInfo,
                        propertyName: actionParameter.Name);

                    visitor.WalkParameter(bindingContext);
                }
            }


            for (var i = context.Results.Count - 1; i >= 0; i--)
            {
                // Remove any 'hidden' parameters. These are things that can't come from user input,
                // so they aren't worth showing.
                if (!context.Results[i].Source.IsFromRequest)
                {
                    context.Results.RemoveAt(i);
                }
            }

            // Next, we want to join up any route parameters with those discovered from the action's parameters.
            // This will result us in creating a parameter representation for each route parameter that does not
            // have a mapping parameter or bound property.
            ProcessRouteParameters(context);

            // Set IsRequired=true
            ProcessIsRequired(context);

            // Set DefaultValue
            ProcessParameterDefaultValue(context);

            return context.Results;
        }

        private void ProcessRouteParameters(ApiParameterContext context)
        {
            var routeParameters = new Dictionary<string, ApiParameterRouteInfo>(StringComparer.OrdinalIgnoreCase);
            foreach (var routeParameter in context.RouteParameters)
            {
                routeParameters.Add(routeParameter.Name, CreateRouteInfo(routeParameter));
            }

            foreach (var parameter in context.Results)
            {
                if (parameter.Source == BindingSource.Path ||
                    parameter.Source == BindingSource.ModelBinding ||
                    parameter.Source == BindingSource.Custom)
                {
                    if (routeParameters.TryGetValue(parameter.Name, out var routeInfo))
                    {
                        parameter.RouteInfo = routeInfo;
                        routeParameters.Remove(parameter.Name);

                        if (parameter.Source == BindingSource.ModelBinding &&
                            !parameter.RouteInfo.IsOptional)
                        {
                            // If we didn't see any information about the parameter, but we have
                            // a route parameter that matches, let's switch it to path.
                            parameter.Source = BindingSource.Path;
                        }
                    }
                }
            }

            // Lastly, create a parameter representation for each route parameter that did not find
            // a partner.
            foreach (var routeParameter in routeParameters)
            {
                context.Results.Add(new ApiParameterDescription()
                {
                    Name = routeParameter.Key,
                    RouteInfo = routeParameter.Value,
                    Source = BindingSource.Path,
                });
            }
        }

        internal static void ProcessIsRequired(ApiParameterContext context)
        {
            foreach (var parameter in context.Results)
            {
                if (parameter.Source == BindingSource.Body)
                {
                    parameter.IsRequired = true;
                }

                if (parameter.ModelMetadata != null && parameter.ModelMetadata.IsBindingRequired)
                {
                    parameter.IsRequired = true;
                }

                if (parameter.Source == BindingSource.Path && parameter.RouteInfo != null && !parameter.RouteInfo.IsOptional)
                {
                    parameter.IsRequired = true;
                }
            }
        }

        internal static void ProcessParameterDefaultValue(ApiParameterContext context)
        {
            foreach (var parameter in context.Results)
            {
                if (parameter.Source == BindingSource.Path)
                {
                    parameter.DefaultValue = parameter.RouteInfo?.DefaultValue;
                }
                else
                {
                    if (parameter.ParameterDescriptor is ControllerParameterDescriptor controllerParameter &&
                        ParameterDefaultValues.TryGetDeclaredParameterDefaultValue(controllerParameter.ParameterInfo, out var defaultValue))
                    {
                        parameter.DefaultValue = defaultValue;
                    }
                }
            }
        }

        private ApiParameterRouteInfo CreateRouteInfo(TemplatePart routeParameter)
        {
            var constraints = new List<IRouteConstraint>();
            if (routeParameter.InlineConstraints != null)
            {
                foreach (var constraint in routeParameter.InlineConstraints)
                {
                    constraints.Add(_constraintResolver.ResolveConstraint(constraint.Constraint));
                }
            }

            return new ApiParameterRouteInfo()
            {
                Constraints = constraints,
                DefaultValue = routeParameter.DefaultValue,
                IsOptional = routeParameter.IsOptional || routeParameter.DefaultValue != null,
            };
        }

        private IEnumerable<string> GetHttpMethods(ControllerActionDescriptor action)
        {
            if (action.ActionConstraints != null && action.ActionConstraints.Count > 0)
            {
                return action.ActionConstraints.OfType<HttpMethodActionConstraint>().SelectMany(c => c.HttpMethods);
            }
            else
            {
                return new string[] { null };
            }
        }

        private RouteTemplate ParseTemplate(ControllerActionDescriptor action)
        {
            if (action.AttributeRouteInfo?.Template != null)
            {
                return TemplateParser.Parse(action.AttributeRouteInfo.Template);
            }

            return null;
        }

        private string GetRelativePath(RouteTemplate parsedTemplate)
        {
            if (parsedTemplate == null)
            {
                return null;
            }

            var segments = new List<string>();

            foreach (var segment in parsedTemplate.Segments)
            {
                var currentSegment = string.Empty;
                foreach (var part in segment.Parts)
                {
                    if (part.IsLiteral)
                    {
                        currentSegment += _routeOptions.LowercaseUrls ?
                            part.Text.ToLowerInvariant() :
                            part.Text;
                    }
                    else if (part.IsParameter)
                    {
                        currentSegment += "{" + part.Name + "}";
                    }
                }

                segments.Add(currentSegment);
            }

            return string.Join("/", segments);
        }

        private IReadOnlyList<ApiRequestFormat> GetSupportedFormats(MediaTypeCollection contentTypes, Type type)
        {
            if (contentTypes.Count == 0)
            {
                contentTypes = new MediaTypeCollection
                {
                    (string)null,
                };
            }

            var results = new List<ApiRequestFormat>();
            foreach (var contentType in contentTypes)
            {
                foreach (var formatter in _mvcOptions.InputFormatters)
                {
                    if (formatter is IApiRequestFormatMetadataProvider requestFormatMetadataProvider)
                    {
                        var supportedTypes = requestFormatMetadataProvider.GetSupportedContentTypes(contentType, type);

                        if (supportedTypes != null)
                        {
                            foreach (var supportedType in supportedTypes)
                            {
                                results.Add(new ApiRequestFormat()
                                {
                                    Formatter = formatter,
                                    MediaType = supportedType,
                                });
                            }
                        }
                    }
                }
            }

            return results;
        }

        private static MediaTypeCollection GetDeclaredContentTypes(IApiRequestMetadataProvider[] requestMetadataAttributes)
        {
            // Walk through all 'filter' attributes in order, and allow each one to see or override
            // the results of the previous ones. This is similar to the execution path for content-negotiation.
            var contentTypes = new MediaTypeCollection();
            if (requestMetadataAttributes != null)
            {
                foreach (var metadataAttribute in requestMetadataAttributes)
                {
                    metadataAttribute.SetContentTypes(contentTypes);
                }
            }

            return contentTypes;
        }

        private IApiRequestMetadataProvider[] GetRequestMetadataAttributes(ControllerActionDescriptor action)
        {
            if (action.FilterDescriptors == null)
            {
                return null;
            }

            // This technique for enumerating filters will intentionally ignore any filter that is an IFilterFactory
            // while searching for a filter that implements IApiRequestMetadataProvider.
            //
            // The workaround for that is to implement the metadata interface on the IFilterFactory.
            return action.FilterDescriptors
                .Select(fd => fd.Filter)
                .OfType<IApiRequestMetadataProvider>()
                .ToArray();
        }

        private class ApiParameterDescriptionContext
        {
            public ModelMetadata ModelMetadata { get; set; }

            public string BinderModelName { get; set; }

            public BindingSource BindingSource { get; set; }

            public string PropertyName { get; set; }

            public static ApiParameterDescriptionContext GetContext(
                ModelMetadata metadata,
                BindingInfo bindingInfo,
                string propertyName)
            {
                // BindingMetadata can be null if the metadata represents properties.
                return new ApiParameterDescriptionContext
                {
                    ModelMetadata = metadata,
                    BinderModelName = bindingInfo?.BinderModelName,
                    BindingSource = bindingInfo?.BindingSource,
                    PropertyName = propertyName ?? metadata.Name,
                };
            }
        }

        private class PseudoModelBindingVisitor
        {
            public PseudoModelBindingVisitor(ApiParameterContext context, ParameterDescriptor parameter)
            {
                Context = context;
                Parameter = parameter;

                Visited = new HashSet<PropertyKey>(new PropertyKeyEqualityComparer());
            }

            public ApiParameterContext Context { get; }

            public ParameterDescriptor Parameter { get; }

            // Avoid infinite recursion by tracking properties.
            private HashSet<PropertyKey> Visited { get; }

            public void WalkParameter(ApiParameterDescriptionContext context)
            {
                // Attempt to find a binding source for the parameter
                //
                // The default is ModelBinding (aka all default value providers)
                var source = BindingSource.ModelBinding;
                /**
                 class a{
                b b; 
                }
                class b{
                 a a;
                }
                //这样就会异常 如何去掉相互引用了(怎么确定相互引用了)
                 */
                //Type type = context.ModelMetadata.ModelType;
                //Dictionary<string, Type> types = new Dictionary<string, Type>();
                //using (var iterator = type.GetRuntimeProperties().GetEnumerator())
                //{
                //    while (iterator.MoveNext())
                //    {
                //        types.Add(iterator.Current.Name, iterator.Current.PropertyType);//这样只获取第一层?如果绑定数据可能有很多层 
                //    }
                //}
                Visit(context, source, containerName: string.Empty);
            }
            /// <summary>
            /// 循环引用 递归遍历 导致 内存 不够 机器死机  重写 
            /// </summary>
            /// <param name="bindingContext"></param>
            /// <param name="ambientSource"></param>
            /// <param name="containerName"></param>
            private void Visit(
                ApiParameterDescriptionContext bindingContext,
                BindingSource ambientSource,
                string containerName,bool res=false,bool userFile=false)
            {
                var source = bindingContext.BindingSource;
              // 不能 删除 不然 swagger 参数没有了 
                if (source != null/* && source.IsGreedy*/)
                {
                    // We have a definite answer for this model. This is a greedy source like
                    // [FromBody] so there's no need to consider properties.
                    Context.Results.Add(CreateResult(bindingContext, source, containerName));
                    return;
                }

                var modelMetadata = bindingContext.ModelMetadata;
                // For any property which is a leaf node, we don't want to keep traversing:
                //
                //  1)  Collections - while it's possible to have binder attributes on the inside of a collection,
                //      it hardly seems useful, and would result in some very weird binding.
                //
                //  2)  Simple Types - These are generally part of the .net framework - primitives, or types which have a
                //      type converter from string.
                //
                //  3)  Types with no properties. Obviously nothing to explore there.
                //
                if (modelMetadata.IsEnumerableType ||
                    !modelMetadata.IsComplexType ||
                    modelMetadata.Properties.Count == 0)
                {
                    Context.Results.Add(CreateResult(bindingContext, source ?? ambientSource, containerName));
                    return;
                }

                // This will come from composite model binding - so investigate what's going on with each property.
                //
                // Ex:
                //
                //      public IActionResult PlaceOrder(OrderDTO order) {...}
                //
                //      public class OrderDTO
                //      {
                //          public int AccountId { get; set; }
                //
                //          [FromBody]
                //          public Order { get; set; }
                //      }
                //
                // This should result in two parameters:
                //
                //  AccountId - source: Any
                //  Order - source: Body
                //

                // We don't want to append the **parameter** name when building a model name.
                var newContainerName = containerName;
                if (modelMetadata.ContainerType != null)
                {
                    newContainerName = GetName(containerName, bindingContext);
                }

                var metadataProperties = modelMetadata.Properties;
                var metadataPropertiesCount = metadataProperties.Count;
                bool newRes = false;
                bool newUserFile = false;
                var types = new LinkedList<Type>();
                ReferenceHelper.DistinctTypes.Add(modelMetadata.BinderType, types);
               
                for (var i = 0; i < metadataPropertiesCount; i++)
                {
                    var propertyMetadata = metadataProperties[i];
                    //尼玛  怎么写死 了 怎么写活 了
                    //过滤 只需要 引用一次  子类再次引用分类  去除引用 防止递归一直循环
                    if(!propertyMetadata.ModelType.IsEnum&& !ReferenceHelper.Distincts.Contains(propertyMetadata.ModelType))
                    {
                        types.AddLast(propertyMetadata.ModelType);
                        if (ReferenceHelper.IsReferenceEvent.Invoke(modelMetadata.BinderType, propertyMetadata.ModelType))
                        {
                            continue;
                        }
                    }
                    //if (propertyMetadata.ModelType.FullName.StartsWith("SocialContact.Domain.Core"))
                    //{
                    //    if (propertyMetadata.ModelType.FullName.StartsWith("SocialContact.Domain.Core.UserFile")) newUserFile = true;
                    //    else newRes = true;
                    //}
                    //if (newUserFile && propertyMetadata.Name != "Src" || propertyMetadata.Name != "FileId")
                    //{
                    //    continue;
                    //}
                    //else
                    //{
                    //    if (res && propertyMetadata.Name != "Id")
                    //    {
                    //        continue;
                    //    }
                    //}
                  
                    var key = new PropertyKey(propertyMetadata, source);
                    var bindingInfo = BindingInfo.GetBindingInfo(Enumerable.Empty<object>(), propertyMetadata);

                    var propertyContext = ApiParameterDescriptionContext.GetContext(
                        propertyMetadata,
                        bindingInfo: bindingInfo,
                        propertyName: null);

                    if (Visited.Add(key))
                    {
                        //循环引用 递归遍历 导致 内存 不够 机器死机  重写 
                        Visit(propertyContext, source ?? ambientSource, newContainerName, newRes,newUserFile);
                        Visited.Remove(key);
                    }
                    else
                    {
                        // This is cycle, so just add a result rather than traversing.
                        Context.Results.Add(CreateResult(propertyContext, source ?? ambientSource, newContainerName));
                    }
                }
            }

            private ApiParameterDescription CreateResult(
                ApiParameterDescriptionContext bindingContext,
                BindingSource source,
                string containerName)
            {
                return new ApiParameterDescription()
                {
                    ModelMetadata = bindingContext.ModelMetadata,
                    Name = GetName(containerName, bindingContext),
                    Source = source,
                    Type = bindingContext.ModelMetadata.ModelType,
                    ParameterDescriptor = Parameter,
                };
            }

            private static string GetName(string containerName, ApiParameterDescriptionContext metadata)
            {
                if (!string.IsNullOrEmpty(metadata.BinderModelName))
                {
                    // Name was explicitly provided
                    return metadata.BinderModelName;
                }
                else
                {
                    return ModelNames.CreatePropertyModelName(containerName, metadata.PropertyName);
                }
            }

            private readonly struct PropertyKey
            {
                public readonly Type ContainerType;

                public readonly string PropertyName;

                public readonly BindingSource Source;

                public PropertyKey(ModelMetadata metadata, BindingSource source)
                {
                    ContainerType = metadata.ContainerType;
                    PropertyName = metadata.PropertyName;
                    Source = source;
                }
            }

            private class PropertyKeyEqualityComparer : IEqualityComparer<PropertyKey>
            {
                public bool Equals(PropertyKey x, PropertyKey y)
                {
                    return
                        x.ContainerType == y.ContainerType &&
                        x.PropertyName == y.PropertyName &&
                        x.Source == y.Source;
                }

                public int GetHashCode(PropertyKey obj)
                {
                    var hashCodeCombiner = HashCodeCombiner.Start();
                    hashCodeCombiner.Add(obj.ContainerType);
                    hashCodeCombiner.Add(obj.PropertyName);
                    hashCodeCombiner.Add(obj.Source);
                    return hashCodeCombiner.CombinedHash;
                }
            }
        }
    }
    internal struct HashCodeCombiner
    {
        private long _combinedHash64;

        public int CombinedHash
        {
            [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _combinedHash64.GetHashCode();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private HashCodeCombiner(long seed)
        {
            _combinedHash64 = seed;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(System.Collections.IEnumerable e)
        {
            if (e == null)
            {
                Add(0);
                return;
            }
            int num = 0;
            foreach (object item in e)
            {
                Add(item);
                num++;
            }
            Add(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator int(HashCodeCombiner self)
        {
            return self.CombinedHash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(int i)
        {
            _combinedHash64 = (((_combinedHash64 << 5) + _combinedHash64) ^ i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(string s)
        {
            int i = s?.GetHashCode() ?? 0;
            Add(i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(object o)
        {
            int i = o?.GetHashCode() ?? 0;
            Add(i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add<TValue>(TValue value, IEqualityComparer<TValue> comparer)
        {
            int i = (value != null) ? comparer.GetHashCode(value) : 0;
            Add(i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashCodeCombiner Start()
        {
            return new HashCodeCombiner(5381L);
        }
    }
    internal static class ParameterDefaultValues
    {
        public static object[] GetParameterDefaultValues(System.Reflection.MethodInfo methodInfo)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            var parameters = methodInfo.GetParameters();
            var values = new object[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                values[i] = GetParameterDefaultValue(parameters[i]);
            }

            return values;
        }

        private static object GetParameterDefaultValue(System.Reflection.ParameterInfo parameterInfo)
        {
            TryGetDeclaredParameterDefaultValue(parameterInfo, out var defaultValue);
            if (defaultValue == null && parameterInfo.ParameterType.IsValueType)
            {
                defaultValue = Activator.CreateInstance(parameterInfo.ParameterType);
            }

            return defaultValue;
        }

        public static bool TryGetDeclaredParameterDefaultValue(System.Reflection.ParameterInfo parameterInfo, out object defaultValue)
        {
            if (ParameterDefaultValue.TryGetDefaultValue(parameterInfo, out defaultValue))
            {
                return true;
            }

            var defaultValueAttribute = parameterInfo.GetCustomAttribute<System.ComponentModel.DefaultValueAttribute>(inherit: false);
            if (defaultValueAttribute != null)
            {
                defaultValue = defaultValueAttribute.Value;
                return true;
            }

            return false;
        }
    }

}
#endif