using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vortex.Models
{
	public class Points
	{
		[Key]
		public int PointsID { get; set; }

		public int Point { get; set; }

		public string Reward { get; set; }
		public virtual Report Report { get; set; }
		public int ReportID { get; set; }
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		private DateTime _returnDate = DateTime.MinValue;
		[Display(Name = "Reward date")]
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