﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Routy
{
    public class ResourceCollector<TContext, TResult, TController>
    {
        private readonly Func<TController> _controllerProvider;
        private readonly Dictionary<string, AsyncResourceCollectorHandler<TContext, TResult>> _namedResourceHandlers = new Dictionary<string, AsyncResourceCollectorHandler<TContext, TResult>>();
        private readonly List<AsyncResourceCollectorHandler<TContext, TResult>> _valuedResourceHandlers = new List<AsyncResourceCollectorHandler<TContext, TResult>>();

        public ResourceCollector(Func<TController> controllerProvider)
        {
            _controllerProvider = controllerProvider;
        }

        public ResourceCollector<TContext, TResult, TController> Named(string name,
            Mutator<HttpMethodCollector<TContext, TResult, TController>> httpMethodCollectorFiller = null,
            Mutator<ResourceCollector<TContext, TResult, TController>> nestedResourceCollectorFiller = null
            ) => Named(name, _controllerProvider, httpMethodCollectorFiller, nestedResourceCollectorFiller);
        
        public ResourceCollector<TContext, TResult, TController> Named<TNewController>(string name,
            Func<TNewController> controllerProvider,
            Mutator<HttpMethodCollector<TContext, TResult, TNewController>> httpMethodCollectorFiller = null,
            Mutator<ResourceCollector<TContext, TResult, TNewController>> nestedResourceCollectorFiller = null
            )
        {
            if (httpMethodCollectorFiller == null) httpMethodCollectorFiller = x => x;
            if (nestedResourceCollectorFiller == null) nestedResourceCollectorFiller = x => x;
            _namedResourceHandlers[name] = new NamedResource<TContext, TResult>(
                httpMethodCollectorFiller(new HttpMethodCollector<TContext, TResult, TNewController>(controllerProvider)).Handle,
                nestedResourceCollectorFiller(new ResourceCollector<TContext, TResult, TNewController>(controllerProvider)).HandleAsync).HandleAsync;
            return this;
        }
        
        public ResourceCollector<TContext, TResult, TController> Valued<TValue>(Func<string, TValue> parser,
            Mutator<HttpMethodCollector<TContext, TResult, TController, TValue>> httpMethodCollectorFiller = null,
            Mutator<ResourceCollector<TContext, TResult, TController, TValue>> nestedResourceCollectorFiller = null
            ) => Valued(parser, _controllerProvider, httpMethodCollectorFiller, nestedResourceCollectorFiller);

        public ResourceCollector<TContext, TResult, TController> Valued<TValue, TNewController>(Func<string, TValue> parser,
            Func<TNewController> controllerProvider,
            Mutator<HttpMethodCollector<TContext, TResult, TNewController, TValue>> httpMethodCollectorFiller = null,
            Mutator<ResourceCollector<TContext, TResult, TNewController, TValue>> nestedResourceCollectorFiller = null
            )
        {
            if (httpMethodCollectorFiller == null) httpMethodCollectorFiller = x => x;
            if (nestedResourceCollectorFiller == null) nestedResourceCollectorFiller = x => x;
            _valuedResourceHandlers.Add(new ValuedResource<TContext, TResult, TValue>(
                parser,
                httpMethodCollectorFiller(new HttpMethodCollector<TContext, TResult, TNewController, TValue>(controllerProvider)).Handle,
                nestedResourceCollectorFiller(new ResourceCollector<TContext, TResult, TNewController, TValue>(controllerProvider)).Handle).Handle);
            return this;
        }
        
        public async Task<TResult> HandleAsync(string httpMethod, ICollection<string> urlSegments, NameValueCollection queryParameters, TContext context, CancellationToken ct)
        {
            if (!urlSegments.Any())
                throw new NotImplementedException("15");
            
            var urlHead = urlSegments.First();

            if (_namedResourceHandlers.TryGetValue(urlHead, out var namedResourceHandler))
                return await namedResourceHandler(httpMethod, urlSegments, queryParameters, context, ct);

            foreach (var valuedResourceHandler in _valuedResourceHandlers)
                try
                {
                    return await valuedResourceHandler(httpMethod, urlSegments, queryParameters, context, ct);
                }
                catch
                {
                    
                }
            
            throw new NotImplementedException("16");
        }
    }

    public class ResourceCollector<TContext, TResult, TController, TP1>
    {
        private readonly Func<TController> _controllerProvider;
        private readonly Dictionary<string, AsyncResourceCollectorHandler<TContext, TResult, TP1>> _namedResourceHandlers = new Dictionary<string, AsyncResourceCollectorHandler<TContext, TResult, TP1>>();
        private readonly List<AsyncResourceCollectorHandler<TContext, TResult, TP1>> _valuedResourceHandlers = new List<AsyncResourceCollectorHandler<TContext, TResult, TP1>>();

        public ResourceCollector(Func<TController> controllerProvider)
        {
            _controllerProvider = controllerProvider;
        }
        
        public async Task<TResult> Handle(string httpMethod, ICollection<string> urlSegments, NameValueCollection queryParameters, TContext context, TP1 p1, CancellationToken ct)
        {
            if (!urlSegments.Any())
                throw new NotImplementedException("17");
            
            var urlHead = urlSegments.First();

            if (_namedResourceHandlers.TryGetValue(urlHead, out var namedResourceHandler))
                return await namedResourceHandler(httpMethod, urlSegments, queryParameters, context, p1, ct);

            foreach (var valuedResourceHandler in _valuedResourceHandlers)
                try
                {
                    return await valuedResourceHandler(httpMethod, urlSegments, queryParameters, context, p1, ct);
                }
                catch
                {
                    
                }
            
            throw new NotImplementedException("18");
        }
    }
    
    public class ResourceCollector<TContext, TResult, TController, TP1, TP2>
    {
        private readonly Func<TController> _controllerProvider;
        private readonly Dictionary<string, AsyncResourceCollectorHandler<TContext, TResult, TP1, TP2>> _namedResourceHandlers = new Dictionary<string, AsyncResourceCollectorHandler<TContext, TResult, TP1, TP2>>();
        private readonly List<AsyncResourceCollectorHandler<TContext, TResult, TP1, TP2>> _valuedResourceHandlers = new List<AsyncResourceCollectorHandler<TContext, TResult, TP1, TP2>>();

        public ResourceCollector(Func<TController> controllerProvider)
        {
            _controllerProvider = controllerProvider;
        }
        
        public async Task<TResult> Handle(string httpMethod, ICollection<string> urlSegments, NameValueCollection queryParameters, TContext context, TP1 p1, TP2 p2, CancellationToken ct)
        {
            if (!urlSegments.Any())
                throw new NotImplementedException("19");
            
            var urlHead = urlSegments.First();

            if (_namedResourceHandlers.TryGetValue(urlHead, out var namedResourceHandler))
                return await namedResourceHandler(httpMethod, urlSegments, queryParameters, context, p1, p2, ct);

            foreach (var valuedResourceHandler in _valuedResourceHandlers)
                try
                {
                    return await valuedResourceHandler(httpMethod, urlSegments, queryParameters, context, p1, p2, ct);
                }
                catch
                {
                    
                }
            
            throw new NotImplementedException("20");
        }
    }
}
