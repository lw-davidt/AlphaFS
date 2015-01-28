/*  Copyright (C) 2008-2015 Peter Palotas, Jeffrey Jangli, Alexandr Normuradov
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

using System;
using System.Security;

namespace Alphaleonis.Win32.Filesystem
{
   public static partial class File
   {
      #region TransferTimestamps

      /// <summary>[AlphaFS] Transfers the date and time stamps for the specified files.</summary>
      /// <remarks>This method does not change last access time for the source file.</remarks>
      /// <param name="sourcePath">The source file to get the date and time stamps from.</param>
      /// <param name="destinationPath">The destination file to set the date and time stamps.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>      
      [SecurityCritical]
      public static void TransferTimestamps(string sourcePath, string destinationPath, PathFormat pathFormat)
      {
         TransferTimestampsInternal(false, null, sourcePath, destinationPath, pathFormat);
      }

      /// <summary>[AlphaFS] Transfers the date and time stamps for the specified files.</summary>
      /// <remarks>This method does not change last access time for the source file.</remarks>
      /// <param name="sourcePath">The source file to get the date and time stamps from.</param>
      /// <param name="destinationPath">The destination file to set the date and time stamps.</param>      
      [SecurityCritical]
      public static void TransferTimestamps(string sourcePath, string destinationPath)
      {
         TransferTimestampsInternal(false, null, sourcePath, destinationPath, PathFormat.RelativePath);
      }

      /// <summary>[AlphaFS] Transfers the date and time stamps for the specified files.</summary>
      /// <remarks>This method does not change last access time for the source file.</remarks>
      /// <param name="transaction">The transaction.</param>
      /// <param name="sourcePath">The source file to get the date and time stamps from.</param>
      /// <param name="destinationPath">The destination file to set the date and time stamps.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>      
      [SecurityCritical]
      public static void TransferTimestamps(KernelTransaction transaction, string sourcePath, string destinationPath, PathFormat pathFormat)
      {
         TransferTimestampsInternal(false, transaction, sourcePath, destinationPath, pathFormat);
      }

      /// <summary>[AlphaFS] Transfers the date and time stamps for the specified files.</summary>
      /// <remarks>This method does not change last access time for the source file.</remarks>
      /// <param name="transaction">The transaction.</param>
      /// <param name="sourcePath">The source file to get the date and time stamps from.</param>
      /// <param name="destinationPath">The destination file to set the date and time stamps.</param>      
      [SecurityCritical]
      public static void TransferTimestamps(KernelTransaction transaction, string sourcePath, string destinationPath)
      {
         TransferTimestampsInternal(false, transaction, sourcePath, destinationPath, PathFormat.RelativePath);
      }


      #endregion // TransferTimestamps

      #region Internal Methods

      /// <summary>
      ///   [AlphaFS] Unified method TransferTimestampsInternal() to transfer the date and time stamps for the specified files and directories.
      /// </summary>
      /// <remarks>This method does not change last access time for the source file.</remarks>
      /// <remarks>This method uses BackupSemantics flag to get Timestamp changed for directories.</remarks>
      /// <param name="isFolder">
      ///   Specifies that <paramref name="sourcePath"/> and <paramref name="destinationPath"/> are a file or directory.
      /// </param>
      /// <param name="transaction">The transaction.</param>
      /// <param name="sourcePath">The source path.</param>
      /// <param name="destinationPath">The destination path.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>      
      [SecurityCritical]
      internal static void TransferTimestampsInternal(bool isFolder, KernelTransaction transaction, string sourcePath, string destinationPath, PathFormat pathFormat)
      {
         NativeMethods.WIN32_FILE_ATTRIBUTE_DATA attrs = GetAttributesExInternal<NativeMethods.WIN32_FILE_ATTRIBUTE_DATA>(transaction, sourcePath, pathFormat);

         SetFsoDateTimeInternal(isFolder, transaction, destinationPath, DateTime.FromFileTimeUtc(attrs.CreationTime), DateTime.FromFileTimeUtc(attrs.LastAccessTime), DateTime.FromFileTimeUtc(attrs.LastWriteTime), pathFormat);
      }

      #endregion // TransferTimestampsInternal
   }
}