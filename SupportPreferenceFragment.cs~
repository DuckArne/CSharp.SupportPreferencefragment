using System;
using Android.Preferences;
using Android.App;
using Java.Lang.Reflect;
using Java.Interop;
using Android.Widget;
using Java.Lang;
using System.Threading.Tasks;
using Android.OS;
using Android.Views;
using Android.Content;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace SupportPreference
{
	
	public class SupportPreferenceFragment: SupportFragment, PreferenceManagerCompat.IOnPreferenceTreeClickListener
	{
		

		private static readonly string PREFERENCES_TAG = "android:preferences";

		private PreferenceManager mPreferenceManager;
		private ListView mList;
		private bool mHavePrefs;
		private bool mInitDone;
		private MyPreferenceFragmentHandler mHandler;

		public static event EventHandler OnActivityDestroy;

		/**
     * The starting request code given out to preference framework.
     */
		private const int FIRST_REQUEST_CODE = 100;

		public const int MSG_BIND_PREFERENCES = 1;


		Action MRequestFocus;



	


		/**
     * Interface that PreferenceFragment's containing activity should
     * implement to be able to process preference items that wish to
     * switch to a new fragment.
     */
		public interface IOnPreferenceStartFragmentCallback
		{
			/**
         * Called when the user has clicked on a Preference that has
         * a fragment class name associated with it.  The implementation
         * to should instantiate and switch to an instance of the given
         * fragment.
         */

			bool OnPreferenceStartFragment (SupportPreferenceFragment caller, Preference pref);
		}

		private static void ActivityDestroy ()
		{
			var handler = OnActivityDestroy;
			if (handler != null) {
				Console.WriteLine (SupportPreferenceFragment.OnActivityDestroy.ToString ());
				handler (typeof(SupportPreferenceFragment), EventArgs.Empty);
			}
		}

		public override void OnCreate (Bundle paramBundle)
		{
			base.OnCreate (paramBundle);
			mPreferenceManager = PreferenceManagerCompat.NewInstance (Activity, FIRST_REQUEST_CODE);
			PreferenceManagerCompat.SetFragment (mPreferenceManager, this);
		}


		public override View OnCreateView (LayoutInflater paramLayoutInflater, ViewGroup paramViewGroup, Bundle paramBundle)
		{
			return paramLayoutInflater.Inflate (Resource.Layout.preference_list_fragment, paramViewGroup, false);
		}



		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);

			MRequestFocus = delegate {
				mList.FocusableViewAvailable (mList);
			};

			mHandler = new MyPreferenceFragmentHandler (this);
		
			if (mHavePrefs) {
				BindPreferences ();
			}

			mInitDone = true;

			if (savedInstanceState != null) {
				Bundle container = savedInstanceState.GetBundle (PREFERENCES_TAG);
				if (container != null) {
					PreferenceScreen preferenceScreen = PreferenceScreen;
					if (preferenceScreen != null) {
						preferenceScreen.RestoreHierarchyState (container);
					}
				}
			}
		}


		public override void OnStart ()
		{
			base.OnStart ();
		
			//FIXME PreferenceManagerCompat.SetOnPreferenceTreeClickListener (mPreferenceManager, this);
		}

	
		public  override void OnStop ()
		{
			
			base.OnStop ();
			PreferenceManagerCompat.DispatchActivityStop (mPreferenceManager);
			//FIXME PreferenceManagerCompat.SetOnPreferenceTreeClickListener (mPreferenceManager, null);
		}


		public override void OnDestroyView ()
		{
			mList = null;
			mHandler.RemoveCallbacks (MRequestFocus);
			mHandler.RemoveMessages (MSG_BIND_PREFERENCES);
			base.OnDestroyView ();
		}


		public override void OnDestroy ()
		{
			base.OnDestroy ();
		
			PreferenceManagerCompat.DispatchActivityDestroy (mPreferenceManager);
			ActivityDestroy ();
		}


		public override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState);

			PreferenceScreen preferenceScreen = PreferenceScreen;
			if (preferenceScreen != null) {
				Bundle container = new Bundle ();
				preferenceScreen.SaveHierarchyState (container);
				outState.PutBundle (PREFERENCES_TAG, container);
			}
		}



		public override void OnActivityResult (int requestCode, int resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			PreferenceManagerCompat.DispatchActivityResult (mPreferenceManager, requestCode, resultCode, data);
		}

		/**
     * Returns the {@link PreferenceManager} used by this fragment.
     * @return The {@link PreferenceManager}.
     */
		public PreferenceManager GetPreferenceManager ()
		{
			return mPreferenceManager;
		}

		public PreferenceManager PreferenceManager { get { return mPreferenceManager; } }

		/**
     * Sets the root of the preference hierarchy that this fragment is showing.
     *
     * @param preferenceScreen The root {@link PreferenceScreen} of the preference hierarchy.
     */

		public void SetPreferenceScreen (PreferenceScreen preferenceScreen)
		{
			
			if (PreferenceManagerCompat.SetPreferences (mPreferenceManager, preferenceScreen) && preferenceScreen != null) {
				mHavePrefs = true;
				if (mInitDone) {
					PostBindPreferences ();
				}
			}
		}

		public  PreferenceScreen PreferenceScreen {
			get { return PreferenceManagerCompat.GetPreferenceScreen (mPreferenceManager); }
			set {
				if (PreferenceManagerCompat.SetPreferences (mPreferenceManager, value) && value != null) {
					mHavePrefs = true;
					if (mInitDone) {
						PostBindPreferences ();
					}
				}
			}
		}

		/**
     * Gets the root of the preference hierarchy that this fragment is showing.
     *
     * @return The {@link PreferenceScreen} that is the root of the preference
     *         hierarchy.
     */
		public PreferenceScreen GetPreferenceScreen ()
		{
			return PreferenceManagerCompat.GetPreferenceScreen (mPreferenceManager);
		}

		/**
     * Adds preferences from activities that match the given {@link Intent}.
     *
     * @param intent The {@link Intent} to query activities.
     */
		public void AddPreferencesFromIntent (Intent intent)
		{
			RequirePreferenceManager ();

			SetPreferenceScreen (PreferenceManagerCompat.InflateFromIntent (mPreferenceManager, intent, GetPreferenceScreen ()));
		}

		/**
     * Inflates the given XML resource and adds the preference hierarchy to the current
     * preference hierarchy.
     *
     * @param preferencesResId The XML resource ID to inflate.
     */
		public void AddPreferencesFromResource (int preferencesResId)
		{
			RequirePreferenceManager ();

			SetPreferenceScreen (PreferenceManagerCompat.InflateFromResource (mPreferenceManager, Activity, preferencesResId, PreferenceScreen));
		}

		/**
     * {@inheritDoc}
     */
		public bool OnPreferenceTreeClick (PreferenceScreen preferenceScreen, Preference preference)
		{
			if (Activity is IOnPreferenceStartFragmentCallback) {
				return ((IOnPreferenceStartFragmentCallback)Activity).OnPreferenceStartFragment (this, preference);
			}
			return false;
		}

		/**
     * Finds a {@link Preference} based on its key.
     *
     * @param key The key of the preference to retrieve.
     * @return The {@link Preference} with the key, or null.
     * @see PreferenceGroup#findPreference(CharSequence)
     */
		public Preference FindPreference (ICharSequence key)
		{
			if (mPreferenceManager == null) {
				return null;
			}
			return mPreferenceManager.FindPreference (key);
		}

		private void RequirePreferenceManager ()
		{
			if (mPreferenceManager == null) {
				throw new RuntimeException ("This should be called after super.onCreate.");
			}
		}

		private void PostBindPreferences ()
		{
			if (mHandler.HasMessages (MSG_BIND_PREFERENCES))
				return;
			
			mHandler.ObtainMessage (MSG_BIND_PREFERENCES).SendToTarget ();
		}

		public ListView GetListView ()
		{
			EnsureList ();
			return mList;
		}

		public ListView ListView {
			get {
				EnsureList ();
				return mList;
			}
		}

		private void EnsureList ()
		{
			if (mList != null) {
				return;
			}
			View root = View;
			if (root == null) {
				throw new IllegalStateException ("Content view not yet created");
			}
			View rawListView = root.FindViewById (Android.Resource.Id.List);
			if (!(rawListView is ListView)) {
				throw new RuntimeException ("Content has view with id attribute 'android.R.id.list' "
				+ "that is not a ListView class");
			}
			mList = (ListView)rawListView;
			if (mList == null) {
				throw new RuntimeException ("Your content must have a ListView whose id attribute is " +
				"'android.R.id.list'");
			}
		

			mHandler.Post (MRequestFocus);
		}

		public void BindPreferences ()
		{
			PreferenceScreen preferenceScreen = PreferenceScreen;
			if (preferenceScreen != null) {
				
				preferenceScreen.Bind (GetListView ());
			}

			if (Build.VERSION.SdkInt <= BuildVersionCodes.GingerbreadMr1) {
				// Workaround android bug for SDK 10 and below - see
				// https://github.com/android/platform_frameworks_base/commit/2d43d283fc0f22b08f43c6db4da71031168e7f59

				GetListView ().ItemClick += (object sender, Android.Widget.AdapterView.ItemClickEventArgs e) => {
					// If the list has headers, subtract them from the index.
					int pos = e.Position;		
					if (sender is ListView) {
						pos -= ((ListView)sender).HeaderViewsCount;
					}
				
					Java.Lang.Object item = PreferenceScreen.RootAdapter.GetItem (pos);
					if (!(item is Preference))
						return;

					Preference preference = (Preference)item;
					try {
						Method performClick = Class.FromType (typeof(Preference)).Class.GetDeclaredMethod ("performClick", Class.FromType (typeof(PreferenceScreen)));
						performClick.Accessible = true;
						performClick.Invoke (preference, preferenceScreen);
					} catch (InvocationTargetException e1) {
						Console.WriteLine (Tag + " " + e1.Message);
					} catch (IllegalAccessException e2) {
						Console.WriteLine (Tag + " " + e2.Message);
					} catch (NoSuchMethodException e3) {
						Console.WriteLine (Tag + " " + e3.Message);
					}
				};

				

			
			}

		


		}
		//		private OnKeyListener mListOnKeyListener = new OnKeyListener() {
		//
		//			@Override
		//			public boolean onKey(View v, int keyCode, KeyEvent event) {
		//				Object selectedItem = mList.getSelectedItem();
		//				if (selectedItem instanceof Preference) {
		//					@SuppressWarnings("unused")
		//					View selectedView = mList.getSelectedView();
		//					//return ((Preference)selectedItem).onKey(
		//					//        selectedView, keyCode, event);
		//					return false;
		//				}
		//				return false;
		//			}
		//
		//		};
	}

}