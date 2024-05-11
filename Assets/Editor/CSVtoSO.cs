/* This script is for importing CSV files and creating scriptable objects
 / The format will depend on the columns from the CSV file, the format I used is on line 27
 / THIS FILE MUST BE IN THE "Editor" folder you created in the assets folder. 
 / This will output data to Resources/Questions folder
*/

using UnityEngine;
using UnityEditor;
using System.IO;

public class CVStoSO
{
    private static string questionsCSVPath = "/Editor/CSVs/RealQuestions.csv";
    private static string questionsPath = "Assets/Resources/Questions/";
    private static int numberOfAnswers = 4;
    
    [MenuItem("Utilities/Generate Questions")]
    
    public static void GeneratePhrases()
    {

        string[] allLines = File.ReadAllLines(Application.dataPath + questionsCSVPath);
        // Debug.Log(allLines[allLines.Length-1]); // csv sonuna kadar okunuyor eğer logda soru çıkıyorsa

        foreach (string s in allLines)
        {
            string[] splitData = s.Split(';');

            // CSV (COMMA SEPARATED VALUE) DATA FORMAT
            // QUESTION, CATEGORY, CORRECT ANSWER, WRONG ANSWER 1, WRONG ANSWER 2, WRONG ANSWER 3

            QuestionData questionData = ScriptableObject.CreateInstance<QuestionData>();
            questionData.question = splitData[0]; // 1st column from csv file
            questionData.category = splitData[1];

            // Initialize the array of answers
            questionData.answers = new string[4];

            // Check if the folder for generating questions does not exist
            if (!Directory.Exists(questionsPath))
            {
                // Create the directory as one does not exist (creates a folder)
                Directory.CreateDirectory(questionsPath);
            }

            for (int i = 0; i < numberOfAnswers; i++)
            {
                questionData.answers[i] = splitData[2 + i];
            }

            // CREATE THE FILE NAME
            // Remove the "?", file name cannot have that character
            // if (questionData.question.Contains("?") )
            // {
            //     // Questions will be named the same as the question text in this example
            //     questionData.name = questionData.question.Remove(questionData.question.IndexOf("?"));
            // }
            // else // Does not contain an invalid character, no changes required
            // {
            //     questionData.name = questionData.question;
            // }
            string derivedName = questionData.question; // soruyu başka bir değişkene atıyoruz.

            // Remove invalid characters from the name
            derivedName = derivedName.Replace("?", "").Replace("/", "").Replace(":", "").Replace("\"", "").Replace("ş", "s").Replace("ğ", "g") // soruda olan türkçe karakterler vs değiştiriliyor.
            .Replace("ü", "u").Replace("ö", "o").Replace("ç", "c").Replace("ı", "i").Replace("I", "i").Replace(",", "").Replace("\n", " ").Replace(".", " ").Replace("'", "");
            .Replace("ü", "u").Replace("ö", "o").Replace("ç", "c").Replace("ı", "i").Replace("I", "i").Replace(",", "").Replace("\n", " ").Replace(".", " ");
            // Assign the derived name to the ScriptableObject
            questionData.name = derivedName; // yeni soru ismi değiştrilmiş şekilde oluyor(scriptable object isimlendirmek için yani sorunun orijinali değişmiyor.)
            Debug.Log($"Creating ScriptableObject with name: {questionData.name}");

            
            // Save this in the questionsPathfolder to load them later by script
            AssetDatabase.CreateAsset(questionData, $"{questionsPath}/{questionData.name}.asset");
            Debug.Log($"{questionData.name} Scriptable object is created");
        }

        AssetDatabase.SaveAssets();

        Debug.Log($"Generated Questions");
    }

    
}
