using System;
namespace CaterModel
{
	/// <summary>
	/// TableInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TableInfo
	{
		public TableInfo()
		{}
		#region Model
		private int _tid;
		private string _ttitle;
		private int? _thallid;
		private int? _tisfree;
		private int? _tisdelete;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int TId
		{
			set{ _tid=value;}
			get{return _tid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TTitle
		{
			set{ _ttitle=value;}
			get{return _ttitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? THallId
		{
			set{ _thallid=value;}
			get{return _thallid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TIsFree
		{
			set{ _tisfree=value;}
			get{return _tisfree;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TIsDelete
		{
			set{ _tisdelete=value;}
			get{return _tisdelete;}
		}
		#endregion Model

	}
}

