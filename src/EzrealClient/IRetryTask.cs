﻿using System;
using System.Threading.Tasks;

namespace EzrealClient
{
    /// <summary>
    /// 定义重试的行为
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IRetryTask<TResult> : Task<TResult>
    {
        /// <summary>
        /// 当捕获到异常时进行Retry
        /// </summary>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <returns></returns>
        IRetryTask<TResult> WhenCatch<TException>() where TException : Exception;

        /// <summary>
        /// 当捕获到异常时进行Retry
        /// </summary>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <param name="handler">捕获到指定异常时</param>
        /// <returns></returns>
        IRetryTask<TResult> WhenCatch<TException>(Action<TException> handler) where TException : Exception;

        /// <summary>
        /// 当捕获到异常时进行Retry
        /// </summary>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <param name="predicate">返回true才Retry</param>
        /// <returns></returns>
        IRetryTask<TResult> WhenCatch<TException>(Func<TException, bool> predicate) where TException : Exception;

        /// <summary>
        /// 当捕获到异常时进行Retry
        /// </summary>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <param name="handler">捕获到指定异常时</param>
        /// <returns></returns>
        IRetryTask<TResult> WhenCatchAsync<TException>(Func<TException, Task> handler) where TException : Exception;

        /// <summary>
        /// 当捕获到异常时进行Retry
        /// </summary>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <param name="predicate">返回true才Retry</param>
        /// <returns></returns>
        IRetryTask<TResult> WhenCatchAsync<TException>(Func<TException, System.Threading.Tasks.Task<bool>> predicate) where TException : Exception;

        /// <summary>
        /// 当结果符合条件时进行Retry
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        IRetryTask<TResult> WhenResult(Func<TResult, bool> predicate);

        /// <summary>
        /// 当结果符合条件时进行Retry
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        IRetryTask<TResult> WhenResultAsync(Func<TResult, System.Threading.Tasks.Task<bool>> predicate);
    }
}
