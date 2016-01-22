using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

namespace lazymonster {
	[Serializable]
	public class dat{
		int level, highScore;
		bool completed, unlocked;
		
		public dat(int l)
		{
			level = l;
			highScore = 0;
			completed = false;
			unlocked = false;
		}
		public void complete()
		{
			completed = true;
		}
		
		public void unlock()
		{
			unlocked = true;
		}

		public bool unlockCheck()
		{
			return unlocked;
		}
	}

	public class DataHandling : MonoBehaviour {

		public static void Complete(int l)
		{
			List<dat> d = GetList();
			d[l - 1].complete();
			d[l].unlock();
			UpdateList(d);
		}

		static void InitDat()
		{
			List<dat> datList = new List<dat>();
			for (int i = 0; i < 20; i++)
			{
				dat da = new dat(i + 1);
				datList.Add(da);
			}
			datList[0].unlock();
			
			using (Stream stream = File.Open("datafile", FileMode.Create))
			{
				BinaryFormatter b = new BinaryFormatter();
				b.Serialize(stream, datList);
			}
		}
		
		public static List<dat> GetList()
		{
			List<dat> d = new List<dat>();
			Stream stream;
			try
			{
				stream = File.Open("datafile", FileMode.Open);
			}
			catch(Exception e)
			{
				InitDat();
				stream = File.Open("datafile", FileMode.Open);
				
			}
			
			BinaryFormatter b = new BinaryFormatter();
			d = (List<dat>)b.Deserialize(stream);
			stream.Close();
			return d;
		}
		
		public static bool UpdateList(List<dat> d)
		{
			try
			{
				using (Stream stream = File.Open("datafile", FileMode.Create))
				{
					BinaryFormatter b = new BinaryFormatter();
					b.Serialize(stream, d);
				}
			}
			catch(Exception e)
			{
				return false;
			}
			
			return true;
		}

		public static void GameReset(){
			if(File.Exists("datafile"))
				File.Delete("datafile");
			InitDat();
		}
	}
}
