﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopManager.Resources.Locale {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ShopManager.Resources.Locale.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A simple manager for a physical shop/store. Handles product information and sales history..
        /// </summary>
        public static string ABOUT_APP_DESCRIPTION {
            get {
                return ResourceManager.GetString("ABOUT_APP_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shop manager.
        /// </summary>
        public static string ABOUT_APP_TITLE {
            get {
                return ResourceManager.GetString("ABOUT_APP_TITLE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Any unsaved changes will be lost if you discard the changes..
        /// </summary>
        public static string DISCARD_CHANGES_TEXT {
            get {
                return ResourceManager.GetString("DISCARD_CHANGES_TEXT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Discard all unsaved changes?.
        /// </summary>
        public static string DISCARD_CHANGES_TITLE {
            get {
                return ResourceManager.GetString("DISCARD_CHANGES_TITLE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fatal error. Any unsaved changes are lost..
        /// </summary>
        public static string FATAL_ERROR_TEXT {
            get {
                return ResourceManager.GetString("FATAL_ERROR_TEXT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fatal error!.
        /// </summary>
        public static string FATAL_ERROR_TITLE {
            get {
                return ResourceManager.GetString("FATAL_ERROR_TITLE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are unsaved changes. Do you want to save your changes?.
        /// </summary>
        public static string SAVE_CHANGES_QUESTION_TEXT {
            get {
                return ResourceManager.GetString("SAVE_CHANGES_QUESTION_TEXT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Save changes?.
        /// </summary>
        public static string SAVE_CHANGES_QUESTION_TITLE {
            get {
                return ResourceManager.GetString("SAVE_CHANGES_QUESTION_TITLE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unexpected error occurred..
        /// </summary>
        public static string UNEXPECTED_ERROR_TEXT {
            get {
                return ResourceManager.GetString("UNEXPECTED_ERROR_TEXT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected error.
        /// </summary>
        public static string UNEXPECTED_ERROR_TITLE {
            get {
                return ResourceManager.GetString("UNEXPECTED_ERROR_TITLE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Would you like to update the product stock (Quantity) as well to reflect the sales changes?.
        /// </summary>
        public static string UPDATE_STOCK_QUESTION_TEXT {
            get {
                return ResourceManager.GetString("UPDATE_STOCK_QUESTION_TEXT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Update stock as well?.
        /// </summary>
        public static string UPDATE_STOCK_QUESTION_TITLE {
            get {
                return ResourceManager.GetString("UPDATE_STOCK_QUESTION_TITLE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The given quantity is larger than the selected product&apos;s stock (Quantity). This will result in a negative stock, which is nonsensical. Make sure this is the right quantity. Do you still want to update the stock?.
        /// </summary>
        public static string WARN_SALE_OUT_OF_STOCK_TEXT {
            get {
                return ResourceManager.GetString("WARN_SALE_OUT_OF_STOCK_TEXT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Quantity larger than stock!.
        /// </summary>
        public static string WARN_SALE_OUT_OF_STOCK_TITLE {
            get {
                return ResourceManager.GetString("WARN_SALE_OUT_OF_STOCK_TITLE", resourceCulture);
            }
        }
    }
}
