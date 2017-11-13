using System;
using System.Reflection;
using System.Threading;

namespace TXDLL.Tools
{
    /// <summary>
    /// 线程的一些辅助方法
    /// </summary>
    public class ThreadTools
    {
        public static void StartVoidAsynThread(Thread thread, object instance,string methodName, object[] param)
        {
            Type type = instance.GetType();
            MethodInfo method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            try
            {
                thread = new Thread(new ThreadStart ( delegate { method.Invoke(instance, param); } ));
                thread.Start();
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
        public static void StartVoidAsynThread(object instance, string methodName, object[] param)
        {
            Thread thread = null;
            StartVoidAsynThread(thread, instance,methodName, param);
        }
    }
}
