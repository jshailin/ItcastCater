using System;
namespace CaterModel
{
	/// <summary>
	/// DishTypeInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DishTypeInfo
	{
		public DishTypeInfo()
		{}
		#region Model
		private int _did;
		private string _dtitle;
		private int? _disdelete;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int DId
		{
			set{ _did=value;}
			get{return _did;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DTitle
		{
			set{ _dtitle=value;}
			get{return _dtitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DIsDelete
		{
			set{ _disdelete=value;}
			get{return _disdelete;}
		}
		#endregion Model

	}
}

