using System;
using System.Windows.Input;

namespace Utility.Wpf
{
    /// <summary>
    /// No WPF project is complete without it's own version of this.
    /// </summary>
    public class CommandImplementation : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// 命令 默认 实现
        /// </summary>
        /// <param name="execute">命令方法</param>
        public CommandImplementation(Action<object> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// 命令 默认 实现
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public CommandImplementation(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute ?? (x => true);
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// 挂起 命令
        /// </summary>
        public void Refresh()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
