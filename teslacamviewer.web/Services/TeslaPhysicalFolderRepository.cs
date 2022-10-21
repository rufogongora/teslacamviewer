using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using teslacamviewer.web.Helpers;
using teslacamviewer.web.Models;

namespace teslacamviewer.web.Services
{
    public interface ITeslaPhysicalFolderRepository 
    {
        IEnumerable<PhysicalTeslaFolder> GetTeslaFolders();
        PhysicalTeslaFolder GetTeslaFolder(string folderName, string folderType);
        Task<byte[]> GetTeslaClip(string folderName, string teslaClip, string folderType);
        Task<byte[]> GetThumbnail(string folderName, string folderType);
        void DeleteTeslaFolder(string folderType, string folderName);
    }
    public class TeslaPhysicalFolderRepository : ITeslaPhysicalFolderRepository
    {
        private readonly IConfiguration _config;
        private readonly IFileSystem _fileSystem;
        private const string SENTRY_CLIPS_FOLDER_TYPE = "SentryClips";
        private const string SAVED_CLIPS_FOLDER_TYPE = "SavedClips";
        public TeslaPhysicalFolderRepository(
            IConfiguration config,
            IFileSystem fileSystem
            ) {
            _config = config;
            _fileSystem = fileSystem;
            RootFolder = _config["rootFolder"];
        }
        private readonly string RootFolder;
        public IEnumerable<PhysicalTeslaFolder> GetTeslaFolders()
        {
            ValidateFolders();
            var sentryDirectory = _fileSystem.Directory.GetDirectories(_fileSystem.Path.Join(RootFolder, SENTRY_CLIPS_FOLDER_TYPE));
            var savedClipsDirectory = _fileSystem.Directory.GetDirectories(_fileSystem.Path.Join(RootFolder, SAVED_CLIPS_FOLDER_TYPE));
            // var directories = Directory.GetDirectories(RootFolder);
            return BuildTeslaFolders(sentryDirectory, SENTRY_CLIPS_FOLDER_TYPE).Concat(BuildTeslaFolders(savedClipsDirectory, SAVED_CLIPS_FOLDER_TYPE));
        }

        public PhysicalTeslaFolder GetTeslaFolder(string folderName, string folderType) {
            return BuildTeslaFolder(_fileSystem.Path.Combine(RootFolder, folderType, folderName), folderType);
        }

        public async Task<byte[]> GetTeslaClip(string folderName, string teslaClip, string folderType) {
            using (var stream = _fileSystem.File.OpenRead(_fileSystem.Path.Combine(RootFolder, folderType, folderName, teslaClip))) {
                var result = new byte[stream.Length];
                await stream.ReadAsync(result, 0, (int)stream.Length);
                return result;
            }
        }

        public async Task<byte[]> GetThumbnail(string folderName, string folderType) {
            using (var stream = _fileSystem.File.OpenRead(_fileSystem.Path.Combine(RootFolder, folderType, folderName, "thumb.png"))) {
                var result = new byte[stream.Length];
                await stream.ReadAsync(result, 0, (int)stream.Length);
                return result;
            }
            
        }

        private IEnumerable<PhysicalTeslaFolder> BuildTeslaFolders(string[] directories, string folderType) {
            return directories.Select(d => BuildTeslaFolder(d, folderType)).Where(t => t != null && t.TeslaClips.Any()).ToList();
        }

        private PhysicalTeslaFolder BuildTeslaFolder(string directory, string folderType) {
                var files = _fileSystem.Directory.GetFiles(directory).ToList();
                if (TeslaFolderHelper.IsValidFolder(directory)) {
                    return new PhysicalTeslaFolder 
                    {
                        ActualPath = directory, 
                        Name = TeslaFolderHelper.TeslaFolderPathParser(directory),
                        TeslaEvent = BuildTeslaEvent(directory),
                        TeslaClips = BuildTeslaClips(directory),
                        Thumbnail = TeslaFolderHelper.ContainsThumbnail(files, directory),
                        FolderType = folderType
                    };
                }
                return null;
        }

        private PhysicalTeslaEvent BuildTeslaEvent(string directory) {
            if (!TeslaFolderHelper.ContainsTeslaEvent(directory))
            {
                return null;
            }
            using (StreamReader r = new StreamReader(_fileSystem.Path.Combine(directory, "event.json"))) 
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<PhysicalTeslaEvent>(json);
            }
        }

        private IEnumerable<PhysicalTeslaClip> BuildTeslaClips(string directory) {
            var files = _fileSystem.Directory
            .GetFiles(directory)
            .Where(c => Regex.Match(c, @"([a-zA-Z0-9\s_\\.\-\(\):])+(mp4)$").Success)
            .ToList();

            return files.Select(f => 
                new PhysicalTeslaClip 
                { 
                    Name = TeslaFolderHelper.TeslaClipPathParser(f),
                    ActualPath = f,
                    DateTime = TeslaFolderHelper.TeslaClipDateTimeParser(TeslaFolderHelper.TeslaClipPathParser(f)),
                    Side = TeslaFolderHelper.TeslaClipSideParser(f)
                });
        }

        private void ValidateFolders() {
            var sentryClipDir = _fileSystem.Path.Join(RootFolder, SENTRY_CLIPS_FOLDER_TYPE);
            var savedClipDir = _fileSystem.Path.Join(RootFolder, SAVED_CLIPS_FOLDER_TYPE);

            if (!_fileSystem.Directory.Exists(RootFolder) 
            || !_fileSystem.Directory.Exists(sentryClipDir) 
            || !_fileSystem.Directory.Exists(savedClipDir)) {
                throw new Exception($"The provided root teslacam directory does not exist. root: {RootFolder}, sentryClipDir: {sentryClipDir}, savedClipDir: {savedClipDir}");
            }
        }

        public void DeleteTeslaFolder(string folderType, string folderName)
        {
            string path = _fileSystem.Path.Join(RootFolder, folderType, folderName);
            if (_fileSystem.Directory.Exists(path)) {
                _fileSystem.Directory.Delete(path, true);
                return;
            }
            throw new Exception($"Path {path} does not exist");
        }
    }
}