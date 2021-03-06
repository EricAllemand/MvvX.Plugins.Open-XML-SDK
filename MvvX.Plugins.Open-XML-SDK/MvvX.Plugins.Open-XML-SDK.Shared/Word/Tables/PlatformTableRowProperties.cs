﻿using DocumentFormat.OpenXml.Wordprocessing;
using MvvX.Plugins.OpenXMLSDK.Word.Tables;

namespace MvvX.Plugins.OpenXMLSDK.Platform.Word.Tables
{
    public class PlatformTableRowProperties : PlatformOpenXmlElement, ITableRowProperties
    {
        private readonly TableRowProperties xmlElement;

        public PlatformTableRowProperties()
            : this(new TableRowProperties())
        {
        }

        public PlatformTableRowProperties(TableRowProperties tableCell)
            : base(tableCell)
        {
            this.xmlElement = tableCell;
        }

        #region Interface :
        
        private ITableRowHeight tableRowHeight;
        public ITableRowHeight TableRowHeight
        {
            get
            {
                if (tableRowHeight == null)
                    tableRowHeight = PlatformTableRowHeight.New(xmlElement);

                return tableRowHeight;
            }
        }

        #endregion

        #region Static helpers methods

        public static PlatformTableRowProperties New(TableRow tableCell)
        {
            var xmlElement = CheckDescendantsOrAppendNewOne<TableRowProperties>(tableCell);
            return new PlatformTableRowProperties(xmlElement);
        }

        #endregion
    }
}
