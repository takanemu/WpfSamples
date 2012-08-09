using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using InvokeCommandActionSample.Models;
using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.Messaging.Windows;

namespace InvokeCommandActionSample.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        /*コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ
         *  
         * を使用してください。
         */

        /*ViewModelからViewを操作したい場合は、
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信してください。
         */

        /*
         * UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         */

        /*
         * Modelからの変更通知などの各種イベントをそのままViewModelで購読する事はメモリリークの
         * 原因となりやすく推奨できません。ViewModelHelperの各静的メソッドの利用を検討してください。
         */

        /// <summary>
        /// Loadedコマンド
        /// </summary>
        private ListenerCommand<string> loadedCommand;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
            : base()
        {
        }

        /// <summary>
        /// Loadedコマンド
        /// </summary>
        public ListenerCommand<string> LoadedCommand
        {
            get
            {
                if (this.loadedCommand == null)
                {
                    this.loadedCommand = new ListenerCommand<string>(this.Loaded);
                }
                return this.loadedCommand;
            }
        }

        /// <summary>
        /// Loadedコマンド
        /// </summary>
        /// <param name="parameter">パラメーター</param>
        /// <author>Takanori Shibuya.</author>
        public void Loaded(string parameter)
        {
            Debug.WriteLine("parameter = " + parameter);
        }
    }
}
