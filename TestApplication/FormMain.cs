#region License
/*
MIT License

Copyright(c) 2023 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using Eto.Drawing;
using Eto.Forms;
using ScintillaNet.Abstractions.Classes;
using ScintillaNet.Abstractions.Classes.Lexers;
using ScintillaNet.Abstractions.Enumerations;


namespace TestApplication;
public class FormMain : Form
{
    private ScintillaNet.Eto.Scintilla scintilla; 
    
    public FormMain()
    {
        ClientSize = new Size(500, 500);
        base.Size = new Size(600, 600);
        scintilla = new ScintillaNet.Eto.Scintilla();
        Content = new TableLayout
        {
            Rows =
            {
                new TableRow(scintilla),
                new TableRow(new Button(Click) { Text = "Click me"}),
            },
        };
        scintilla.Size = new Size(500, 500);
        
        Shown += OnShown;
    }


        private void CreateCsStyling()
    {
        scintilla.Styles[Cpp.Preprocessor].ForeColor = Color.Parse("#804000");
        scintilla.Styles[Cpp.Preprocessor].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.Default].ForeColor = Color.Parse("#000000");
        scintilla.Styles[Cpp.Default].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.Word].Bold = true;
        scintilla.Styles[Cpp.Word].ForeColor = Color.Parse("#0000FF");
        scintilla.Styles[Cpp.Word].BackColor = Color.Parse("#FFFFFF");
        
        scintilla.Styles[Cpp.Word2].ForeColor = Color.Parse("#8000FF");
        scintilla.Styles[Cpp.Word2].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.Number].ForeColor = Color.Parse("#FF8000");
        scintilla.Styles[Cpp.Number].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.String].ForeColor = Color.Parse("#000080");
        scintilla.Styles[Cpp.String].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.Character].ForeColor = Color.Parse("#000000");
        scintilla.Styles[Cpp.Character].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.Operator].Bold = true;
        scintilla.Styles[Cpp.Operator].ForeColor = Color.Parse("#000080");
        scintilla.Styles[Cpp.Operator].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.Verbatim].ForeColor = Color.Parse("#000000");
        scintilla.Styles[Cpp.Verbatim].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.Regex].Bold = true;
        scintilla.Styles[Cpp.Regex].ForeColor = Color.Parse("#000000");
        scintilla.Styles[Cpp.Regex].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.Comment].ForeColor = Color.Parse("#008000");
        scintilla.Styles[Cpp.Comment].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.CommentLine].ForeColor = Color.Parse("#008080");
        scintilla.Styles[Cpp.CommentLine].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.CommentDoc].ForeColor = Color.Parse("#008080");
        scintilla.Styles[Cpp.CommentDoc].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.CommentLineDoc].ForeColor = Color.Parse("#008080");
        scintilla.Styles[Cpp.CommentLineDoc].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.CommentDocKeyword].Bold = true;
        scintilla.Styles[Cpp.CommentDocKeyword].ForeColor = Color.Parse("#008080");
        scintilla.Styles[Cpp.CommentDocKeyword].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.CommentDocKeywordError].ForeColor = Color.Parse("#008080");
        scintilla.Styles[Cpp.CommentDocKeywordError].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.PreprocessorComment].ForeColor = Color.Parse("#008000");
        scintilla.Styles[Cpp.PreprocessorComment].BackColor = Color.Parse("#FFFFFF");
        scintilla.Styles[Cpp.PreprocessorCommentDoc].ForeColor = Color.Parse("#008080");
        scintilla.Styles[Cpp.PreprocessorCommentDoc].BackColor = Color.Parse("#FFFFFF");
        var test = scintilla.Lexilla.CreateLexer("cpp");
        scintilla.LexerName = "cpp";
        
        scintilla.SetKeywords(0, "alignof and and_eq bitand bitor break case catch compl const_cast continue default delete do dynamic_cast else false for goto if namespace new not not_eq nullptr operator or or_eq reinterpret_cast return sizeof static_assert static_cast switch this throw true try typedef typeid using while xor xor_eq NULL");
        scintilla.SetKeywords(1, "alignas asm auto bool char char16_t char32_t class clock_t const constexpr decltype double enum explicit export extern final float friend inline int int8_t int16_t int32_t int64_t int_fast8_t int_fast16_t int_fast32_t int_fast64_t intmax_t intptr_t long mutable noexcept override private protected ptrdiff_t public register short signed size_t ssize_t static struct template thread_local time_t typename uint8_t uint16_t uint32_t uint64_t uint_fast8_t uint_fast16_t uint_fast32_t uint_fast64_t uintmax_t uintptr_t union unsigned virtual void volatile wchar_t");
        scintilla.SetKeywords(2, "a addindex addtogroup anchor arg attention author authors b brief bug c callergraph callgraph category cite class code cond copybrief copydetails copydoc copyright date def defgroup deprecated details diafile dir docbookonly dontinclude dot dotfile e else elseif em endcode endcond enddocbookonly enddot endhtmlonly endif endinternal endlatexonly endlink endmanonly endmsc endparblock endrtfonly endsecreflist enduml endverbatim endxmlonly enum example exception extends f$ f[ f] file fn f{ f} headerfile hidecallergraph hidecallgraph hideinitializer htmlinclude htmlonly idlexcept if ifnot image implements include includelineno ingroup interface internal invariant latexinclude latexonly li line link mainpage manonly memberof msc mscfile n name namespace nosubgrouping note overload p package page par paragraph param parblock post pre private privatesection property protected protectedsection protocol public publicsection pure ref refitem related relatedalso relates relatesalso remark remarks result return returns retval rtfonly sa secreflist section see short showinitializer since skip skipline snippet startuml struct subpage subsection subsubsection tableofcontents test throw throws todo tparam typedef union until var verbatim verbinclude version vhdlflow warning weakgroup xmlonly xrefitem");

        scintilla.SetProperty("fold", "1");
        scintilla.SetProperty("fold.compact", "1");
        scintilla.SetProperty("fold.preprocessor", "1");

        // Configure a margin to display folding symbols
        scintilla.Margins[2].Type = MarginType.Symbol;
        scintilla.Margins[2].Mask = MarkerConstants.MaskFolders;
        scintilla.Margins[2].Sensitive = true;
        scintilla.Margins[2].Width = 20;

        // Set colors for all folding markers
        for (int i = 25; i <= 31; i++)
        {
            scintilla.Markers[i].SetForeColor(Colors.LightGrey);
            scintilla.Markers[i].SetBackColor(Colors.DarkGray);
        }

        // Configure folding markers with respective symbols
        scintilla.Markers[MarkerConstants.Folder].Symbol = MarkerSymbol.BoxPlus;
        scintilla.Markers[MarkerConstants.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
        scintilla.Markers[MarkerConstants.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
        scintilla.Markers[MarkerConstants.FolderMidTail].Symbol = MarkerSymbol.TCorner;
        scintilla.Markers[MarkerConstants.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
        scintilla.Markers[MarkerConstants.FolderSub].Symbol = MarkerSymbol.VLine;
        scintilla.Markers[MarkerConstants.FolderTail].Symbol = MarkerSymbol.LCorner;

        // Enable automatic folding
        scintilla.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
    }

    private void Click(object? sender, EventArgs e)
    {
        //MessageBox.Show(scintilla.Lines.LastOrDefault()?.Text);
    }

    private void OnShown(object? sender, EventArgs e)
    {
        CreateCsStyling();
        MessageBox.Show(scintilla.Lexilla.LexerCount.ToString());
    }
}