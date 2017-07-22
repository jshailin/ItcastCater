﻿using System;
namespace CaterModel
{
	/// <summary>
	/// memberinfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MemberInfo
	{
		public MemberInfo()
		{}
		#region Model
		private int _mid;
		private int? _mtypeid;
		private string _mname;
		private string _mphone;
		private decimal? _mmoney;
		private int? _misdelete;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int MId
		{
			set{ _mid=value;}
			get{return _mid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MTypeId
		{
			set{ _mtypeid=value;}
			get{return _mtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MName
		{
			set{ _mname=value;}
			get{return _mname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MPhone
		{
			set{ _mphone=value;}
			get{return _mphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MMoney
		{
			set{ _mmoney=value;}
			get{return _mmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MIsDelete
		{
			set{ _misdelete=value;}
			get{return _misdelete;}
		}
		#endregion Model

	}
}

