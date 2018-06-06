using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Net.Mail;
using Microsoft.VisualBasic;
public class FileUtils
{
    private string _ErrMessage;

    public string ErrMessage
    {
        get
        {
            return _ErrMessage;
        }
        set
        {
            _ErrMessage = value;
        }
    }

    public static bool CopyFile(string strOldFileName, string strNewFileName)
    {
        bool boolOK = true;
        strOldFileName = GetPhysicalPath(strOldFileName);
        strNewFileName = GetPhysicalPath(strNewFileName);
        try
        {
            File.Copy(strOldFileName, strNewFileName);
        }
        catch (Exception Err)
        {
            boolOK = false;
        }
        return boolOK;
    }

    public static bool DeleteFolder(string strFolderName)
    {
        bool boolOK = true;
        strFolderName = GetPhysicalPath(strFolderName);
        try
        {
            if (Directory.Exists(strFolderName))
            {
                Directory.Delete(strFolderName, true);
            }
        }
        catch (Exception Err)
        {
           
            boolOK = false;
        }
        return boolOK;
    }

    public static bool IsImageFile(string strFileName)
    {
        string strThisExtension = "";
        strThisExtension = GetFileExtension(strFileName);
        if (strThisExtension.ToLower() == ".gif" || strThisExtension.ToLower() == ".jpg" || strThisExtension.ToLower() == ".jpeg")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string GetFileExtension(string strFileName)
    {
        return System.IO.Path.GetExtension(strFileName);
    }

    public static bool MoveFile(string strOldFileName, string strNewFileName)
    {
        bool boolOK = true;
        strOldFileName = GetPhysicalPath(strOldFileName);
        strNewFileName = GetPhysicalPath(strNewFileName);
        try
        {
            File.Move(strOldFileName, strNewFileName);
        }
        catch (Exception Err)
        {
          
            boolOK = false;
        }
        return boolOK;
    }

    public static bool DeleteFile(string strFileName)
    {
        FileInfo fInfo;
        bool boolOK = true;
        strFileName = GetPhysicalPath(strFileName);
        try
        {
            fInfo = new FileInfo(strFileName);
            fInfo.Delete();
        }
        catch (Exception Err)
        {
            
            boolOK = false;
        }
        return boolOK;
    }

    public static string GetPhysicalPath(string strFileName)
    {
        strFileName = strFileName.Replace("//", "/");
        if (strFileName.IndexOf(":") == -1)
        {
            return HttpContext.Current.Server.MapPath(strFileName);
        }
        else
        {
            return strFileName;
        }
    }

    public bool FileExists(string FileFullPath)
    {
        FileInfo f = new FileInfo(FileFullPath);
        return f.Exists;
    }

    public bool FolderExists(string FolderPath)
    {
        System.IO.DirectoryInfo f = new DirectoryInfo(FolderPath);
        return f.Exists;
    }

    public bool CreateFolder(string strFolderName)
    {
        bool boolOK = false;
        try
        {
            DirectoryInfo di = Directory.CreateDirectory(strFolderName);
            boolOK = true;
        }
        catch (Exception Err)
        {
            ErrMessage += "Create folder failed:" + Err.Message;
        }
        return boolOK;
    }

    public bool SaveTextToFile(string strData, string strFullPath, string ErrInfo)
    {
        bool boolSuccess = false;
        StreamWriter objReader;
        try
        {
            objReader = new StreamWriter(strFullPath);
            objReader.Write(strData);
            objReader.Close();
            boolSuccess = true;
        }
        catch (Exception err)
        {
            ErrInfo = err.Message;
            ErrMessage += err.Message;
        }
        return boolSuccess;
    }

    public static string GetFileContents(string strFullPath, ref string ErrInfo)
    {
        string strContents = "";
        StreamReader objReader;
        try
        {
            objReader = new StreamReader(strFullPath);
            strContents = objReader.ReadToEnd();
            objReader.Close();

        }
        catch (Exception err)
        {
            ErrInfo = err.Message;
        }

        return strContents;
    }


}


