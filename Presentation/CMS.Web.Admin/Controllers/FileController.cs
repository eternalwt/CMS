using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Web.Admin.Models;
using System.IO;
using CMS.Web.FrameWork.Helpers;
using CMS.Web.FrameWork.Models;

namespace CMS.Web.Admin.Controllers {
	public class FileController : Controller {


		[HttpPost]
		[ValidateInput(false)]
		[AcceptVerbs(HttpVerbs.Post)]
		public JsonResult Upload() {
			JsonResultInfo result = new JsonResultInfo();
			List<ErrorInfo> errors = new List<ErrorInfo>();
			string uploadpath = Convert.ToString(Request["uploadpath"]);
			int index;
			if (string.IsNullOrEmpty(uploadpath) == false) {
				UploadFileModel model;
				if (Request.Files != null) {
					for (index = 0; index < Request.Files.Count; index++) {
						if (Request.Files[index] != null) {
							if (string.IsNullOrEmpty(Request.Files[index].FileName) == false) {
								model = UploadFileHelper.Upload(Request.Files[index], "FileUploadPath", uploadpath, Request.Files[index].FileName);
							}
						}
					}
				}
			} else {
				errors.Add(new ErrorInfo("", "Upload type is required"));
			}
			return Json(errors);
		}

		public JsonResult FilesList(string directoryName) {
			List<DirectoryModal> directories = new List<DirectoryModal>();
			string path = Path.Combine(Server.MapPath("~/"), directoryName);
			DirectoryInfo dirInfo = new DirectoryInfo(path);
			path = Path.Combine(Server.MapPath("~/"), directoryName, "thumbs");
			if (Directory.Exists(path) == false) {
				Directory.CreateDirectory(path);
			}
			FileInfo[] files = dirInfo.GetFiles();
			List<FileModel> fileList = new List<FileModel>();
			string destFileName = string.Empty;
			string rootPath = Server.MapPath("~/");
			FileModel fleModel = new FileModel();
			foreach (var file in files) {
				if (file.Name != "Thumbs.db") {
					destFileName = System.IO.Path.Combine(dirInfo.FullName, "thumbs", file.Name);
					if (System.IO.File.Exists(destFileName) == false) {
						FileHelper.CreateThumbNail(file.FullName, destFileName, 160, 120);
					}
					fleModel = new FileModel {
						FileName = file.Name,
						FullName = file.FullName
									.Replace(Server.MapPath("~/"), ""),
						FilePath = "/" + file.FullName
												.Replace(Server.MapPath("~/"), "")
												.Replace("\\", "/")
					};
					if (System.IO.File.Exists(destFileName)) {
						fleModel.ThumbFilePath = "/" + destFileName
							.Replace(Server.MapPath("~/"), "")
							.Replace("\\", "/");
						fleModel.IsImage = true;
					}
					fileList.Add(fleModel);
				}
			}
			return Json(new { files = fileList }, JsonRequestBehavior.AllowGet);
		}


		public JsonResult DirectoryList(string directoryName) {
			List<DirectoryModal> directories = new List<DirectoryModal>();
			string path = Server.MapPath(string.Format("~/Files/{0}", directoryName));
			FileDirectoryInfo(ref directories, path);
			return Json(new { dirs = directories }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public void DeleteFile(FormCollection collection) {
			try {
				var files = collection["FileName"];
				if (string.IsNullOrEmpty(files) == false) {
					var arrFiles = files.Split((",").ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
					foreach (string fname in arrFiles) {
						List<DirectoryModal> directories = new List<DirectoryModal>();
						string fileName = Path.Combine(Server.MapPath("~/"), fname);
						if (System.IO.File.Exists(fileName)) {
							System.IO.FileInfo fileInfo = new FileInfo(fileName);
							System.IO.File.Delete(fileName);
							string dirName = fileInfo.DirectoryName;
							fileName = Path.Combine(dirName, "thumbs", fileInfo.Name);
							if (System.IO.File.Exists(fileName)) {
								System.IO.File.Delete(fileName);
							}
						}
					}
				}
			} catch { }
		}

		private void FileDirectoryInfo(ref List<DirectoryModal> directories, string directoryName) {
			DirectoryInfo dirInfo = new DirectoryInfo(directoryName);
			DirectoryInfo[] dirs = dirInfo.GetDirectories();
			FileInfo[] files = dirInfo.GetFiles();
			DirectoryModal dirModel = new DirectoryModal { DirectoryName = dirInfo.Name, DirectoryFullName = dirInfo.FullName.Replace(Server.MapPath("~/"), "") };
			directories.Add(dirModel);
			foreach (var dir in dirs) {
				List<DirectoryModal> subDirs = dirModel.Directories;
				if (dir.Name != "thumbs") {
					FileDirectoryInfo(ref subDirs, dir.FullName);
				}
			}
		}
	}
}
