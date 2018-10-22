﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace Routy
{
    public class HttpMethodCollector<TContext, TResult, TController>
    {
        private readonly Func<TController> _controllerProvider;
        private readonly Dictionary<string, QueryCollector<TContext, TResult, TController>> _queryCollectors = new Dictionary<string, QueryCollector<TContext, TResult, TController>>();

        public HttpMethodCollector(Func<TController> controllerProvider)
        {
            _controllerProvider = controllerProvider;
        }

        public HttpMethodCollector<TContext, TResult, TController> Method(
            string method,
            Mutator<QueryCollector<TContext, TResult, TController>> filler
            )
        {
            _queryCollectors[method] = filler(new QueryCollector<TContext, TResult, TController>(_controllerProvider));
            return this;
        }

        public async Task<TResult> Handle(string httpMethod, NameValueCollection query, TContext context, CancellationToken ct)
        {
            if (_queryCollectors.TryGetValue(httpMethod.ToLower(), out var queryCollector))
                return await queryCollector.Handle(query, context, ct);
            throw new NotImplementedException("22");
        }
    }
    
    public class HttpMethodCollector<TContext, TResult, TController, TP1>
    {
        private readonly Func<TController> _controllerProvider;
        private readonly Dictionary<string, QueryCollector<TContext, TResult, TController, TP1>> _queryCollectors = new Dictionary<string, QueryCollector<TContext, TResult, TController, TP1>>();

        public HttpMethodCollector(Func<TController> controllerProvider)
        {
            _controllerProvider = controllerProvider;
        }
        
        public HttpMethodCollector<TContext, TResult, TController, TP1> Method(
            string method,
            Mutator<QueryCollector<TContext, TResult, TController, TP1>> filler)
        {
            _queryCollectors[method] = filler(new QueryCollector<TContext, TResult, TController, TP1>(_controllerProvider));
            return this;
        }
        
        public async Task<TResult> Handle(string httpMethod, NameValueCollection query, TContext context, TP1 p1, CancellationToken ct)
        {
            if (_queryCollectors.TryGetValue(httpMethod.ToLower(), out var queryCollector))
                return await queryCollector.Handle(query, context, p1, ct);
            throw new NotImplementedException("23");
        }
    }
    
    public class HttpMethodCollector<TContext, TResult, TController, TP1, TP2>
    {
        private readonly Func<TController> _controllerProvider;
        private readonly Dictionary<string, QueryCollector<TContext, TResult, TController, TP1, TP2>> _queryCollectors = new Dictionary<string, QueryCollector<TContext, TResult, TController, TP1, TP2>>();

        public HttpMethodCollector(Func<TController> controllerProvider)
        {
            _controllerProvider = controllerProvider;
        }
        
        public HttpMethodCollector<TContext, TResult, TController, TP1, TP2> Method(
            string method,
            Mutator<QueryCollector<TContext, TResult, TController, TP1, TP2>> filler)
        {
            _queryCollectors[method] = filler(new QueryCollector<TContext, TResult, TController, TP1, TP2>(_controllerProvider));
            return this;
        }
        
        public async Task<TResult> Handle(string httpMethod, NameValueCollection query, TContext context, TP1 p1, TP2 p2, CancellationToken ct)
        {
            if (_queryCollectors.TryGetValue(httpMethod.ToLower(), out var queryCollector))
                return await queryCollector.Handle(query, context, p1, p2, ct);
            throw new NotImplementedException("24");
        }
    }
}