﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PoeAcolyte {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    public sealed partial class GameClient : global::System.Configuration.ApplicationSettingsBase {
        
        private static GameClient defaultInstance = ((GameClient)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new GameClient())));
        
        public static GameClient Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int TradeUILeft {
            get {
                return ((int)(this["TradeUILeft"]));
            }
            set {
                this["TradeUILeft"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public int TradeUITop {
            get {
                return ((int)(this["TradeUITop"]));
            }
            set {
                this["TradeUITop"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("500, 500")]
        public global::System.Drawing.Size TradeUISize {
            get {
                return ((global::System.Drawing.Size)(this["TradeUISize"]));
            }
            set {
                this["TradeUISize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int StashUILeft {
            get {
                return ((int)(this["StashUILeft"]));
            }
            set {
                this["StashUILeft"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int StashUITop {
            get {
                return ((int)(this["StashUITop"]));
            }
            set {
                this["StashUITop"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("500, 500")]
        public global::System.Drawing.Size StashUISize {
            get {
                return ((global::System.Drawing.Size)(this["StashUISize"]));
            }
            set {
                this["StashUISize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files (x86)\\Grinding Gear Games\\Path of Exile\\logs\\Client.txt")]
        public string ClientLogPath {
            get {
                return ((string)(this["ClientLogPath"]));
            }
            set {
                this["ClientLogPath"] = value;
            }
        }
    }
}
