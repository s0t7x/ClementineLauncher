using System;
using System.IO;
using System.Collections;

public class IniParser
{
    private Hashtable keyPairs = new Hashtable();
    private String iniFilePath;

    private struct SectionPair
    {
        public String Section;
        public String Key;
    }
    public IniParser(String iniPath)
    {
        TextReader iniFile = null;
        String strLine = null;
        String currentRoot = null;
        String[] keyPair = null;

        iniFilePath = iniPath;

        if (File.Exists(iniPath))
        {
            try
            {
                iniFile = new StreamReader(iniPath);
                strLine = iniFile.ReadLine();

                while (strLine != null)
                {
                    strLine = strLine.Trim().ToUpper();

                    if (strLine != "")
                    {
                        if (strLine.StartsWith("[") && strLine.EndsWith("]"))
                        {
                            currentRoot = strLine.Substring(1, strLine.Length - 2);
                        }
                        else
                        {
                            keyPair = strLine.Split(new char[] { '=' }, 2);

                            SectionPair sectionPair;
                            String value = null;

                            if (currentRoot == null)
                                currentRoot = "ROOT";

                            sectionPair.Section = currentRoot;
                            sectionPair.Key = keyPair[0];

                            if (keyPair.Length > 1)
                                value = keyPair[1];

                            keyPairs.Add(sectionPair, value);
                        }
                    }

                    strLine = iniFile.ReadLine();
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                if (iniFile != null)
                    iniFile.Close();
            }
        }
        else
            throw new FileNotFoundException("Unable to locate " + iniPath);

    }
    public String GetSetting(String sectionName, String settingName)
    {
        SectionPair sectionPair;
        sectionPair.Section = sectionName.ToUpper();
        sectionPair.Key = settingName.ToUpper();

        return (String)keyPairs[sectionPair];
    }
    public String[] EnumSection(String sectionName)
    {
        ArrayList tmpArray = new ArrayList();

        foreach (SectionPair pair in keyPairs.Keys)
        {
            if (pair.Section == sectionName.ToUpper())
                tmpArray.Add(pair.Key);
        }

        return (String[])tmpArray.ToArray(typeof(String));
    }




}