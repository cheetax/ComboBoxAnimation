﻿#pragma checksum "D:\users\user\documents\github\ComboBoxAnimation\ComboBoxAnimation\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7725D4A28FE1104C1EC8C52FCFFD16E0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComboBoxAnimation
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.rect = (global::Windows.UI.Xaml.Shapes.Rectangle)(target);
                }
                break;
            case 2:
                {
                    global::ComboBoxAnimation.Controls.ComboBoxUser element2 = (global::ComboBoxAnimation.Controls.ComboBoxUser)(target);
                    #line 32 "..\..\..\MainPage.xaml"
                    ((global::ComboBoxAnimation.Controls.ComboBoxUser)element2).PressedAddButton += this.ComboBoxUser_PressedAddButton;
                    #line 32 "..\..\..\MainPage.xaml"
                    ((global::ComboBoxAnimation.Controls.ComboBoxUser)element2).PressedSettingsButton += this.ComboBoxUser_PressedSettingsButton;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

