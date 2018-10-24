﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Routy
{
    public class NamedResource<TContext, TResult>
    {
        private readonly AsyncHttpMethodCollectorHandler<TContext, TResult> _methodCollectorHandler;
        private readonly AsyncResourceCollectorHandler<TContext, TResult> _nestedAsyncResourceCollectorHandler;
        
        public NamedResource(
            AsyncHttpMethodCollectorHandler<TContext, TResult> methodCollectorHandler,
            AsyncResourceCollectorHandler<TContext, TResult> nestedAsyncResourceCollectorHandler)
        {
            _methodCollectorHandler = methodCollectorHandler;
            _nestedAsyncResourceCollectorHandler = nestedAsyncResourceCollectorHandler;
        }
        
        public async Task<TResult> HandleAsync(string httpMethod, IEnumerable<string> uriSegments, NameValueCollection query, TContext context, CancellationToken ct)
        {
            if (!uriSegments.Any())
                throw new NotImplementedException("6");

            var tail = uriSegments.Skip(1).ToArray();

            return tail.Any()
                ? await _nestedAsyncResourceCollectorHandler(httpMethod, tail, query, context, ct)
                : await _methodCollectorHandler(httpMethod, query, context, ct);
        }
    }
}