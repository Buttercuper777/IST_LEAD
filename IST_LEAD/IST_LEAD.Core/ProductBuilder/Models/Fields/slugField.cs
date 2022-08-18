using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IST_LEAD.Core.ProductBuilder.Models.Fields;

public class slugField : BaseField
{
    
    private Dictionary<string, string> dictionaryChar = new Dictionary<string, string>()
    {
        {"а","a"},{"б","b"},{"в","v"},{"г","g"},{"д","d"},{"е","e"},
        {"ё","yo"},{"ж","zh"},{"з","z"},{"и","i"},{"й","y"},{"к","k"},
        {"л","l"},{"м","m"},{"н","n"},{"о","o"},{"п","p"},{"р","r"},
        {"с","s"},{"т","t"},{"у","u"},{"ф","f"},{"х","h"},{"ц","ts"},
        {"ч","ch"},{"ш","sh"},{"щ","sch"},{"ъ","'"},{"ы","yi"},{"ь",""},
        {"э","e"},{"ю","yu"},{"я","ya"}
    };
    
    private string ToEng(string source)
    {   
        var baseTetx = source.ToLower();
        var result = "";
        foreach (var ch in baseTetx)
        {
            var ss = "";
             
            if (dictionaryChar.TryGetValue(ch.ToString(), out ss))
            {
                result += ss;
            }
                
            else result += ch;
        }
        return result;
    }
    
    
    private string RemoveAccent(string txt)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        byte[] bytes = System.Text.Encoding.GetEncoding("windows-1251").GetBytes(txt);
        var res = System.Text.Encoding.ASCII.GetString(bytes);
        return res;
    }
    
    private string GenerateSlug(string phrase)
    {
        string str = RemoveAccent(phrase).ToLower();
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        str = Regex.Replace(str, @"\s+", " ").Trim();
        str = Regex.Replace(str, @"\s", "-"); 
        return str;
    }
    
    
    protected sealed override string Field { get; set; }

    public slugField(slugField item) : base(item) { }
    
    public slugField(string field) : base(field)
    {
        if (field != null)
        {
            var newSlug = field;
            var engSlug = ToEng(newSlug);
            Field = GenerateSlug(engSlug);
        }
    }
    
    
    
}