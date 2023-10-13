using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vortex.Models
{
	public class Report

	{

		[Key]
		public int ReportID { get; set; }
		[DisplayName("Fault type:")]
		public string FaultTyp { get; set; }
		public enum FaultTypList

		{
			Electric,
			Plumbing,
			Carpentry,
			Building
		}
		[DisplayName("additional info:")]
		public string FaultDescr{ get; set; }
		public string Location { get; set; }
		public enum LocationList


		{
			Steve_Biko,
			Ritson,
			City_Campus,
			Ml_Sultan

		}
		[DisplayName("Optional* type location")]
		public string OptionalLocation { get; set; }


		[DisplayName("Upload File")]
		public string ImagePath { get; set; }
		[NotMapped]
		public HttpPostedFileBase ImageFile { get; set; }

		[DisplayName("Fault progress")]
		public string Faultprgrss { get; set; }

		public enum ProgressList



		{
			Pending,
			Completed

		}

		public string Name { get; set; }

		public string Surname { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		private DateTime _returnDate = DateTime.MinValue;
		[Display(Name = "Report Date")]
		public DateTime Date
		{
			get
			{
				return (_returnDate == DateTime.MinValue) ? DateTime.Now : _returnDate;
			}
			set { _returnDate = value; }
		}
		

	}
}