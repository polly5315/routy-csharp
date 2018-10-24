﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace Routy
{
    // RequestHandler
    public delegate Task<TResult> AsyncRequestHandler<in TContext, TResult>(string httpMethod, Uri uri, TContext context, CancellationToken ct);

    // Functional value extractor
    public delegate T ValueExtractor<in TContext, out T>(TContext context, NameValueCollection parameters, CancellationToken ct);

    // Functional mutator
    public delegate T Mutator<T>(T t);

    #region Parameter collector fillers
    
    public delegate ParameterCollector<TContext, TResult, TController> ParameterCollectorFiller<TContext, TResult, TController>(ParameterCollector<TContext, TResult, TController> parameterCollector);
    public delegate ParameterCollector<TContext, TQ1, TResult, TController> ParameterCollectorFiller<TContext, TQ1, TResult, TController>(ParameterCollector<TContext, TResult, TController> parameterCollector);
    public delegate ParameterCollector<TContext, TQ1, TQ2, TResult, TController> ParameterCollectorFiller<TContext, TQ1, TQ2, TResult, TController>(ParameterCollector<TContext, TResult, TController> parameterCollector);
    public delegate ParameterCollector<TContext, TQ1, TQ2, TQ3, TResult, TController> ParameterCollectorFiller<TContext, TQ1, TQ2, TQ3, TResult, TController>(ParameterCollector<TContext, TResult, TController> parameterCollector);
    
    #endregion

    #region Middle handlers

    public delegate Task<TResult> AsyncResourceCollectorHandler<in TContext, TResult>(string httpMethod, ICollection<string> urlSegments, NameValueCollection queryParameters, TContext context, CancellationToken ct);
    public delegate Task<TResult> AsyncResourceCollectorHandler<in TContext, TResult, in TP1>(string httpMethod, ICollection<string> urlSegments, NameValueCollection queryParameters, TContext context, TP1 p1, CancellationToken ct);
    public delegate Task<TResult> AsyncResourceCollectorHandler<in TContext, TResult, in TP1, in TP2>(string httpMethod, ICollection<string> urlSegments, NameValueCollection queryParameters, TContext context, TP1 p1, TP2 tp2, CancellationToken ct);
    
    public delegate Task<TResult> AsyncHttpMethodCollectorHandler<in TContext, TResult>(string httpMethod, NameValueCollection query, TContext context, CancellationToken ct);
    public delegate Task<TResult> AsyncHttpMethodCollectorHandler<in TContext, TResult, in TP1>(string httpMethod, NameValueCollection query, TContext context, TP1 p1, CancellationToken ct);
    public delegate Task<TResult> AsyncHttpMethodCollectorHandler<in TContext, TResult, in TP1, in TP2>(string httpMethod, NameValueCollection query, TContext context, TP1 p1, TP2 p2, CancellationToken ct);
    
    public delegate Task<TResult> AsyncParameterCollectorHandler<in TContext, TResult>(NameValueCollection query, TContext context, CancellationToken ct);
    public delegate Task<TResult> AsyncParameterCollectorHandler<in TContext, TResult, in TP1>(NameValueCollection query, TContext context, TP1 p1, CancellationToken ct);
    public delegate Task<TResult> AsyncParameterCollectorHandler<in TContext, TResult, in TP1, in TP2>(NameValueCollection query, TContext context, TP1 p1, TP2 p2, CancellationToken ct);
    public delegate Task<TResult> AsyncParameterCollectorHandler<in TContext, TResult, in TP1, in TP2, in TP3>(NameValueCollection query, TContext context, TP1 p1, TP2 p2, TP3 p3, CancellationToken ct);
    public delegate Task<TResult> AsyncParameterCollectorHandler<in TContext, TResult, in TP1, in TP2, in TP3, in TP4, in TP5, in TP6, in TP7, in TP8, in TP9, in TP10, in TP11, in TP12, in TP13, in TP14, in TP15>(NameValueCollection query, TContext context, TP1 p1, TP2 p2, TP3 p3, TP4 p4, TP5 p5, TP6 p6, TP7 p7, TP8 p8, TP9 p9, TP10 p10, TP11 p11, TP12 p12, TP13 p13, TP14 p14, TP15 p15, CancellationToken ct);

    #endregion

    #region Endpoint handlers

    public delegate TResult Handler<out TResult>();
    public delegate TResult Handler<in T1, out TResult>(T1 v1);
    public delegate TResult Handler<in T1, in T2, out TResult>(T1 v1, T2 v2);
    public delegate TResult Handler<in T1, in T2, in T3, out TResult>(T1 v1, T2 v2, T3 v3);
    public delegate TResult Handler<in T1, in T2, in T3, in T4, out TResult>(T1 v1, T2 v2, T3 v3, T4 v4);
    public delegate TResult Handler<in T1, in T2, in T3, in T4, in T5, out TResult>(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5);
    public delegate TResult Handler<in T1, in T2, in T3, in T4, in T5, in T6, out TResult>(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6);
    
    public delegate Task<TResult> AsyncHandler<TResult>();
    public delegate Task<TResult> AsyncHandler<in T1, TResult>(T1 v1);
    public delegate Task<TResult> AsyncHandler<in T1, in T2, TResult>(T1 v1, T2 v2);
    public delegate Task<TResult> AsyncHandler<in T1, in T2, in T3, TResult>(T1 v1, T2 v2, T3 v3);
    public delegate Task<TResult> AsyncHandler<in T1, in T2, in T3, in T4, TResult>(T1 v1, T2 v2, T3 v3, T4 v4);
    public delegate Task<TResult> AsyncHandler<in T1, in T2, in T3, in T4, in T5, TResult>(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5);
    public delegate Task<TResult> AsyncHandler<in T1, in T2, in T3, in T4, in T5, in T6, TResult>(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6);
    public delegate Task<TResult> AsyncHandler<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, TResult>(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, T14 v14, T15 v15, T16 v16);

    #endregion
    
    #region Endpoint handler factories
    
    public delegate Handler<TResult> HandlerProvider<in TController, out TResult>(Func<TController> controllerProvider);
    public delegate Handler<T1, TResult> HandlerProvider<in TController, in T1, out TResult>(Func<TController> controllerProvider);
    public delegate Handler<T1, T2, TResult> HandlerProvider<in TController, in T1, in T2, out TResult>(Func<TController> controllerProvider);
    public delegate Handler<T1, T2, T3, TResult> HandlerProvider<in TController, in T1, in T2, in T3, out TResult>(Func<TController> controllerProvider);
    public delegate Handler<T1, T2, T3, T4, TResult> HandlerProvider<in TController, in T1, in T2, in T3, in T4, out TResult>(Func<TController> controllerProvider);
    public delegate Handler<T1, T2, T3, T4, T5, TResult> HandlerProvider<in TController, in T1, in T2, in T3, in T4, in T5, out TResult>(Func<TController> controllerProvider);
    public delegate Handler<T1, T2, T3, T4, T5, T6, TResult> HandlerProvider<in TController, in T1, in T2, in T3, in T4, in T5, in T6, out TResult>(Func<TController> controllerProvider);
    
    public delegate AsyncHandler<TResult> AsyncHandlerProvider<in TController, TResult>(Func<TController> controllerProvider);
    public delegate AsyncHandler<T1, TResult> AsyncHandlerProvider<in TController, in T1, TResult>(Func<TController> controllerProvider);
    public delegate AsyncHandler<T1, T2, TResult> AsyncHandlerProvider<in TController, in T1, in T2, TResult>(Func<TController> controllerProvider);
    public delegate AsyncHandler<T1, T2, T3, TResult> AsyncHandlerProvider<in TController, in T1, in T2, in T3, TResult>(Func<TController> controllerProvider);
    public delegate AsyncHandler<T1, T2, T3, T4, TResult> AsyncHandlerProvider<in TController, in T1, in T2, in T3, in T4, TResult>(Func<TController> controllerProvider);
    public delegate AsyncHandler<T1, T2, T3, T4, T5, TResult> AsyncHandlerProvider<in TController, in T1, in T2, in T3, in T4, in T5, TResult>(Func<TController> controllerProvider);
    public delegate AsyncHandler<T1, T2, T3, T4, T5, T6, TResult> AsyncHandlerProvider<in TController, in T1, in T2, in T3, in T4, in T5, in T6, TResult>(Func<TController> controllerProvider);
    public delegate AsyncHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> AsyncHandlerProvider<in TController, in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, TResult>(Func<TController> controllerProvider);
    
    #endregion
}
