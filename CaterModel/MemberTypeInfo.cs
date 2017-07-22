using System;
namespace CaterModel
{
	/// <summary>
	/// membertypeinfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MemberTypeInfo
	{
		public MemberTypeInfo()
		{}
		#region Model
		private int _mid;
		private string _mtitle;
		private decimal? _mdiscount;
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
		public string MTitle
		{
			set{ _mtitle=value;}
			get{return _mtitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MDiscount
		{
			set{ _mdiscount=value;}
			get{return _mdiscount;}
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

