﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SKZSoft.Twitter.TwitterJobs {
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
    internal class TwitterDataStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal TwitterDataStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SKZSoft.Twitter.TwitterJobs.TwitterDataStrings", typeof(TwitterDataStrings).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to running a batch.
        /// </summary>
        internal static string JobDescBatch {
            get {
                return ResourceManager.GetString("JobDescBatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to deleting a tweet.
        /// </summary>
        internal static string JobDescDestroy {
            get {
                return ResourceManager.GetString("JobDescDestroy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to deleting a tweet from the previous job.
        /// </summary>
        internal static string JobDescDestroyFromPrevious {
            get {
                return ResourceManager.GetString("JobDescDestroyFromPrevious", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to sending a DM.
        /// </summary>
        internal static string JobDescDMSend {
            get {
                return ResourceManager.GetString("JobDescDMSend", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to getting the access token.
        /// </summary>
        internal static string JobDescGetAccessToken {
            get {
                return ResourceManager.GetString("JobDescGetAccessToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to getting the Auth token.
        /// </summary>
        internal static string JobDescGetAuthToken {
            get {
                return ResourceManager.GetString("JobDescGetAuthToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to getting Twitter config.
        /// </summary>
        internal static string JobDescGetConfig {
            get {
                return ResourceManager.GetString("JobDescGetConfig", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to getting list of follower IDs..
        /// </summary>
        internal static string JobDescGetFollowersId {
            get {
                return ResourceManager.GetString("JobDescGetFollowersId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to getting a list of followers.
        /// </summary>
        internal static string JobDescGetFollowersList {
            get {
                return ResourceManager.GetString("JobDescGetFollowersList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to getting mentions.
        /// </summary>
        internal static string JobDescGetMentions {
            get {
                return ResourceManager.GetString("JobDescGetMentions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to getting tweet details.
        /// </summary>
        internal static string JobDescGetStatus {
            get {
                return ResourceManager.GetString("JobDescGetStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to getting tweets on a timeline.
        /// </summary>
        internal static string JobDescGetUserTimeline {
            get {
                return ResourceManager.GetString("JobDescGetUserTimeline", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to retweeting.
        /// </summary>
        internal static string JobDescRetweet {
            get {
                return ResourceManager.GetString("JobDescRetweet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to posting a new tweet.
        /// </summary>
        internal static string JobDescStatusUpdate {
            get {
                return ResourceManager.GetString("JobDescStatusUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uploading media.
        /// </summary>
        internal static string JobDescUploadMedia {
            get {
                return ResourceManager.GetString("JobDescUploadMedia", resourceCulture);
            }
        }
    }
}
