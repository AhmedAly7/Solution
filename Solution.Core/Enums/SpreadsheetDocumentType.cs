namespace Solution.Core.Enums;

public enum SpreadsheetDocumentType
{
	Workbook,              /// Excel Workbook (*.xlsx).
	Template,              /// Excel Template (*.xltx).
	MacroEnabledWorkbook,  /// Excel Macro-Enabled Workbook (*.xlsm).
	MacroEnabledTemplate,  /// Excel Macro-Enabled Template (*.xltm).
	AddIn,                 /// Excel Add-In (*.xlam).
}