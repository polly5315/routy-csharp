﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Routy
{
    public class ValuedResource<TContext, TResult, TValue>
    {
        private readonly Func<string, TValue> _parser;
        private readonly AsyncHttpMethodCollectorHandler<TContext, TResult, TValue> _methodCollectorHandler;
        private readonly AsyncResourceCollectorHandler<TContext, TResult, TValue> _nestedAsyncResourceCollectorHandler;
        
        public ValuedResource(
            Func<string, TValue> parser,
            AsyncHttpMethodCollectorHandler<TContext, TResult, TValue> methodCollectorHandler,
            AsyncResourceCollectorHandler<TContext, TResult, TValue> nestedAsyncResourceCollectorHandler)
        {
            _parser = parser;
            _methodCollectorHandler = methodCollectorHandler;
            _nestedAsyncResourceCollectorHandler = nestedAsyncResourceCollectorHandler;
        }
        
        public async Task<TResult> Handle(string httpMethod, IEnumerable<string> uriSegments, NameValueCollection query, TContext context, CancellationToken ct)
        {
            if (!uriSegments.Any())
                throw new NotImplementedException("1");

            var head = uriSegments.First();
            var tail = uriSegments.Skip(1).ToArray();

            var value = _parser(head);

            return tail.Any()
                ? await _nestedAsyncResourceCollectorHandler(httpMethod, tail, query, context, value, ct)
                : await _methodCollectorHandler(httpMethod, query, context, value, ct);
        }
    }
}