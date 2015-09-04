using System;
using Android.Preferences;
using Android.App;
using Java.Lang.Reflect;
using Java.Interop;
using Java.Lang;
using Android.Content;
using Android.Runtime;

using Android.OS;

namespace SupportPreference
{
	public class PreferenceManagerCompat:Java.Lang.Object
	{

		private static readonly string TAG = "PreferenceManagerCompat";







	




		/**
     * Interface definition for a callback to be invoked when a
     * {@link Preference} in the hierarchy rooted at this {@link PreferenceScreen} is
     * clicked.
     */
		public interface IOnPreferenceTreeClickListener
		{
			

			

			

			/**
         * Called when a preference in the tree rooted at this
         * {@link PreferenceScreen} has been clicked.
         * 
         * @param preferenceScreen The {@link PreferenceScreen} that the
         *        preference is located in.
         * @param preference The preference that was clicked.
         * @return Whether the click was handled.
         */
			bool OnPreferenceTreeClick (PreferenceScreen preferenceScreen, Preference preference);
		}

		public	static PreferenceManager NewInstance (Activity activity, int firstRequestCode)
		{
			try {
				Class cl = Java.Lang.Class.FromType (typeof(PreferenceManager));
				Constructor c = cl.GetConstructor (Class.FromType (typeof(Activity)), Java.Lang.Integer.Type);
				c.Accessible = true;
				return (PreferenceManager)c.NewInstance (activity, firstRequestCode);
			} catch (System.Exception e) {
				Console.WriteLine (TAG + " Couldn't call constructor PreferenceManager by reflection " + e.Message);
			}
			return null;
		}

		/**
     * Sets the owning preference fragment
     */
		public	static void SetFragment (PreferenceManager manager, SupportPreferenceFragment fragment)
		{
			// stub
		}

		/**
     * Sets the callback to be invoked when a {@link Preference} in the
     * hierarchy rooted at this {@link PreferenceManager} is clicked.
     * 
     * @param listener The callback to be invoked.
     */
		public	static void SetOnPreferenceTreeClickListener (PreferenceManager manager, IOnPreferenceTreeClickListener listener)
		{
			//FIXME How Should this Happen??? I dont need it so i leave it..
//			try {                
//				
//				Class cl = Java.Lang.Class.FromType (typeof(PreferenceManager));
//				Field onPreferenceTreeClickListener = cl.GetDeclaredField ("mOnPreferenceTreeClickListener");
//				onPreferenceTreeClickListener.Accessible = true;
//				if (listener != null) {
//					
//					Java.Lang.Object proxy = Proxy.NewProxyInstance (manager.Class.ClassLoader, new Class[] { Class.FromType (typeof(PreferenceManagerCompat).GetInterface ("IOnPreferenceTreeClickListener"))  }, new MyInvocationHandler (listener));
//					onPreferenceTreeClickListener.Set (manager, proxy);
//				} else {
//					onPreferenceTreeClickListener.Set (manager, null);
//				}
//			} catch (System.Exception e) {
//				Console.WriteLine (TAG + " Couldn't set PreferenceManager.mOnPreferenceTreeClickListener by reflection " + e.Message);
//			}
		}

		/**
     * Inflates a preference hierarchy from the preference hierarchies of
     * {@link Activity Activities} that match the given {@link Intent}. An
     * {@link Activity} defines its preference hierarchy with meta-data using
     * the {@link #METADATA_KEY_PREFERENCES} key.
     * <p>
     * If a preference hierarchy is given, the new preference hierarchies will
     * be merged in.
     * 
     * @param queryIntent The intent to match activities.
     * @param rootPreferences Optional existing hierarchy to merge the new
     *            hierarchies into.
     * @return The root hierarchy (if one was not provided, the new hierarchy's
     *         root).
     */
		public	static PreferenceScreen InflateFromIntent (PreferenceManager manager, Intent intent, PreferenceScreen screen)
		{

			try {
				Class cl = Java.Lang.Class.FromType (typeof(PreferenceManager));
				Method m = cl.GetDeclaredMethod ("inflateFromIntent", Class.FromType (typeof(Intent)), Class.FromType (typeof(PreferenceScreen)));
				m.Accessible = true;
				PreferenceScreen prefScreen = (PreferenceScreen)m.Invoke (manager, intent, screen);
				return prefScreen;
			} catch (System.Exception e) {
				Console.WriteLine (TAG + " Couldn't call PreferenceManager.inflateFromIntent by reflection " + e.Message);
			}
			return null;
		}

