﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EddiMaterialMonitor.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class MaterialMonitor {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MaterialMonitor() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EddiMaterialMonitor.Properties.MaterialMonitor", typeof(MaterialMonitor).Assembly);
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
        ///   Looks up a localized string similar to Track the amount of materials and generate events when limits are reached..
        /// </summary>
        public static string desc {
            get {
                return ResourceManager.GetString("desc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Desired.
        /// </summary>
        public static string desired_header {
            get {
                return ResourceManager.GetString("desired_header", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Inventory.
        /// </summary>
        public static string inventory_header {
            get {
                return ResourceManager.GetString("inventory_header", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Maximum.
        /// </summary>
        public static string max_header {
            get {
                return ResourceManager.GetString("max_header", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Minimum.
        /// </summary>
        public static string min_header {
            get {
                return ResourceManager.GetString("min_header", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Material monitor.
        /// </summary>
        public static string name {
            get {
                return ResourceManager.GetString("name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name.
        /// </summary>
        public static string name_header {
            get {
                return ResourceManager.GetString("name_header", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Below is a list of all known materials. You can set values for the minimum, desired and maximum levels of each and EDDI will generate events when you move beyond the levels..
        /// </summary>
        public static string p1 {
            get {
                return ResourceManager.GetString("p1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Note that if your inventory is incorrect you will need to start or restart Elite and this information will be updated..
        /// </summary>
        public static string p2 {
            get {
                return ResourceManager.GetString("p2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type.
        /// </summary>
        public static string type_header {
            get {
                return ResourceManager.GetString("type_header", resourceCulture);
            }
        }
    }
}