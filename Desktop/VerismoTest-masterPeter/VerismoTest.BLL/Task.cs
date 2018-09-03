using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using VerismoTest.DAL;

namespace VerismoTest.BLL
{
	/// <summary>En klass för "Tasks" som använder sig av BaseClass och Repository för att sparas/ändras/ta bort.</summary>
	[DataContract(Namespace = "http://test.verismo.se")]
	public class Task : BaseClass
	{
		// =============================================================================================================

		#region Fields/Properties

		/// <summary>Namn på din "Task".</summary>
		[DataMember]
		public String Name { get; set; }

		/// <summary>En beskrivning...</summary>
		[DataMember]
		public String Description { get; set; }

		/// <summary>"Bäst"-före-datum :)</summary>
		[DataMember]
		public DateTime DueDate { get; set; }

		#endregion

		// =============================================================================================================

		#region Constructors/Destructors

		/// <summary>Finns inget vi behöver göra här för tillfället...</summary>
		public Task()
		{

		}

		// -------------------------------------------------------------------------------------------------------------

		/// <summary>Vi gör en ny instans av "Task" genom att skicka med dess ID. Då hämtas motsvarande post via DAL och sedan populerar vi vår "Task" med dess värden.</summary>
		public Task(Guid id)
		{
			Repository.Retrieve<Task>(this, id);
		}

		#endregion

		// =============================================================================================================

		#region Retrieve

		/// <summary>Hämtar ALLA "Tasks".</summary>
		public static IEnumerable<Task> RetrieveAll()
		{
			return Repository.RetrieveAll<Task>();
		}

		#endregion

		// =============================================================================================================

		#region Create/Update

		/// <summary>Sparar "Task" på disk.</summary>
		public void Save()
		{
			Repository.Save<Task>(this);
		}

		#endregion

		// =============================================================================================================

		#region Delete

		/// <summary>Tar bort "Task" från disk.</summary>
		public void Delete()
		{
			Repository.Delete<Task>(this);
		}

		#endregion

		// =============================================================================================================
	}
}