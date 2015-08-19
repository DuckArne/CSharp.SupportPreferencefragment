using System;
using Android.Preferences;
using Android.App;
using Java.Lang.Reflect;
using Java.Interop;
using Java.Lang;
using Android.Content;


using Java.Lang

namespace SupportPreference{
public class PreferenceManagerCompat {

		private static readonly string TAG = PreferenceManagerCompat.Class.SimpleName;

		/**
     * Interface definition for a callback to be invoked when a
     * {@link Preference} in the hierarchy rooted at this {@link PreferenceScreen} is
     * clicked.
     */
	public interface IOnPreferenceTreeClickListener {
			/**
         * Called when a preference in the tree rooted at this
         * {@link PreferenceScreen} has been clicked.
         * 
         * @param preferenceScreen The {@link PreferenceScreen} that the
         *        preference is located in.
         * @param preference The preference that was clicked.
         * @return Whether the click was handled.
         */
			bool OnPreferenceTreeClick(PreferenceScreen preferenceScreen, Preference preference);
		}

		static PreferenceManager NewInstance(Activity activity, int firstRequestCode) {
			try {
			Constructor<PreferenceManager> c = PreferenceManager.Class.GetDeclaredConstructor(typeof(Activity),typeof(int));
				c.SetAccessible(true);
				return c.NewInstance(activity, firstRequestCode);
			} catch (Exception e) {
				Console.WriteLine (TAG +" Couldn't call constructor PreferenceManager by reflection "+ e.Message);
			}
			return null;
		}

		/**
     * Sets the owning preference fragment
     */
		static void SetFragment(PreferenceManager manager, PreferenceFragment fragment) {
			// stub
		}

		/**
     * Sets the callback to be invoked when a {@link Preference} in the
     * hierarchy rooted at this {@link PreferenceManager} is clicked.
     * 
     * @param listener The callback to be invoked.
     */
		static void SetOnPreferenceTreeClickListener(PreferenceManager manager,  IOnPreferenceTreeClickListener listener) {
			try {
				Field onPreferenceTreeClickListener = PreferenceManager.Class.GetDeclaredField("mOnPreferenceTreeClickListener");
				onPreferenceTreeClickListener.SetAccessible(true);
				if (listener != null) {
					Java.Lang.Object proxy = Proxy.NewProxyInstance(
						onPreferenceTreeClickListener.Type.ClassLoader,
						new Class[] { onPreferenceTreeClickListener.GetType() },
						new MyInvocationHandler(listener));
					onPreferenceTreeClickListener.Set(manager, proxy);
				} else{
					onPreferenceTreeClickListener.Set(manager, null);
				}
			} catch(System.Exception e) {
				Console.WriteLine(TAG+ " Couldn't set PreferenceManager.mOnPreferenceTreeClickListener by reflection "+ e.Message);
			}
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
		static PreferenceScreen InflateFromIntent(PreferenceManager manager, Intent intent, PreferenceScreen screen) {

		try {
				Method m = PreferenceManager.Class.GetDeclaredMethod("inflateFromIntent", typeof(Intent), typeof(PreferenceScreen));
				m.SetAccessible(true);
				PreferenceScreen prefScreen = (PreferenceScreen) m.Invoke(manager, intent, screen);
				return prefScreen;
			} catch (System.Exception e) {
						Console.WriteLine(TAG+" Couldn't call PreferenceManager.inflateFromIntent by reflection "+ e.Message);
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
		static PreferenceScreen InflateFromResource(PreferenceManager manager, Activity activity, int resId, PreferenceScreen screen) {
			try {
							Method m = PreferenceManager.Class.GetDeclaredMethod("inflateFromResource", typeof(Context), typeof(int), typeof(PreferenceScreen));
				m.SetAccessible(true);
				PreferenceScreen prefScreen = (PreferenceScreen) m.Invoke(manager, activity, resId, screen);
				return prefScreen;
			} catch (System.Exception e) {
									Console.WriteLine(TAG+ " Couldn't call PreferenceManager.inflateFromResource by reflection "+e.Message);
			}
			return null;
		}

		/**
     * Returns the root of the preference hierarchy managed by this class.
     *  
     * @return The {@link PreferenceScreen} object that is at the root of the hierarchy.
     */
		static PreferenceScreen GetPreferenceScreen(PreferenceManager manager) {
			try {
				Method m = PreferenceManager.Class.GetDeclaredMethod("getPreferenceScreen");
				m.SetAccessible(true);
				return (PreferenceScreen) m.Invoke(manager);
			} catch (System.Exception e) {
										Console.WriteLine(TAG +" Couldn't call PreferenceManager.getPreferenceScreen by reflection "+ e.Message);
			}
			return null;
		}

		/**
     * Called by the {@link PreferenceManager} to dispatch a subactivity result.
     */
		static void DispatchActivityResult(PreferenceManager manager, int requestCode, int resultCode, Intent data) {
			try {
										Method m = PreferenceManager.Class.GetDeclaredMethod("dispatchActivityResult", typeof(int), typeof(int), typeof(Intent));
				m.SetAccessible(true);
				m.Invoke(manager, requestCode, resultCode, data);
			} catch (System.Exception e) {
												Console.WriteLine(TAG + " Couldn't call PreferenceManager.dispatchActivityResult by reflection "+e.Message);
			}
		}

		/**
     * Called by the {@link PreferenceManager} to dispatch the activity stop
     * event.
     */
		static void DispatchActivityStop(PreferenceManager manager) {
			try {
				Method m = PreferenceManager.Class.GetDeclaredMethod("dispatchActivityStop");
				m.SetAccessible(true);
				m.Invoke(manager);
			} catch (System.Exception e) {
				Console.WriteLine(TAG +" Couldn't call PreferenceManager.dispatchActivityStop by reflection " +e.Message);
			}
		}

		/**
     * Called by the {@link PreferenceManager} to dispatch the activity destroy
     * event.
     */
		static void DispatchActivityDestroy(PreferenceManager manager) {
			try {
				Method m = PreferenceManager.Class.GetDeclaredMethod("dispatchActivityDestroy");
				m.SetAccessible(true);
				m.Invoke(manager);
			} catch (System.Exception e) {
				Console.WriteLine(TAG+  " Couldn't call PreferenceManager.dispatchActivityDestroy by reflection "+e.Message);
			}
		}

		/**
     * Sets the root of the preference hierarchy.
     * 
     * @param preferenceScreen The root {@link PreferenceScreen} of the preference hierarchy.
     * @return Whether the {@link PreferenceScreen} given is different than the previous. 
     */
		static bool SetPreferences(PreferenceManager manager, PreferenceScreen screen) {
			try {
				Method m = PreferenceManager.Class.GetDeclaredMethod("setPreferences", typeof(PreferenceScreen));
				m.SetAccessible(true);
				return ((bool) m.Invoke(manager, screen));
			} catch (System.Exception e) {
				Console.WriteLine(TAG+" Couldn't call PreferenceManager.setPreferences by reflection "+e.Message);
			}
			return false;
		}

	}

}