		/**
     * Inflates a preference hierarchy from XML. If a preference hierarchy is
     * given, the new preference hierarchies will be merged in.
     * 
     * @param context The context of the resource.
     * @param resId The resource ID of the XML to inflate.
     * @param rootPreferences Optional existing hierarchy to merge the new
     *            hierarchies into.
     * @return The root hierarchy (if one was not provided, the new hierarchy's
     *         root).
     * @hide
     */
		public	static PreferenceScreen InflateFromResource (PreferenceManager manager, Activity activity, int resId, PreferenceScreen screen)
		{
			try {
				Class cl = Java.Lang.Class.FromType (typeof(PreferenceManager));
				Method m = cl.GetDeclaredMethod ("inflateFromResource", Class.FromType (typeof(Context)), Java.Lang.Integer.Type, Class.FromType (typeof(PreferenceScreen)));
				m.Accessible = true;
				PreferenceScreen prefScreen = (PreferenceScreen)m.Invoke (manager, activity, resId, screen);
				return prefScreen;
			} catch (System.Exception e) {
				Console.WriteLine (TAG + " Couldn't call PreferenceManager.inflateFromResource by reflection " + e.Message);
			}
			return null;
		}

		/**
     * Returns the root of the preference hierarchy managed by this class.
     *  
     * @return The {@link PreferenceScreen} object that is at the root of the hierarchy.
     */
		public	static PreferenceScreen GetPreferenceScreen (PreferenceManager manager)
		{
			try {
				Class cl = Java.Lang.Class.FromType (typeof(PreferenceManager));
				Method m = cl.GetDeclaredMethod ("getPreferenceScreen");
				m.Accessible = true;
				return (PreferenceScreen)m.Invoke (manager);
			} catch (System.Exception e) {
				Console.WriteLine (TAG + " Couldn't call PreferenceManager.getPreferenceScreen by reflection " + e.Message);
			}
			return null;
		}

		/**
     * Called by the {@link PreferenceManager} to dispatch a subactivity result.
     */
		public	static void DispatchActivityResult (PreferenceManager manager, int requestCode, int resultCode, Intent data)
		{
			try {
				Class cl = Java.Lang.Class.FromType (typeof(PreferenceManager));
				Method m = cl.GetDeclaredMethod ("dispatchActivityResult", Java.Lang.Integer.Type, Java.Lang.Integer.Type, Class.FromType (typeof(Intent)));
				m.Accessible = true;
				m.Invoke (manager, requestCode, resultCode, data);
			} catch (System.Exception e) {
				Console.WriteLine (TAG + " Couldn't call PreferenceManager.dispatchActivityResult by reflection " + e.Message);
			}
		}

		/**
     * Called by the {@link PreferenceManager} to dispatch the activity stop
     * event.
     */
		public	static void DispatchActivityStop (PreferenceManager manager)
		{
			try {
				Class cl = Java.Lang.Class.FromType (typeof(PreferenceManager));
				Method m = cl.GetDeclaredMethod ("dispatchActivityStop");
				m.Accessible = true;
				m.Invoke (manager);
			} catch (System.Exception e) {
				Console.WriteLine (TAG + " Couldn't call PreferenceManager.dispatchActivityStop by reflection " + e.Message);
			}
		
		}



		/**
     * Called by the {@link PreferenceManager} to dispatch the activity destroy
     * event.
     */
		public	static void DispatchActivityDestroy (PreferenceManager manager)
		{
			try {
				Class cl = Java.Lang.Class.FromType (typeof(PreferenceManager));
				Method m = cl.GetDeclaredMethod ("dispatchActivityDestroy");
				m.Accessible = true;
				m.Invoke (manager);
			} catch (System.Exception e) {
				Console.WriteLine (TAG + " Couldn't call PreferenceManager.dispatchActivityDestroy by reflection " + e.Message);
			}
		}

		/**
     * Sets the root of the preference hierarchy.
     * 
     * @param preferenceScreen The root {@link PreferenceScreen} of the preference hierarchy.
     * @return Whether the {@link PreferenceScreen} given is different than the previous. 
     */
		public	static bool SetPreferences (PreferenceManager manager, PreferenceScreen screen)
		{
			try {
				Class cl = Java.Lang.Class.FromType (typeof(PreferenceManager));
				Method m = cl.GetDeclaredMethod ("setPreferences", Class.FromType (typeof(PreferenceScreen)));
				m.Accessible = true;
				return ((bool)m.Invoke (manager, screen));
			} catch (System.Exception e) {
				Console.WriteLine (TAG + " Couldn't call PreferenceManager.setPreferences by reflection " + e.Message);
			}
			return false;
		}

	}

}

