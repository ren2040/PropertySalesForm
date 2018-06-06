using System;
using System.Data;
using System.Configuration;
using System.Web;

using Property_Sale_Reservation_Form.Controllers;

/// <summary>
/// Summary description for AppOptions
/// </summary>
public class AppOptions
{
    public AppOptions()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetOption(string p)
    {

        return Helpers.GetConfigSetting(p);
    }


}
