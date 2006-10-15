//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------


using System;
using System.Drawing;
using System.ComponentModel;
using System.Xml.Serialization;

using SharpReportCore.Exporters;
	
	
/// <summary>
/// This Class is used for Databased items
/// </summary>
/// <remarks>
/// 	created by - Forstmeier Peter
/// 	created on - 22.08.2005 00:12:59
/// </remarks>
namespace SharpReportCore {	
	public class BaseDataItem : SharpReportCore.BaseTextItem,IItemRenderer,
								IExportColumnBuilder {
		
		private string columnName;
		private string baseTableName;
		private string dbValue;
		private string dataType;
		private string nullValue;
		
		
		#region Constructor
		
		public BaseDataItem():base() {
			this.dataType = "System.String";
		}
		
		public BaseDataItem(string columnName):base(){
			this.columnName = columnName;
			this.dataType = "System.String";
		}
		
		#endregion
		
		#region privates
		
		//TODO Need a much better handling for 'null' values
		
		private string CheckForNullValue() {
			if (String.IsNullOrEmpty(this.dbValue)) {
				if (String.IsNullOrEmpty(this.nullValue)) {
					return GlobalValues.UnboundName;
					
				} else
					return this.nullValue;
			}
			return this.dbValue;
		}
	
		#endregion
		
		#region IExportColumnBuilder  implementation
//		public new IPerformLine  CreateExportColumn(Graphics graphics)
		public new BaseExportColumn  CreateExportColumn(Graphics graphics)
		{
			string toPrint = CheckForNullValue();
			BaseStyleDecorator st = base.CreateItemStyle(graphics);
			ExportText item = new ExportText(st,false);
			item.Text = base.FormatOutput(toPrint,
			                              this.FormatString,
			                              DataTypeHelper.TypeCodeFromString (this.dataType),
			                              this.nullValue);
			return item;
		}
		
		#endregion
		
		public override void Render(SharpReportCore.ReportPageEventArgs rpea) {
			
//			TypeCode tc = Type.GetTypeCode( Type.GetType(this.dataType));

			// TODO this.DbValue should beformatted in the BeforePrintEvent
			
			string toPrint = CheckForNullValue();
			
			
			base.Text = base.FormatOutput(toPrint,
			                              this.FormatString,
			                              DataTypeHelper.TypeCodeFromString (this.dataType),
			                              this.nullValue);
			base.Render (rpea);
			
		}
		
		public override string ToString() {
			return "BaseDataItem";
		}
		
		#region Properies
		
		[XmlIgnoreAttribute]
		[Browsable(false)]
		public virtual string DbValue {
			get {
				return dbValue;
			}
			set {
				dbValue = value;
			}
		}
		
		[Browsable(true),
		 Category("Databinding"),
		 Description("Datatype of the underlying Column")]
		
		public virtual string ColumnName {
			get {
				if (String.IsNullOrEmpty(columnName)) {
					this.columnName = GlobalValues.UnboundName;
				}
				return columnName;
			}
			set {
				columnName = value;
				this.Text = this.columnName;
			}
		}
		
		
		[Browsable(true),
		 Category("Databinding"),
		 Description("Datatype of the underlying Column")]
		public string DataType {
			get {
				return dataType;
			}
			set {
				dataType = value;
			}
		}
		
		///<summary>
		/// Mappingname to Datasource
		/// </summary>
		/// 
		[Browsable(true),
		 Category("Databinding"),
		 Description("Mapping Name to DataTable")]
		[XmlIgnoreAttribute]
		public string MappingName {
			get {
				return baseTableName + "." + columnName;
			} 
		}
		
		
		[Browsable(true),
		 Category("Databinding"),
		 Description("TableName")]
		public string BaseTableName {
			get {
				return baseTableName;
			}
			set {
				baseTableName = value;
			}
		}
		[Browsable(true),
		 Category("Databinding"),
		 Description("Display Value for empty Field")]
		public string NullValue {
			get {
				return nullValue;
			}
			set {
				nullValue = value;
			}
		}
		
		#endregion
		
	}
}
