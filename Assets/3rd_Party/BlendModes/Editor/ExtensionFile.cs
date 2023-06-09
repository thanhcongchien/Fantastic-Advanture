﻿// Copyright 2014-2018 Elringus (Artyom Sovetnikov). All Rights Reserved.

using System.IO;
using UnityEngine;

namespace BlendModes
{
    /// <summary>
    /// Represents an extension file sources.
    /// </summary>
    [System.Serializable]
    public class ExtensionFile
    {
        /// <summary>
        /// Relative (rooting at <see cref="ExtensionPackage.PackagePath"/>) path to the extension file.
        /// </summary>
        public string FilePath { get { return filePath.Replace('\\', Path.DirectorySeparatorChar); } }
        /// <summary>
        /// Content (UTF-8) of the extension file.
        /// </summary>
        public string FileContent { get { return fileContent; } }

        [SerializeField] private string filePath;
        [SerializeField] private string fileContent;

        public ExtensionFile (string filePath, string fileContent)
        {
            this.filePath = filePath;
            this.fileContent = fileContent;
        }
    }
}
