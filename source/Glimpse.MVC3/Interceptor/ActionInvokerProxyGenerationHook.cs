﻿using System;
using System.Reflection;
using System.Web;
using Castle.DynamicProxy;
using Glimpse.Core.Extensions;
using Glimpse.Mvc3.Warning;

namespace Glimpse.Mvc3.Interceptor
{
    internal class ActionInvokerProxyGenerationHook : IProxyGenerationHook
    {
        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            var methodName = methodInfo.Name;

            var result =  (methodName.Equals("GetFilters") ||
                    methodName.Equals("InvokeActionResult") ||
                    methodName.Equals("InvokeActionMethod"));

            return result;
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
            var warnings = new HttpContextWrapper(HttpContext.Current).GetWarnings();//Hack
            warnings.Add(new NonProxyableMemberWarning(type, memberInfo));
        }

        public void NonVirtualMemberNotification(Type type, MemberInfo memberInfo)
        {
            var warnings = new HttpContextWrapper(HttpContext.Current).GetWarnings();//Hack
            warnings.Add(new NonVirtualMemberWarning(type, memberInfo));
        }

        public void MethodsInspected()
        {
        }
    }
}