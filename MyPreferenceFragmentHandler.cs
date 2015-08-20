using System;
using Android.OS;

namespace SupportPreference{
	public class MyPreferenceFragmentHandler:Handler{
		private SupportPreferenceFragment _fragment;

		public MyPreferenceFragmentHandler(SupportPreferenceFragment fragment){
		_fragment = fragment;
		}

		public override void HandleMessage(Message msg){
			
		switch(msg.What){
			case SupportPreferenceFragment.MSG_BIND_PREFERENCES:
				if(_fragment != null){
					_fragment.BindPreferences();
				}
			break;
		}
		}
	
	
	}
}

