/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : WordHelperOpenXml.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Rikhu
 * Created on     : 4-09-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */

using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System;


namespace UMGI.GRCS.UI.Helper
{
    /// <summary>
    /// Helper Class For creating Word document using OpenXML
    /// </summary>
    public class WordHelperOpenXml
    {
        #region Table Helper Functions
        /// <summary>
        /// sets the properties for a Table Without Borders
        /// </summary>
        /// <param name="table"></param>
        public static void SetTablePropertiesWithNoBorders(Table table)
        {
            TableProperties tableProperties = new TableProperties(
                    new TableBorders(new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.None), Size = ExcelAndWordOpenXmlConstants.TableBorderSize },
                        new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.None), Size = ExcelAndWordOpenXmlConstants.TableBorderSize },
                        new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.None), Size = ExcelAndWordOpenXmlConstants.TableBorderSize },
                        new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.None), Size = ExcelAndWordOpenXmlConstants.TableBorderSize },
                        new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.None), Size = ExcelAndWordOpenXmlConstants.TableBorderSize },
                        new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.None), Size = ExcelAndWordOpenXmlConstants.TableBorderSize },
                        new TableWidth { Width = ExcelAndWordOpenXmlConstants.TableWidth, Type = TableWidthUnitValues.Pct })
                );
            TableCellMarginDefault tableCellMargin = new TableCellMarginDefault(
            new TableCellLeftMargin { Width = ExcelAndWordOpenXmlConstants.TableCellLeftMarginWidth, Type = TableWidthValues.Dxa },
            new TableCellRightMargin { Width = ExcelAndWordOpenXmlConstants.TableCellRightMarginWidth, Type = TableWidthValues.Dxa },
            new TopMargin { Width = ExcelAndWordOpenXmlConstants.TableCellTopMarginWidth, Type = TableWidthUnitValues.Dxa },
            new BottomMargin { Width = ExcelAndWordOpenXmlConstants.TableCellBottomMarginWidth, Type = TableWidthUnitValues.Dxa }
            );
            tableProperties.Append(tableCellMargin);
            table.Append(tableProperties);
        }

        /// <summary>
        /// Sets Properties for Table with Borders
        /// </summary>
        /// <param name="table"></param>
        public static void SetTablePropertiesWithBorders(Table table)
        {
            TableProperties tableProperties = new TableProperties(
                    new TableBorders(new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = ExcelAndWordOpenXmlConstants.TableBorderSize },
                        new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = ExcelAndWordOpenXmlConstants.TableBorderSize },
                        new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = ExcelAndWordOpenXmlConstants.TableBorderSize },
                        new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = ExcelAndWordOpenXmlConstants.TableBorderSize },
                        new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = ExcelAndWordOpenXmlConstants.TableBorderSize },
                        new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = ExcelAndWordOpenXmlConstants.TableBorderSize },
                        new TableWidth { Width = ExcelAndWordOpenXmlConstants.TableWidth, Type = TableWidthUnitValues.Pct })
                );
            TableCellMarginDefault tableCellMargin = new TableCellMarginDefault(
            new TableCellLeftMargin { Width = ExcelAndWordOpenXmlConstants.TableCellLeftMarginWidth, Type = TableWidthValues.Dxa },
            new TableCellRightMargin { Width = ExcelAndWordOpenXmlConstants.TableCellLeftMarginWidth, Type = TableWidthValues.Dxa },
            new TopMargin { Width = ExcelAndWordOpenXmlConstants.TableCellTopMarginWidth, Type = TableWidthUnitValues.Dxa },
            new BottomMargin { Width = ExcelAndWordOpenXmlConstants.TableCellBottomMarginWidth, Type = TableWidthUnitValues.Dxa }
            );
            tableProperties.Append(tableCellMargin);
            table.Append(tableProperties);
        }

        /// <summary>
        /// Row in the form of TableForm : Key as cell item & Value with different font size
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static TableRow AddTableFormRow(List<string> list)
        {
            TableRow tableRow = new TableRow();
            int cellWidth = Convert.ToInt32(ExcelAndWordOpenXmlConstants.TableWidth) / list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                tableRow.Append(i%ExcelAndWordOpenXmlConstants.AlternateTableColumnId == 0
                                    ? GetCell(list[i], ExcelAndWordOpenXmlConstants.FormKeyFontSize, string.Empty, cellWidth, string.Empty)
                                    : GetCell(list[i], ExcelAndWordOpenXmlConstants.FormValueFontSize, string.Empty, cellWidth, ExcelAndWordOpenXmlConstants.TableCellColorBackgroundShadeBlack));
            }
            return tableRow;
        }

        /// <summary>
        ///Adds Row Heading to a Table 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static TableRow AddTableFormRowHeader(List<string> list)
        {
            TableRow tableRow = new TableRow();
            int cellWidth = Convert.ToInt32(ExcelAndWordOpenXmlConstants.TableWidth) / list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                tableRow.Append(i%ExcelAndWordOpenXmlConstants.AlternateTableColumnId == 0
                                    ? GetCellHeader(list[i], cellWidth, string.Empty)
                                    : GetCellHeader(list[i], cellWidth, ExcelAndWordOpenXmlConstants.TableCellColorBackgroundShadeBlack));
            }
            return tableRow;
        }

        /// <summary>
        /// Create cell with specified text size
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textShade"> </param>
        /// <param name="cellWidth"></param>
        /// <param name="textSize"> </param>
        /// <param name="cellShadeColor"> </param>
        /// <returns></returns>
        public static TableCell GetCell(string text, int textSize, string textShade, int cellWidth, string cellShadeColor)
        {
            TableCell tableCell = new TableCell();
            tableCell.Append(new TableCellProperties(
                new TableCellWidth { Type = TableWidthUnitValues.Pct, Width = cellWidth.ToString() },
                new Shading { Val = ShadingPatternValues.Clear, Color = ExcelAndWordOpenXmlConstants.ColorAuto, Fill = cellShadeColor }
                ));
            tableCell.Append(CreateParagraph(text, textSize, textShade, ExcelAndWordOpenXmlConstants.Left));
            return tableCell;
        }

        /// <summary>
        /// Gets the TableCell Header
        /// </summary>
        /// <param name="text"></param>
        /// <param name="cellWidth"></param>
        /// <param name="cellShadeColor"> </param>
        /// <returns></returns>
        public static TableCell GetCellHeader(string text, int cellWidth, string cellShadeColor)
        {
            TableCell tableCell = new TableCell();
            tableCell.Append(new TableCellProperties(
                new TableCellWidth { Type = TableWidthUnitValues.Pct, Width = cellWidth.ToString() },
                new Shading { Val = ShadingPatternValues.Clear, Color = ExcelAndWordOpenXmlConstants.ColorAuto, Fill = cellShadeColor }
                ));
            tableCell.Append(CreateSectionHeaderParagraph(text));
            return tableCell;
        }

        #endregion

        #region Paragraph Helper Functions

        /// <summary>
        /// Creates Paragraph for a given details
        /// </summary>
        /// <param name="paragraphData"></param>
        /// <param name="textSize"> </param>
        /// <param name="textShadeColor"> </param>
        /// <param name="justificationValue"> </param>
        /// <returns></returns>
        public static Paragraph CreateParagraph(string paragraphData, int textSize, string textShadeColor, string justificationValue)
        {
            Paragraph paragraph = new Paragraph();
            if(justificationValue.Equals(ExcelAndWordOpenXmlConstants.Right))
            {
                paragraph.Append(new ParagraphProperties( new Justification { Val = JustificationValues.Right }));
            }
            Run run = new Run();
            RunProperties runProperties = new RunProperties(
                        new RunFonts { Ascii = ExcelAndWordOpenXmlConstants.FontArial, HighAnsi = ExcelAndWordOpenXmlConstants.FontArial, ComplexScript = ExcelAndWordOpenXmlConstants.FontArial },
                        new FontSize { Val = (textSize * ExcelAndWordOpenXmlConstants.FontMultiplicationForOpenXml).ToString() },
                        new Shading { Val = ShadingPatternValues.Clear, Color = ExcelAndWordOpenXmlConstants.ColorAuto, Fill = textShadeColor }
                        );
            run.Append(
                runProperties,
                new Text { Text = paragraphData }
                );
            paragraph.Append(run);
            return paragraph;
        }

        /// <summary>
        /// Creates Heading in a page
        /// </summary>
        /// <param name="paragraphData"></param>
        /// <returns></returns>
        public static Paragraph CreatePageHeaderParagraph(string paragraphData)
        {
            Paragraph paragraph = new Paragraph();
            Run run = new Run();
            RunProperties runProperties1 = new RunProperties(
                            new RunFonts { Ascii = ExcelAndWordOpenXmlConstants.FontArial, HighAnsi = ExcelAndWordOpenXmlConstants.FontArial, ComplexScript = ExcelAndWordOpenXmlConstants.FontArial },
                            new Bold(),
                            new FontSize { Val = ExcelAndWordOpenXmlConstants.FontSizePageHeader },
                            new Underline { Val = UnderlineValues.Single }
                            );
            run.Append(
                runProperties1,
                new Text { Text = paragraphData });
            paragraph.Append(run);
            return paragraph;
        }

        /// <summary>
        /// Creates Secion Heading
        /// </summary>
        /// <param name="paragraphData"></param>
        /// <returns></returns>
        public static Paragraph CreateSectionHeaderParagraph(string paragraphData)
        {
            Paragraph paragraph = new Paragraph();
            Run run = new Run();
            RunProperties runProperties = new RunProperties(
                            new RunFonts { Ascii = ExcelAndWordOpenXmlConstants.FontArial, HighAnsi = ExcelAndWordOpenXmlConstants.FontArial, ComplexScript = ExcelAndWordOpenXmlConstants.FontArial },
                            new Bold(),
                            new FontSize { Val = ExcelAndWordOpenXmlConstants.FontSizeSectionHeader }
            );
            run.Append(
                runProperties,
                new Text { Text = paragraphData }
                );
            paragraph.Append(run);
            return paragraph;
        }

        /// <summary>
        /// Appends Data to a Paragraph
        /// </summary>
        /// <param name="paragraph"></param>
        /// <param name="paragraphData"></param>
        public static Paragraph AddTextToParagraph(Paragraph paragraph, string paragraphData)
        {
            Run run = new Run();
            RunProperties runProperties = new RunProperties(
                            new RunFonts { Ascii = ExcelAndWordOpenXmlConstants.FontArial, HighAnsi = ExcelAndWordOpenXmlConstants.FontArial, ComplexScript = ExcelAndWordOpenXmlConstants.FontArial },
                            new FontSize { Val = ExcelAndWordOpenXmlConstants.FontSizeParagraph }
                            );
            run.Append(
                runProperties,
                new Text { Text = paragraphData }
                );
            paragraph.Append(run);
            return paragraph;
        }

        /// <summary>
        /// Creates an Empty Line
        /// </summary>
        /// <returns></returns>
        public static Paragraph EmptyLine()
        {
            return CreateParagraph(string.Empty, ExcelAndWordOpenXmlConstants.FormKeyFontSize, string.Empty, ExcelAndWordOpenXmlConstants.Left);
        }

        /// <summary>
        /// Creates Tab for the specified number of times : numberOfTabs
        /// </summary>
        /// <param name="paragraph"></param>
        /// <param name="numberOfTabs"></param>
        public static Paragraph AppendTab(Paragraph paragraph, int numberOfTabs)
        {
            while ((numberOfTabs--) != 0)
            {
                Run run = new Run();
                TabChar tabChar = new TabChar();
                run.Append(tabChar);
                paragraph.Append(run);
            }
            return paragraph;
        }

        #endregion

    }
}