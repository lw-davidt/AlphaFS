/* Copyright (C) 2008-2015 Peter Palotas, Jeffrey Jangli, Alexandr Normuradov
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy 
 *  of this software and associated documentation files (the "Software"), to deal 
 *  in the Software without restriction, including without limitation the rights 
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 *  copies of the Software, and to permit persons to whom the Software is 
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 *  THE SOFTWARE. 
 */

using System.IO;
using System.Linq;
using System.Security;

namespace Alphaleonis.Win32.Filesystem
{
   partial class DirectoryInfo
   {
      #region .NET

      /// <summary>Returns the subdirectories of the current directory.</summary>
      /// <returns>An array of <see cref="DirectoryInfo"/> objects.</returns>
      /// <remarks>If there are no subdirectories, this method returns an empty array. This method is not recursive.</remarks>
      /// <remarks>
      /// The EnumerateDirectories and GetDirectories methods differ as follows: When you use EnumerateDirectories, you can start enumerating the collection of names
      /// before the whole collection is returned; when you use GetDirectories, you must wait for the whole array of names to be returned before you can access the array.
      /// Therefore, when you are working with many files and directories, EnumerateDirectories can be more efficient.
      /// </remarks>
      [SecurityCritical]
      public DirectoryInfo[] GetDirectories()
      {
         return Directory.EnumerateFileSystemEntryInfosInternal<DirectoryInfo>(Transaction, LongFullName, Path.WildcardStarMatchAll, DirectoryEnumerationOptions.Folders, PathFormat.LongFullPath).ToArray();
      }

      /// <summary>Returns an array of directories in the current <see cref="DirectoryInfo"/> matching the given search criteria.</summary>
      /// <param name="searchPattern">
      ///   <para>The search string to match against the names of directories. This parameter can contain a</para>
      ///   <para>combination of valid literal path and wildcard (<see cref="Path.WildcardStarMatchAll"/> and <see cref="Path.WildcardQuestion"/>)</para>
      ///   <para>characters, but does not support regular expressions.</para>
      /// </param>
      /// <returns>An array of type <see cref="DirectoryInfo"/> matching <paramref name="searchPattern"/>.</returns>
      /// <remarks>
      /// The EnumerateDirectories and GetDirectories methods differ as follows: When you use EnumerateDirectories, you can start enumerating the collection of names
      /// before the whole collection is returned; when you use GetDirectories, you must wait for the whole array of names to be returned before you can access the array.
      /// Therefore, when you are working with many files and directories, EnumerateDirectories can be more efficient.
      /// </remarks>
      [SecurityCritical]
      public DirectoryInfo[] GetDirectories(string searchPattern)
      {
         return Directory.EnumerateFileSystemEntryInfosInternal<DirectoryInfo>(Transaction, LongFullName, searchPattern, DirectoryEnumerationOptions.Folders, PathFormat.LongFullPath).ToArray();
      }

      /// <summary>Returns an array of directories in the current <see cref="DirectoryInfo"/> matching the given search criteria and using a value to determine whether to search subdirectories.</summary>
      /// <param name="searchPattern">
      ///   <para>The search string to match against the names of directories. This parameter can contain a</para>
      ///   <para>combination of valid literal path and wildcard (<see cref="Path.WildcardStarMatchAll"/> and <see cref="Path.WildcardQuestion"/>)</para>
      ///   <para>characters, but does not support regular expressions.</para>
      /// </param>
      /// <param name="searchOption">
      ///   <para>One of the <see cref="SearchOption"/> enumeration values that specifies whether the <paramref name="searchOption"/></para>
      ///   <para> should include only the current directory or should include all subdirectories.</para>
      /// </param>
      /// <returns>An array of type <see cref="DirectoryInfo"/> matching <paramref name="searchPattern"/>.</returns>
      /// <remarks>If there are no subdirectories, or no subdirectories match the searchPattern parameter, this method returns an empty array.</remarks>
      /// <remarks>
      /// The EnumerateDirectories and GetDirectories methods differ as follows: When you use EnumerateDirectories, you can start enumerating the collection of names
      /// before the whole collection is returned; when you use GetDirectories, you must wait for the whole array of names to be returned before you can access the array.
      /// Therefore, when you are working with many files and directories, EnumerateDirectories can be more efficient.
      /// </remarks>
      [SecurityCritical]
      public DirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
      {
         var directoryEnumerationOptions = DirectoryEnumerationOptions.Folders;

         if (searchOption == SearchOption.AllDirectories)
            directoryEnumerationOptions |= DirectoryEnumerationOptions.Recursive;

         return Directory.EnumerateFileSystemEntryInfosInternal<DirectoryInfo>(Transaction, LongFullName, searchPattern, directoryEnumerationOptions, PathFormat.LongFullPath).ToArray();
      }

      #endregion // .NET
   }
}