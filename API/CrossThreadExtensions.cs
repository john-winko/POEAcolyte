using System;
using System.Windows.Forms;

namespace PoeAcolyte.API
{
    /// <summary>
    ///     https://stackoverflow.com/questions/10775367/cross-thread-operation-not-valid-control-textbox1-accessed-from-a-thread-othe
    /// </summary>
    public static class CrossThreadExtensions
    {
        /// <summary>
        ///     Extension method to allow invoking actions to the UI thread
        /// </summary>
        /// <param name="target">Control</param>
        /// <param name="action">Action to be performed</param>
        public static void PerformSafely(this Control target, Action action)
        {
            if (target.InvokeRequired)
                target.Invoke(action);
            else
                action();
        }

        /// <summary>
        ///     Extension method to allow invoking actions to the UI thread
        /// </summary>
        /// <typeparam name="T1">First type parameter</typeparam>
        /// <param name="target">Control</param>
        /// <param name="action">Action to be performed</param>
        /// <param name="parameter">First parameter</param>
        public static void PerformSafely<T1>(this Control target, Action<T1> action, T1 parameter)
        {
            if (target.InvokeRequired)
                target.Invoke(action, parameter);
            else
                action(parameter);
        }

        /// <summary>
        ///     Extension method to allow invoking actions to the UI thread
        /// </summary>
        /// <typeparam name="T1">First type parameter</typeparam>
        /// <typeparam name="T2">Second type parameter</typeparam>
        /// <param name="target">Control</param>
        /// <param name="action">Action to be performed</param>
        /// <param name="p1">First parameter</param>
        /// <param name="p2">Second parameter</param>
        public static void PerformSafely<T1, T2>(this Control target, Action<T1, T2> action, T1 p1, T2 p2)
        {
            if (target.InvokeRequired)
                target.Invoke(action, p1, p2);
            else
                action(p1, p2);
        }
    }
}