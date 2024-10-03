using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq.Expressions;

namespace sg
{
    public class SaveFileDataWriter 
    {
        public string saveDataDirectoryPath = " ";
        public string saveFilename = " ";

        public bool CheckToSeeFileExists()
        {
            if (File.Exists(Path.Combine(saveDataDirectoryPath, saveFilename)))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public void DeleteSaveFile()
        {
            File.Delete(Path.Combine(saveDataDirectoryPath, saveFilename));
        }

        public void CreateNewCharacterSaveFile(CharacterSave characterData)
        {
            string Savepath = Path.Combine(saveDataDirectoryPath, saveFilename);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Savepath));
                Debug.Log("Creative Save File, At Save Path" + Savepath);

                string dataToStore = JsonUtility.ToJson(characterData, true);

                using (FileStream stream = new FileStream(Savepath, FileMode.Create))
                {
                    using (StreamWriter filewriter = new StreamWriter(stream))
                        filewriter.Write(dataToStore);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error,Game not saved" + Savepath + "\n" + ex);
            }


        }

        public CharacterSave Loadsave()
        {
            CharacterSave characterdata = null;
            string LoadPath = Path.Combine(saveDataDirectoryPath, saveFilename);


            if (File.Exists(LoadPath))
            {
                try
                {
                    string datatoload = " ";

                    using (FileStream stream = new FileStream(LoadPath, FileMode.Open))
                    {
                        using (StreamReader filereader = new StreamReader(stream))
                            datatoload = filereader.ReadToEnd();

                    }
                    characterdata = JsonUtility.FromJson<CharacterSave>(datatoload);
                }
                catch (Exception )
                {
                    Debug.Log("file is bland");
                }
            }
            return characterdata;
        }
    }
}
