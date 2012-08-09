
namespace LivetCommandSample.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interactivity;
    using System.Windows.Controls;

    public class CommandTestBehavior : Behavior<CheckBox>
    {
        private static EventHandler canExecuteChangedHandler;

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(CommandTestBehavior),
            new PropertyMetadata((ICommand)null,
            new PropertyChangedCallback(CommandChanged)));

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter",
            typeof(string),
            typeof(CommandTestBehavior),
            new PropertyMetadata((object)null));

        public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register(
            "CommandTarget",
            typeof(IInputElement),
            typeof(CommandTestBehavior),
            new PropertyMetadata((IInputElement)null));

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public string CommandParameter
        {
            get
            {
                return (string)GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        public IInputElement CommandTarget
        {
            get
            {
                return (IInputElement)GetValue(CommandTargetProperty);
            }
            set
            {
                SetValue(CommandTargetProperty, value);
            }
        }

        /// <summary>
        /// 要素にアタッチされたときの処理。
        /// </summary>
        /// <author>Takanori Shibuya.</author>
        protected override void OnAttached()
        {
            base.OnAttached();

            ((CheckBox)this.AssociatedObject).Checked += new RoutedEventHandler(CommandTestBehavior_Checked);
        }

        void CommandTestBehavior_Checked(object sender, RoutedEventArgs e)
        {
            if (this.Command != null)
            {
                ((ICommand)Command).Execute(this.CommandParameter);
            } 
        }

        /// <summary>
        /// 要素にデタッチされたときの処理。
        /// </summary>
        /// <author>Takanori Shibuya.</author>
        protected override void OnDetaching()
        {
            base.OnDetaching();
        }

        private static void CommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CommandTestBehavior mine = (CommandTestBehavior)sender;

            mine.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
        {
            if (oldCommand != null)
            {
                RemoveCommand(oldCommand, newCommand);
            }
            AddCommand(oldCommand, newCommand);
        }

        private void RemoveCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = CanExecuteChanged;
            oldCommand.CanExecuteChanged -= handler;
        }

        private void AddCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = new EventHandler(CanExecuteChanged);
            canExecuteChangedHandler = handler;
            if (newCommand != null)
            {
                newCommand.CanExecuteChanged += canExecuteChangedHandler;
            }
        }

        private void CanExecuteChanged(object sender, EventArgs e)
        {

            if (this.Command != null)
            {
                RoutedCommand command = this.Command as RoutedCommand;

                // If a RoutedCommand.
                if (command != null)
                {
                    if (command.CanExecute(CommandParameter, CommandTarget))
                    {
                        //this.IsEnabled = true;
                    }
                    else
                    {
                        //this.IsEnabled = false;
                    }
                }
                // If a not RoutedCommand.
                else
                {
                    if (Command.CanExecute(CommandParameter))
                    {
                        //this.IsEnabled = true;
                    }
                    else
                    {
                        //this.IsEnabled = false;
                    }
                }
            }
        }


    }
}
