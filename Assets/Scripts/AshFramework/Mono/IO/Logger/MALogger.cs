using ASHFramework.Mono.Utility;
using System;
using System.IO;
using UnityEngine;

namespace ASHFramework.Mono.IO.Logger
{
    public class MALogger : MADispose
    {
        private bool _isExists = false;
        private string _fullPathName = string.Empty;
        private string _logFileName = string.Empty;
        private StreamWriter _writer = null;

        public MALogger(string fullPathName)
        {
            _fullPathName = MAUtility.GetAbsolutelyPath(fullPathName);
            _logFileName = BuildUniqueFileName();
        }

        protected override void Construct()
        {

        }

        protected override void Destruct()
        {
            _isExists = false;
            if (_writer != null) 
            { 
                _writer.Flush();
                _writer.Close();
                _writer.Dispose();
                _writer = null;
            }
        }

        private string BuildUniqueFileName()
        {
            string dirName = Path.GetDirectoryName(_fullPathName);
            string fileName = Path.GetFileNameWithoutExtension(_fullPathName);
            string extName = Path.GetExtension(_fullPathName);
            return string.Format("{0}/{1}_{2}{3}", dirName, fileName, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), extName);
        }

        public void Write(string text)
        {
            if (_isExists == false)
            {
                string dirName = Path.GetDirectoryName(_fullPathName);
                if (System.IO.Directory.Exists(dirName) == false)
                    System.IO.Directory.CreateDirectory(dirName);
                _isExists = true;
            }
            else
            {
                FileInfo fileInfo = new FileInfo(_logFileName);
                int size = (int)(fileInfo.Length / 1000000);
                if (size > 0)
                    _logFileName = BuildUniqueFileName();
            }
            _writer = new StreamWriter(_logFileName, true);
#if UNITY_EDITOR_WIN
            _writer.NewLine = MAUtility.DELIM;
#endif
            _writer.WriteLine(text);
            _writer.Flush();
            _writer.Close();
            _writer.Dispose();
            _writer = null;
        }

        public void Write(string format, params object[] messages)
        {
            Write(string.Format(format, messages));
        }
    }
}
