using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.International.Converters.PinYinConverter;

namespace CaterCommon
{
    public partial class PinyinHelper
    {
        /// <summary>
        /// 获取简体中文字符串的拼音首字母,不是中文字符则不变
        /// </summary>
        /// <param name="sChinese">简体中文字符串</param>
        /// <returns></returns>
        public static string GetPinyin(string sChinese)
        {
            string sPinyinFirst = "";
            foreach (char ch in sChinese)
            {
                if (ChineseChar.IsValidChar(ch))
                {
                    ChineseChar cc = new ChineseChar(ch);
                    sPinyinFirst += cc.Pinyins[0][0];
                }
                else
                {
                    sPinyinFirst += ch;
                }
            }
            return sPinyinFirst;
        }
    }
}
