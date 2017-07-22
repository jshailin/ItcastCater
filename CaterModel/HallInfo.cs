using System;
namespace CaterModel
{
	/// <summary>
	/// HallInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class HallInfo
	{
		public HallInfo()
		{}
		#region Model
		private int _hid;
		private string _htitle;
		private int? _hisdelete;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int HId
		{
			set{ _hid=value;}
			get{return _hid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HTitle
		{
			set{ _htitle=value;}
			get{return _htitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? HIsDelete
		{
			set{ _hisdelete=value;}
			get{return _hisdelete;}
		}
		#endregion Model

	}
}

