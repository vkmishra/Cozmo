using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace lazymonster {
	[Serializable]
	public class Configuration {
		bool SoundEnabled;
		float UIVolume;
		float MusicVolume;

		public Configuration() {
			loadDefaults();
		}

		public void loadDefaults(){
			
			SoundEnabled = true;
			UIVolume = 0.5f;
			MusicVolume = 1.0f;
		}

		public bool isSoundEnabled(){
			return SoundEnabled;
		}

		public float getUIVolume(){
			return UIVolume;
		}

		public float getMusicVolume(){
			return MusicVolume;
		}

		public void toggleSound(){
			SoundEnabled = !SoundEnabled;
		}
		
		public void updateVolumeUI(float value) {
			UIVolume = value;
		}
		
		public void updateVolumeMusic(float value) {
			MusicVolume = value;
		}
	}

	public class Settings {
		static void Init(){
			Configuration c = new Configuration();
			using (Stream s = File.Open("config.dat", FileMode.Create)){
				BinaryFormatter b = new BinaryFormatter();
				b.Serialize(s, c);
			}
		}

		static public void ResetSettings(){
			if(File.Exists("config.dat"))
				File.Delete("config.dat");
			Init ();
		}

		static public Configuration GetSettings(){
			Configuration c;
			if(!File.Exists("config.dat"))
				Init ();
			using (Stream s = File.Open("config.dat", FileMode.Open)){
				BinaryFormatter b = new BinaryFormatter();
				c = (Configuration)b.Deserialize(s);
			}
			return c;
		}

		static public void UpdateSettings(Configuration newConfig){
			if(File.Exists("config.dat"))
				File.Delete("config.dat");
			using (Stream s = File.Open("config.dat", FileMode.Create)){
				BinaryFormatter b = new BinaryFormatter();
				b.Serialize(s, newConfig);
			}
		}
	}
}