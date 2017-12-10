﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace WebExperiment
{
    public class HttpMethodCollector<TContext, TResult, TController>
    {
        private readonly Factory<TController> _controllerFactory;
        private readonly Dictionary<string, QueryCollector<TContext, TResult, TController>> _queryCollectors = new Dictionary<string, QueryCollector<TContext, TResult, TController>>();

        public HttpMethodCollector(Factory<TController> controllerFactory)
        {
            _controllerFactory = controllerFactory;
        }

        public HttpMethodCollector<TContext, TResult, TController> Method(
            string method,
            QueryCollectorFiller<TContext, TResult, TController> filler)
        {
            _queryCollectors[method] = filler(new QueryCollector<TContext, TResult, TController>(_controllerFactory));
            return this;
        }

        public async Task<TResult> Handle(string httpMethod, NameValueCollection query, TContext context, CancellationToken ct)
        {
            if (_queryCollectors.TryGetValue(httpMethod, out var queryCollector))
                return await queryCollector.Handle(query, context, ct);
            throw new NotImplementedException();
        }
    }
    
    public class HttpMethodCollector<TContext, TResult, TController, TP1>
    {
        private readonly Factory<TController> _controllerFactory;
        private readonly Dictionary<string, QueryCollector<TContext, TResult, TController, TP1>> _queryCollectors = new Dictionary<string, QueryCollector<TContext, TResult, TController, TP1>>();

        public HttpMethodCollector(Factory<TController> controllerFactory)
        {
            _controllerFactory = controllerFactory;
        }
        
        public HttpMethodCollector<TContext, TResult, TController, TP1> Method(
            string method,
            QueryCollectorFiller<TContext, TResult, TController, TP1> filler)
        {
            _queryCollectors[method] = filler(new QueryCollector<TContext, TResult, TController, TP1>(_controllerFactory));
            return this;
        }
        
        public async Task<TResult> Handle(string httpMethod, NameValueCollection query, TContext context, TP1 p1, CancellationToken ct)
        {
            if (_queryCollectors.TryGetValue(httpMethod, out var queryCollector))
                return await queryCollector.Handle(query, context, p1, ct);
            throw new NotImplementedException();
        }
    }
    
    public class HttpMethodCollector<TContext, TResult, TController, TP1, TP2>
    {
        private readonly Factory<TController> _controllerFactory;
        private readonly Dictionary<string, QueryCollector<TContext, TResult, TController, TP1, TP2>> _queryCollectors = new Dictionary<string, QueryCollector<TContext, TResult, TController, TP1, TP2>>();

        public HttpMethodCollector(Factory<TController> controllerFactory)
        {
            _controllerFactory = controllerFactory;
        }
        
        public HttpMethodCollector<TContext, TResult, TController, TP1, TP2> Method(
            string method,
            QueryCollectorFiller<TContext, TResult, TController, TP1, TP2> filler)
        {
            _queryCollectors[method] = filler(new QueryCollector<TContext, TResult, TController, TP1, TP2>(_controllerFactory));
            return this;
        }
        
        public async Task<TResult> Handle(string httpMethod, NameValueCollection query, TContext context, TP1 p1, TP2 p2, CancellationToken ct)
        {
            if (_queryCollectors.TryGetValue(httpMethod, out var queryCollector))
                return await queryCollector.Handle(query, context, p1, p2, ct);
            throw new NotImplementedException();
        }
    }
}