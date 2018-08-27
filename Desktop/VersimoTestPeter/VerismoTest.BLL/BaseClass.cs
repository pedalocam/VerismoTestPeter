using System;
using System.Runtime.Serialization;

namespace VerismoTest.BLL
{
	/// <summary>Basklass som tar hand om ID och Created.</summary>
	[DataContract(Namespace = "http://test.verismo.se")]
	public class BaseClass
	{
		// =============================================================================================================

		#region Fields/Properties

		/// <summary>Ett unikt ID per post.</summary>
		[DataMember]
		public Guid ID { get; internal set; }

		/// <summary>Datum när posten skapades.</summary>
		[DataMember]
		public DateTime Created { get; internal set; }

		#endregion

		// =============================================================================================================

		#region Constructors/Destructors

		/// <summary>Generera ID när en ny instans skapas.</summary>
		public BaseClass()
		{
			this.ID = Guid.NewGuid();
			this.Created = DateTime.Now;
		}

		#endregion

		// =============================================================================================================
	}
}