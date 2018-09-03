using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

[assembly: InternalsVisibleTo("VerismoTest.BLL, PublicKey=002400000480000094000000060200000024000052534131000400000100010043363B4F0CADF3A196E25179651F77E886BA00F25CA809AC402510CD4F604D92C067E71642A9781C1F81E563401E759DD1126E098E327985DDD5151C85B4D017490BDDE08A81396C47935B31EF9A4B416BF65538D030B27DA7B47D1ED8BC0F1038F85395CC282C4F5AA4191084A4F6194EDAB5A23B6A359FEED5D64B077129C3")]
namespace VerismoTest.DAL
{
	/// <summary>En snabbt ihopslängd klass för CRUD.</summary>
	internal class Repository
	{
		// =============================================================================================================

		#region Fields/Properties

		// Detta är platsen där datat sparas.
		private static String DataBaseDirectory = AppDomain.CurrentDomain.BaseDirectory + "/../Data";

		#endregion

		// =============================================================================================================

		#region Constructors/Destructors

		/// <summary>Finns ingen anledning att göra en instans av denna klass.</summary>
		public Repository()
		{

		}

		#endregion

		// =============================================================================================================

		#region Retrieve

		/// <summary>Hämtar ut ALLA "poster" av typen som skickas med.</summary>
		public static IEnumerable<T> RetrieveAll<T>()
		{
			String dataDirectory = Path.Combine(DataBaseDirectory, typeof(T).Name);

			if (!Directory.Exists(dataDirectory))
			{
				Directory.CreateDirectory(dataDirectory);
			}

			DirectoryInfo directoryInfo = new DirectoryInfo(dataDirectory);

			foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.xml", SearchOption.AllDirectories))
			{
				Stream stream = fileInfo.Open(FileMode.Open);
				DataContractSerializer serializer = new DataContractSerializer(typeof(T));
				dynamic item = serializer.ReadObject(stream);
				stream.Close();

				yield return item;
			}
		}

		// -------------------------------------------------------------------------------------------------------------

		/// <summary>Hämtar ut en specifik post av den typ som skickas med.</summary>
		public static void Retrieve<T>(T item, Guid id)
		{
			String dataDirectory = Path.Combine(DataBaseDirectory, typeof(T).Name);

			if (!Directory.Exists(dataDirectory))
			{
				Directory.CreateDirectory(dataDirectory);
			}

			String dataFileName = Path.Combine(dataDirectory, id.ToString("N") + ".xml");

			if (!File.Exists(dataFileName))
			{
				throw new Exception(id.ToString("N") + " hittades inte!");
			}

			FileStream stream = File.OpenRead(dataFileName);
			DataContractSerializer serializer = new DataContractSerializer(typeof(T));
			dynamic dataContract = serializer.ReadObject(stream);
			stream.Close();

			foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
			{
				propertyInfo.SetValue(item, propertyInfo.GetValue(dataContract, null), null);
			}
		}

		#endregion

		// =============================================================================================================

		#region Create/Update

		/// <summary>Skapar ny om den inte finns, annars uppdateras posten.</summary>
		public static void Save<T>(T item)
		{
			String dataDirectory = Path.Combine(DataBaseDirectory, typeof(T).Name);

			if (!Directory.Exists(dataDirectory))
			{
				Directory.CreateDirectory(dataDirectory);
			}

			String dataFileName = Path.Combine(dataDirectory, ((Guid)item.GetType().GetProperty("ID").GetValue(item, null)).ToString("N") + ".xml");

			Stream stream = File.Open(dataFileName, FileMode.Create);
			DataContractSerializer serializer = new DataContractSerializer(typeof(T));
			serializer.WriteObject(stream, item);
			stream.Close();
		}

		#endregion

		// =============================================================================================================

		#region Delete

		/// <summary>Tar bort posten.</summary>
		public static void Delete<T>(T item)
		{
			String dataDirectory = Path.Combine(DataBaseDirectory, typeof(T).Name);

			if (!Directory.Exists(dataDirectory))
			{
				Directory.CreateDirectory(dataDirectory);
			}

			String dataFileName = Path.Combine(dataDirectory, ((Guid)item.GetType().GetProperty("ID").GetValue(item, null)).ToString("N") + ".xml");

			File.Delete(dataFileName);
		}

		#endregion

		// =============================================================================================================
	}
}