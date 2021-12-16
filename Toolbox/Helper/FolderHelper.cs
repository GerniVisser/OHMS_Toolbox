using System;
using System.Collections.Generic;
using System.IO;

namespace Toolbox.Helper
{
    public static class FolderHelper
    {
        public static string UpperFolder(this string pFolderName, Int32 pLevel)
        {
            List<string> TheList = new List<string>();

            while (!string.IsNullOrEmpty(pFolderName))
            {
                var temp = Directory.GetParent(pFolderName);
                if (temp == null)
                {
                    break;
                }

                pFolderName = Directory.GetParent(pFolderName).FullName;
                TheList.Add(pFolderName);

            }

            if (TheList.Count > 0 && pLevel > 0)
            {
                if (pLevel - 1 <= TheList.Count - 1)
                {
                    return TheList[pLevel - 1];
                }
                else
                {
                    return pFolderName;
                }
            }
            else
            {
                return pFolderName;
            }
        }
        public static string SolutionFolder(this string pSender)
        {
            return pSender.UpperFolder(5);
        }
    }
}
