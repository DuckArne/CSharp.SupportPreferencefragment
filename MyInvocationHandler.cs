using System;
using Android.OS;
using Java.Lang.Reflect;
using Android.Preferences;


namespace SupportPreference{
	public class MyInvocationHandler:Handler,IInvocationHandler{
		private PreferenceManagerCompat.IOnPreferenceTreeClickListener _listener;

		public MyInvocationHandler(PreferenceManagerCompat.IOnPreferenceTreeClickListener listener){
		_listener = listener;
		}

		public Java.Lang.Object Invoke(Java.Lang.Object proxy, Method method, Java.Lang.Object[] args){
		if(method.Name.Equals("OnPreferenceTreeClick")){
			return _listener.OnPreferenceTreeClick((PreferenceScreen)args[0], (Preference)args[1]);
		} else{
			return null;
		}
		}
	}
}

