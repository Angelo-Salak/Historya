using System;
using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor;
using System.IO;

namespace Paroxe.PdfRenderer.Internal
{
    public class WebGLPostBuild : ScriptableObject
    {
        private static string[] FilesToCopy = {
            @"Plugins/WebGL/pdf.js.bytes",
            @"Plugins/WebGL/pdf.js.map.bytes",
            @"Plugins/WebGL/pdf.worker.js.bytes",
            @"Plugins/WebGL/pdf.worker.js.map.bytes"
        };

        private static string[] FoldersToCopy =
        {
            @"Plugins/WebGL/cmaps"
        };


        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target == BuildTarget.WebGL)
            {
                ScriptableObject scriptableObject = CreateInstance<WebGLPostBuild>();
                MonoScript script = MonoScript.FromScriptableObject(scriptableObject);

                string scriptPath = AssetDatabase.GetAssetPath(script);
                DestroyImmediate(scriptableObject);

                DirectoryInfo directoryInfo = new DirectoryInfo(scriptPath);

                string baseFolder = directoryInfo.Parent.Parent.Parent.FullName;

                foreach (string sourceFile in FilesToCopy)
                {
                    string sourceFileFullPath = Path.Combine(baseFolder, sourceFile);

                    // Remove ".bytes" from filename
                    string destFileName = new FileInfo(sourceFileFullPath).Name;
                    destFileName = destFileName.Substring(0, destFileName.Length - 6);

                    File.Copy(sourceFileFullPath, Path.Combine(pathToBuiltProject, destFileName), true);
                }

                foreach (string sourceFolder in FoldersToCopy)
                {
                    string sourceFolderFullPath = Path.Combine(baseFolder, sourceFolder);

                    DirectoryInfo sourceDirectory = new DirectoryInfo(sourceFolderFullPath);

                    string targetFolder = sourceFolder.Replace(@"Plugins/WebGL/", string.Empty);
                    targetFolder = Path.Combine(pathToBuiltProject, targetFolder);

                    if (!Directory.Exists(targetFolder))
                        Directory.CreateDirectory(targetFolder);

                    foreach (FileInfo file in sourceDirectory.GetFiles())
                    {
                        if (file.Extension.EndsWith("meta", StringComparison.OrdinalIgnoreCase))
                            continue;

                        File.Copy(file.FullName, Path.Combine(targetFolder, file.Name), true);
                    }
                }
            }
        }
    }
}